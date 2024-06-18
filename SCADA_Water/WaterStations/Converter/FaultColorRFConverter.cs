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
    public class FaultColorRFConverter : IValueConverter
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
            {
 var param = parameter.ToString();

                switch (param)
                {
                    case "RFFP":
                    {
                        bool rf = ((byte) value & 1 << 0) == (1 << 0);
                        return (rf) ? CorrectBrush : ErrorBrush;
                    }
                    case "RTUFP":
                        {
                            
                            return  CorrectBrush ;
                        }
                    case "RFSSW":
                    {
                        return ((byte) value > 0) ? CorrectBrush : ErrorBrush;
                    }
                    case "LoggerSSW":
                    {
                        return (((byte) value & 1) == 1) ? CorrectBrush : ErrorBrush;
                    }
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
