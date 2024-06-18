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
using ReporterWPF.WaterStations.Enums;

namespace ReporterWPF.WaterStations.Converter
{
    public class VoltageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                switch (parameter.ToString())
                {
                    case "RFRSS":
                    {
                        if (System.Convert.ToByte(value.ToString()) >= 2)
                            return "-"+ value.ToString() +"dBm";
                        return "-";
                    }
                    case "RTUBatt":
                    {
                            return  (float)Math.Round(System.Convert.ToDouble(value.ToString()), 1)+"V"; 
                    }
                    case "Solar":
                    {
                            return  (float)Math.Round(System.Convert.ToDouble(value.ToString()), 1)+"V"; 
                    }
                    case "LevelReservoir":
                        return  (float)Math.Round(System.Convert.ToDouble(value.ToString()), 1);
                    case "TempReservoir":
                        return (float)Math.Round(System.Convert.ToDouble(value.ToString()), 1);
                    case "FlotterStatus":
                    {
                        bool floaters = ((System.Convert.ToByte(value.ToString()) & 2) == 2);
                        return (floaters) ? ControlModeParameterResource.Working : ControlModeParameterResource.NeedService;
                    }

                    case "RFLoggerVisible":
                    {
                        return (((System.Convert.ToByte(value.ToString()) >> 7)) & 1 ) == 0
                            ? Visibility.Visible
                            : Visibility.Hidden;
                        }
                    case "GSMLoggerVisible":
                    {
                            return (((System.Convert.ToByte(value.ToString())) >> 7) & 1 ) == 1
                         ? Visibility.Visible
                         : Visibility.Hidden;
                        }
                }
                return "";
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
