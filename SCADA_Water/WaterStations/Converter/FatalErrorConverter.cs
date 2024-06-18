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
    public class FatalErrorConverter : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var error = (((byte)value  & 0x40) == 0x40);
                return error ? Visibility.Visible : Visibility.Hidden;

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
