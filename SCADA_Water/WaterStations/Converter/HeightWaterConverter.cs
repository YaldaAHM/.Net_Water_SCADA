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
    public class HeightWaterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                switch (parameter.ToString())
                {
                    case "RTUHOn": return ((float)Math.Round(System.Convert.ToDouble(value.ToString()), 1)).ToString()+"*";
                    case "RTUHOff": return (100- (float)Math.Round(System.Convert.ToDouble(value.ToString()), 1)).ToString()+"*";

                    case "ValveHOn": return (50).ToString() + "*";
                    case "ValveHOff": return (50).ToString() + "*";
                }
                return "0*";
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
