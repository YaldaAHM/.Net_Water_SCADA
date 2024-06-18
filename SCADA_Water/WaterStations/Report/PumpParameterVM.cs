using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReporterWPF.WaterStations.Enums;

namespace ReporterWPF.WaterStations.Report
{
   public class PumpParameterVM : ObservableCollection<String>, INotifyPropertyChanged
    {
        public PumpParameterVM()
            : base()
        {
            
            foreach (var pp in Enum.GetValues(typeof(PumpParameter)))
            {
                   var attributes =
                                 (DescriptionAttribute[])
                                     pp.GetType()
                                         .GetField(pp.ToString())
                                         .GetCustomAttributes(typeof(DescriptionAttribute), false);
            var d = ((attributes.Length > 0) && (!String.IsNullOrEmpty(attributes[0].Description)))
                ? attributes[0].Description
                : pp;
                Add(d.ToString());
            }

        }
      

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;


    }
}
