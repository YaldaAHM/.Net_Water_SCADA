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
    public class RTU_ModeMultiConverter : IMultiValueConverter
    {
       

        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            try
            {
                if (values[0] == DependencyProperty.UnsetValue || values[1]==DependencyProperty.UnsetValue)
                {
                    return null;
                }
                byte RTU_Mode = (byte) (System.Convert.ToByte(values[0].ToString()) & 0x07);
                bool is_rtu = (System.Convert.ToByte(values[1].ToString()) & 1 << 6) != (1 << 6);
                bool is_timer = ((byte)System.Convert.ToByte(values[1].ToString()) & 1 << 1) != (1 << 1);
                if (is_rtu)
                {

                    switch (RTU_Mode)
                    {
                        case 1:
                            return ControlModeParameterResource.Auto ;
                        case 2:
                            return ControlModeParameterResource.SERVER;
                        case 3:
                            return ControlModeParameterResource.SMS;
                        default:
                            return ControlModeParameterResource.Time;

                    }
                }
                 if (is_timer)
                {
                    return ControlModeParameterResource.Time;
                }

                return ControlModeParameterResource.Manual;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
