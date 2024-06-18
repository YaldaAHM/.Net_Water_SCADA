using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class SelectedComboBoxPSConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
switch (parameter.ToString())
            {
                case "PS":
                    {
                        ObservableCollection< Pump_Station> PumpStationSelected = (ObservableCollection<Pump_Station>)(value);
                 
                    return PumpStationSelected[0];
                    //return 1;
                }
                case "SS":
                {
                    return 1;
                }
            }
            return 1;
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
