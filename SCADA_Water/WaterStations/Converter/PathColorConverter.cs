using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ReporterWPF.WaterStations.Converter
{
    public class PathColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
 var myResourceDictionary = new ResourceDictionary();
            myResourceDictionary.Source =
                new Uri("Styles\\WaterStation.xaml",
                         UriKind.RelativeOrAbsolute);
            var param = parameter.ToString();
            byte rtu = (byte) value;
            bool fuse_01 = (rtu & 1 << 0) != (1 << 0);
            bool phase_c = (rtu & 1 << 5) != (1 << 5);
            bool bimetal = (rtu & 1 << 4) != (1 << 4);
            bool power_mt = false;
            bool main_cnt = (rtu & 1 << 7) != (1 << 7);
            bool delta_cnt = (rtu & 1 << 3) != (1 << 3);
            bool star_cnt = (rtu & 1 << 2) != (1 << 2);
            bool is_rtu = (rtu & 1 << 6) != (1 << 6);
            bool is_timer = (rtu & 1 << 1) != (1 << 1);
            switch (param)
            {
                   
                    case "FuseH":
                        return (fuse_01) ? myResourceDictionary["ControlTemplateLineHOn"] : myResourceDictionary["ControlTemplateLineHOff"];
                    case "PhaseH":
                        return (phase_c) ? myResourceDictionary["ControlTemplateLineHOn"] : myResourceDictionary["ControlTemplateLineHOff"];
                    case "BimetalH":
                        return (bimetal) ? myResourceDictionary["ControlTemplateLineHOn"] : myResourceDictionary["ControlTemplateLineHOff"];
                    case "BimetalV":
                        return (bimetal) ? myResourceDictionary["ControlTemplateLineVOn"] : myResourceDictionary["ControlTemplateLineVOff"];
                    case "ContactorMainV":
                        return (main_cnt) ? myResourceDictionary["ControlTemplateLineVOn"] : myResourceDictionary["ControlTemplateLineVOff"];
                    case "ContactorStarH":
                        return (main_cnt) ? myResourceDictionary["ControlTemplateLineHOn"] : myResourceDictionary["ControlTemplateLineHOff"];
                    case "ContactorTriangleH":
                        return (main_cnt) ? myResourceDictionary["ControlTemplateLineHOn"] : myResourceDictionary["ControlTemplateLineHOff"];
                    case "MotorH":
                        return (delta_cnt) ? myResourceDictionary["ControlTemplateLineHOn"] : myResourceDictionary["ControlTemplateLineHOff"];

                   
                    case "RTUV1":
                        return myResourceDictionary["ControlTemplateLineVOff"];
                    case "RTUV2":
                        return myResourceDictionary["ControlTemplateLineVOff"];
                    case "RTUV3":
                        return myResourceDictionary["ControlTemplateLineVOff"];
                    case "RTUH1":
                        return myResourceDictionary["ControlTemplateLineHOff"];
                    case "RTUH2":
                        return myResourceDictionary["ControlTemplateLineHOff"];
                    case "RTUH3":
                        return myResourceDictionary["ControlTemplateLineHOff"];
                }
            
            return myResourceDictionary["ControlTemplateLineHOff"];
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
