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
    public class EnableControlConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null) return null;
                var param = parameter.ToString();


                switch (param)
                {
                    case "ToggleSwitchControlMode":
                        return (Cont_Mode)(value) == Cont_Mode.SERVER;


                }
                return false;
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
