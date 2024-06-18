using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ReporterWPF.WaterStations.Converter
{
    public class FaultColorMultiConverter : IMultiValueConverter
    {
        public Color CorrectBrush
        {
            get { return Color.FromArgb(255,0,128,0); }
            set { }
        }

        public Color WarningBrush
        {
            get { return Color.FromArgb(255, 255, 153, 0); }
            set { }
        }

        public Color ErrorBrush
        {
            get { return Color.FromArgb(255, 255, 0, 0); }
            set { }
        }

        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            try
            {
               
                if (int.Parse(values[1].ToString()) != 0)
                {
                    return WarningBrush;
                }
                if (int.Parse(values[0].ToString()) == 0)
                {
                    return ErrorBrush;
                }
                return CorrectBrush;
            

            }
            catch (Exception ex)
            {

                return CorrectBrush;
            }
           
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
