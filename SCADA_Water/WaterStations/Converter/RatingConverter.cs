using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ReporterWPF.WaterStations.Converter
{
    public class RatingConverter : IValueConverter
    {
        private Utils.Utils utils;
        public Brush OnBrush { get { return Brushes.Green; } set { } }
        public Brush OffBrush { get { return Brushes.Gray; } set { } }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
 var param = parameter.ToString();
           var RF = (byte)value;
            switch (param)
            {
                case "1":
                    return ((RF >= 2 && RF < 90) || RF == 1) ? OnBrush : OffBrush;
                case "2":
                    return ((RF >= 2 && RF < 80) || RF == 1) ? OnBrush : OffBrush;
                case "3":
                    return ((RF >= 2 && RF < 70) || RF == 1) ? OnBrush : OffBrush;
                case "4":
                    return ((RF >= 2 && RF < 60) || RF == 1) ? OnBrush : OffBrush;

            }


            return OffBrush;
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
