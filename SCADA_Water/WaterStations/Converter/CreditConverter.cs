using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Reporter.Localization.Parameter;

namespace ReporterWPF.WaterStations.Converter
{
    public class CreditConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                float cred = (float) value;
                return (cred == 0) ? ControlModeParameterResource.PermanentSIMcard: (cred.ToString() +
                    ControlModeParameterResource.Rial
);
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
