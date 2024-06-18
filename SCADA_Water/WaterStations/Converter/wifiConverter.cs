using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using ReporterWPF.Utils;

namespace ReporterWPF.WaterStations.Converter
{
    public class wifiConverter : IValueConverter
    {
         private DateConverter dateConverter=new DateConverter();
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
 return dateConverter.GetNumber((-115 + (byte.Parse(value.ToString())*2)).ToString()) + " dBm";
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
