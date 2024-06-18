using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Editors.Helpers;
using ReporterWPF.Utils;

namespace ReporterWPF.WaterStations.Converter
{
    public class ChartContentConverter : DependencyObject, IValueConverter
    {


        #region IValueConverter Members
        private DateConverter dateConverter=new DateConverter();
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
             
             var result=   value.TryConvertToDateTime();
                if(result==null)
                    return "";
               

                return   dateConverter.ToDateString(result)  ;
    
            }

            catch (Exception ex)
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
           
            return value;

        }

        #endregion
    }
}