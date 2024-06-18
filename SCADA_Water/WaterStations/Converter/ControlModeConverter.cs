using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ReporterWPF.WaterStations.Enums;

namespace ReporterWPF.WaterStations.Converter
{
    public class ControlModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                byte RTU_Mode = (byte) (System.Convert.ToByte(value.ToString()) & 0x07);
                //  return RTU_Mode - 1;
                if(RTU_Mode==0) return (Cont_Mode)(RTU_Mode);
                return (Cont_Mode) (RTU_Mode - 1);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
