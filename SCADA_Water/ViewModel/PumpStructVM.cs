using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReporterWPF.Update;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Reporter.Localization.Message;
using ReporterWPF.WaterStations;

namespace ReporterWPF.ViewModel
{
   public class PumpStructVM : INotifyPropertyChanged
    {
        private Pump_Struct pump_Struct;
        private Pump_Station pumpStation;
        public PumpStructVM()
        {
            pumpStation = new Pump_Station();
            PSsVM = new ObservableCollection<Pump_Struct>();
         
            pump_Struct = new Pump_Struct();
            PumpStructsVM = new ObservableCollection<Pump_Struct>();
            UpDate();
        }

        public void UpDate()
        {

            PumpStructsVM.Clear();
            PumpStructsVM.Add(new Pump_Struct()
            {
                Name = "Data not available",
                Pump=new Pump_Station()
                {
                    ID_station=00,
                    ID=000
                }

            });
            PSsVM.Clear();
            PSsVM.Add(new Pump_Struct()
            {
                Name = "Data not available",
                Pump = new Pump_Station()
                {
                    ID_station = 00,
                    ID = 000
                }

            });

        }

       public PumpStructVM(List<Pump_Struct> lps, Pump_Station ps)
       {
           try
           {
               pumpStation = new Pump_Station();
               PSsVM = new ObservableCollection<Pump_Struct>();

               pump_Struct = new Pump_Struct();
               PumpStructsVM = new ObservableCollection<Pump_Struct>();
               // UpDate();
               // GlobalVariable.ComboBoxPumpStationG = lps[0] ;
               PumpStructsVM.Clear();
               foreach (var p in lps)
               {
                   PumpStructsVM.Add(p);
               }
               PSsVM.Clear();
            //   PSsVM.Add(lps.FirstOrDefault(x => x.Pump.ID == ps.ID));
                if (GlobalVariable.ComboBoxPumpStationG == null)
                {
                    PSsVM.Add(lps.FirstOrDefault(x => x.Pump.ID == ps.ID));// lps[0].Pump.ID));
                    GlobalVariable.ComboBoxPumpStationG = lps[0];
                }
                else
                {
                    PSsVM.Add(lps.FirstOrDefault(x => x.Pump.ID == GlobalVariable.ComboBoxPumpStationG.Pump.ID));
                }
                //PSWindow pSWindow = Application.Current.Windows.OfType<PSWindow>().FirstOrDefault();
                //if (pSWindow != null) pSWindow.ComboBoxPumpStation.SelectedValue = PSsVM[0];
            }
           catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    TabPWSWindow tabPWSWindow =
                        Application.Current.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                    PSWindow pSWindow =
                        Application.Current.Windows.OfType<PSWindow>().FirstOrDefault();
                    if (tabPWSWindow != null && tabPWSWindow.tabsubWS != null && pSWindow != null)
                    {
                        pSWindow.TextBlockErrorConnection.Text = MessageResource.ConnectionErrorInline;
                    }
                });
                return;

            }
       }

       public PumpStructVM(Pump_Station ps)
       {
           if (this.PumpStructsVM == null || this.PumpStructsVM.Count == 0)
           {
               PumpStructsVM = new ObservableCollection<Pump_Struct>();
           }
           ObservableCollection<Pump_Struct> lps = new ObservableCollection<Pump_Struct>();
           TabPWSWindow tabPWSWindow = Application.Current.Windows.OfType<TabPWSWindow>().FirstOrDefault();


        
           if (tabPWSWindow != null && tabPWSWindow.tabsubPS != null)
           {
               PumpStructVM vm = tabPWSWindow.tabsubPS.DataContext as PumpStructVM;
               var OldPumpStructsVM = vm.PumpStructsVM;
               lps = OldPumpStructsVM;
           }
        

           pumpStation = new Pump_Station();
           PSsVM = new ObservableCollection<Pump_Struct>();

           pump_Struct = new Pump_Struct();
           PumpStructsVM = new ObservableCollection<Pump_Struct>();

           PumpStructsVM.Clear();
           foreach (var p in lps)
           {
               PumpStructsVM.Add(p);
           }

           PSsVM.Clear();
            if (GlobalVariable.ComboBoxPumpStationG == null)
            {
                PSsVM.Add(lps.FirstOrDefault(x => x.Pump.ID == ps.ID));// lps[0].Pump.ID));
                GlobalVariable.ComboBoxPumpStationG = lps[0];
            }
            else
            {
                PSsVM.Add(lps.FirstOrDefault(x => x.Pump.ID == GlobalVariable.ComboBoxPumpStationG.Pump.ID));
            }
        }

    
        public void AddPumpStructVM(Pump_Struct ps)
        {
             PSsVM = new ObservableCollection<Pump_Struct>();
        
            this.PSsVM.Add(ps);
   
        }
        public void AddPumpStructVM(List<Pump_Struct> ps)
        {

            PumpStructsVM = new ObservableCollection<Pump_Struct>();
            foreach (var p in ps)
            {
                PumpStructsVM.Add(p);
            }


        }
        public void RemovePumpStructVM(string PSVMName)
        {
            //PSsVM.Remove(new MainVM() { Name = PSVMName });
        }
        public void ClearPumpStructVM()
        {
            PumpStructsVM.Clear();
        }

        private ObservableCollection<Pump_Struct> PumpStructVMs;
        public ObservableCollection<Pump_Struct> PumpStructsVM
        {
            get { return PumpStructVMs; }
            set
            {
                PumpStructVMs = value;
                onpropertychanged("Pump_StructsVM");
            }
        }






        private ObservableCollection<Pump_Struct> PSVMs;
        public ObservableCollection<Pump_Struct> PSsVM
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
