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
    public class PumpMotorrConverter : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool rtu1 = true;
                var param = parameter.ToString();
                byte rtu = (byte) value;

                bool main_cnt = (rtu & 1 << 7) != (1 << 7);
                bool delta_cnt = (rtu & 1 << 3) != (1 << 3);
                switch (param)
                {
                    case "PumpMotorCbx":
                        return !(main_cnt && delta_cnt);
                    case "PumpMotorWL":
                        return (main_cnt && delta_cnt) ? "0:0:01" : "0:0:0";
                    default:
                        return (main_cnt && delta_cnt) ? "0:0:01" : "0:0:0";
                }
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
