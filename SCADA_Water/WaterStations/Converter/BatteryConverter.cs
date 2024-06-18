using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ReporterWPF.WaterStations.Converter
{
    public class BatteryConverter : IMultiValueConverter
    {
        public BitmapImage FullBattery
        {
            get { return new BitmapImage(new Uri("..\\..\\Image\\Pic\\battery_l4.png", UriKind.Relative)); }
            //get { return "Image/Pic/bat_lvl_four.png"; }
            set { }
        }
        public BitmapImage HalfFullBattery
        {
            get { return new BitmapImage(new Uri("..\\..\\Image\\Pic\\battery_l3.png", UriKind.Relative)); }
            //get { return "Image/Pic/bat_lvl_four.png"; }
            set { }
        }
        public BitmapImage HalfBattery
        {
            get { return new BitmapImage(new Uri("..\\..\\Image\\Pic\\battery_l2.png", UriKind.Relative)); }
            //get { return "Image/Pic/bat_lvl_three.png"; }
            set { }
        }

        public BitmapImage LowBattery
        {
            get { return new BitmapImage(new Uri("..\\..\\Image\\Pic\\battery_l1.png", UriKind.Relative)); }
           // get { return "Image/Pic/bat_lvl_two.png"; }
            set { }
        }

        public BitmapImage EmptyBattery
        {
            get { return new BitmapImage(new Uri("..\\..\\Image\\Pic\\battery_l0.png", UriKind.Relative)); }
            // return "Image/Pic/bat_lvl_zero.png"; }
            set { }
        }

        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            try
            {
                if (values[0] == DependencyProperty.UnsetValue)
                {
                    return null;
                }
                float battery_v = (float)Math.Round(float.Parse(values[0].ToString()), 1);
                if (battery_v < 9) return EmptyBattery;
                else if (battery_v < 10.5) return LowBattery;
                else if (battery_v < 11.7) return HalfBattery;
                else return FullBattery;
               
            }
            catch (Exception ex)
            {
                return EmptyBattery;
            }

        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
