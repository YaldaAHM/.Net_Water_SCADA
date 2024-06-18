using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ReporterWPF.WaterStations.Converter
{
    public class FaultColorConverter : IValueConverter
    {
        public Color CorrectBrush
        {
            get { return Color.FromArgb(255, 0, 128, 0); }
            set { }
        }

        public Color WarningBrush
        {
            get { return Color.FromArgb(255, 255, 153, 0); }
            set { }
        }

        public Color ErrorBrush
        {
            get { return Color.FromArgb(255, 255, 0, 0); }
            set { }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          
            try
            {  bool rtu1 = true;
            var param = parameter.ToString();
            byte rtu = (byte) value;
            bool fuse_01 = (rtu & 1 << 0) != (1 << 0);
            bool phase_c = (rtu & 1 << 5) != (1 << 5);
            bool bimetal = (rtu & 1 << 4) != (1 << 4);
            bool power_mt = false;
            bool main_cnt = (rtu & 1 << 7) != (1 << 7);
            bool delta_cnt = (rtu & 1 << 3) != (1 << 3);
            bool star_cnt = (rtu & 1 << 2) != (1 << 2);
            bool is_rtu = (rtu & 1 << 6) != (1 << 6);
            bool is_timer = (rtu & 1 << 1) != (1 << 1);
            switch (param)
            {
                case "FuseF":
                      return  (fuse_01) ? CorrectBrush : ErrorBrush;
                case "PhaseF":
                    return (phase_c) ? CorrectBrush : ErrorBrush;
                case "BimetalF":
                    return (bimetal) ? CorrectBrush : ErrorBrush;
                case "PMF":
                    return (power_mt) ? CorrectBrush : ErrorBrush;
                case "ContactorMainF":
                    return (main_cnt) ? CorrectBrush : ErrorBrush;
                case "ContactorStarF":
                    return (main_cnt) ? CorrectBrush : ErrorBrush;
                case "ContactorTriangleF":
                    return (main_cnt) ? CorrectBrush : ErrorBrush;
                case "RTUF":
                    return (rtu1) ? CorrectBrush : ErrorBrush;
                case "RFF":
                    return (rtu1) ? CorrectBrush : ErrorBrush;
                case "MotorF":
                    return (main_cnt && (delta_cnt || star_cnt)) ? CorrectBrush : ErrorBrush;
                        
            }
            
            return WarningBrush;

            }
            catch (Exception ex)
            {
                return null;
            }

          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
