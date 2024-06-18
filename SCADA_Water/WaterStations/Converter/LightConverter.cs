using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ReporterWPF.WaterStations.Converter
{
    public class LightConverter : IValueConverter
    {
        private static Brush B1 = Brushes.LightGreen;
        private static Brush B2 = Brushes.Salmon;
        private static Brush B3 = Brushes.Gold;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return value == null ? B3 : ((bool)value ? B1 : B2);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.Equals(B1)) return true;
            else if (value.Equals(B2)) return false;
            else return null;
        }
    }
}
