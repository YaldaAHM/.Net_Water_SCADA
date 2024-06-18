using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporterWPF.ViewModel
{
    internal class PumpListBind : ObservableCollection<PumpStructVM>, INotifyPropertyChanged
    {
        
        public PumpListBind()
            : base()
        {
            this.NotifyPropertyChanged("ID");
            this.NotifyPropertyChanged("Name");
           
        }

      



        private int deviceSetting;

        public int DeviceSetting
        {
            get { return deviceSetting; }
            set
            {
                if (deviceSetting == value) return;
                deviceSetting = value;
                OnPropertyChanged("PumpStruct");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string Name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(Name));
        }
    }
}
