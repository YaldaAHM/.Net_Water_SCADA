using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using ReporterWPF.WaterStations;

namespace ReporterWPF.ViewModel
{
    public class PSVM : INotifyPropertyChanged
    {
        //private MainVM mainVM;
        private Pump_Station pumpStation;
        public PSVM()
        {
            
            pumpStation = new Pump_Station();
            PSsVM = new ObservableCollection<Pump_Station>();
            UpDate();
        }
      
        public void UpDate()
        {
         
            PSsVM.Clear();
            PSsVM.Add(new Pump_Station()
            {
                ID_station = 1,
                ID=2,
                   
            });

        }

       

      
        public void AddPSVM(string PSVMName)
        {
            //MainVM PSVM = new MainVM { Name = PSVMName };
            //this.PSsVM.Add(PSVM);
        }
        public void RemovePSVM(string PSVMName)
        {
            //PSsVM.Remove(new MainVM() { Name = PSVMName });
        }
        public void ClearPSsVM()
        {
            PSsVM.Clear();
        }

        private ObservableCollection<Pump_Station> PSVMs;
        public ObservableCollection<Pump_Station> PSsVM
        {
            get { return PSVMs; }
            set
            {
                PSVMs = value;
                onpropertychanged("PSsVM");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void onpropertychanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
