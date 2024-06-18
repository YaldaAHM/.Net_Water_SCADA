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
    public class ControlModeButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool main_cnt = ((byte) value & 1 << 7) != (1 << 7);
                return main_cnt;
            }
           catch (Exception exception)
           {
               return null;
           }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
