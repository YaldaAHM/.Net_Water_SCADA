using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ReporterWPF.Management.VMs
{
    internal class InputAnalogVM : ObservableCollection<InputAnalog>, INotifyPropertyChanged
    {
        
        public InputAnalogVM()
            : base()
        {
            this.NotifyPropertyChanged("Id");
            this.NotifyPropertyChanged("Name");

            for (int i = 0; i < 29; i++)
            {
                   Add(new InputAnalog( 1, "AI"+i.ToString()));
            }


        }

        public InputAnalogVM(string name)
            : base()
        {
            
        }



        private int _inputAnalogEntry;

        public int InputAnalogEntry
        {
            get { return _inputAnalogEntry; }
            set
            {
                if (_inputAnalogEntry == value) return;
                _inputAnalogEntry = value;
                OnPropertyChanged("inputAnalogEntry");
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
