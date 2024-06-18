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
    public class PumpStations : INotifyPropertyChanged      
    {

        public ObservableCollection<PumpStation> PStations { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
    public class PumpStation
    {
        public string Name { get; set; }
    }
}
