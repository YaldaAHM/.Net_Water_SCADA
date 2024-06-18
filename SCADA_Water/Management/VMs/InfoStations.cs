using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReporterWPF.Management.VMs
{   
    public class InfoStations : INotifyPropertyChanged      
    {

        public ObservableCollection<InfoStation> Stations { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }


    public class InfoStation
    {
        public string Name { get; set; }
    }
}
