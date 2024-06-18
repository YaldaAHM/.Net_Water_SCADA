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
   public class ValveStructVM : INotifyPropertyChanged
    {
        private Pump_Struct pump_Struct;
        private Pump_Station pumpStation;
        public ValveStructVM()
        {
            pumpStation = new Pump_Station();
            ValvesVM = new ObservableCollection<Pump_Struct>();
         
            pump_Struct = new Pump_Struct();
            ValveStructsVM = new ObservableCollection<Pump_Struct>();
            UpDate();
        }

        public void UpDate()
        {

            ValveStructsVM.Clear();
            ValveStructsVM.Add(new Pump_Struct()
            {
                Name = "Data not available",
                Pump=new Pump_Station()
                {
                    ID_station=00,
                    ID=000
                }

            });
            ValvesVM.Clear();
            ValvesVM.Add(new Pump_Struct()
            {
                Name = "Data not available",
                Pump = new Pump_Station()
                {
                    ID_station = 00,
                    ID = 000
                }

            });

        }

       public ValveStructVM(List<Pump_Struct> lps, Pump_Station ps)
       {
           try
           {
               pumpStation = new Pump_Station();
               ValvesVM = new ObservableCollection<Pump_Struct>();

               pump_Struct = new Pump_Struct();
               ValveStructsVM = new ObservableCollection<Pump_Struct>();
              ValveStructsVM.Clear();
               foreach (var p in lps)
               {
                   ValveStructsVM.Add(p);
               }
               ValvesVM.Clear();
                if (GlobalVariable.ComboBoxValveStationG == null)
                {
                    ValvesVM.Add(lps.FirstOrDefault(x => x.Pump.ID == ps.ID));// lps[0].Pump.ID));
                    GlobalVariable.ComboBoxValveStationG = lps[0];
                }
                else
                {
                    ValvesVM.Add(lps.FirstOrDefault(x => x.Pump.ID == GlobalVariable.ComboBoxValveStationG.Pump.ID));
                }
                   }
           catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    TabPWSWindow tabPWSWindow =
                        Application.Current.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                    EValveWindow eValveWindow =
                        Application.Current.Windows.OfType<EValveWindow>().FirstOrDefault();
                    if (tabPWSWindow != null && tabPWSWindow.tabsubVlv != null && eValveWindow != null)
                    {
                        eValveWindow.TextBlockErrorConnection.Text = MessageResource.ConnectionErrorInline;
                    }
                });
                return;

            }
       }

       public ValveStructVM(Pump_Station ps)
       {
           if (this.ValveStructsVM == null || this.ValveStructsVM.Count == 0)
           {
               ValveStructsVM = new ObservableCollection<Pump_Struct>();
           }
           ObservableCollection<Pump_Struct> lps = new ObservableCollection<Pump_Struct>();
           TabPWSWindow tabPWSWindow = Application.Current.Windows.OfType<TabPWSWindow>().FirstOrDefault();


       
           if (tabPWSWindow != null && tabPWSWindow.tabsubVlv != null)
           {
               ValveStructVM vm = tabPWSWindow.tabsubVlv.DataContext as ValveStructVM;
               var OldValveStructsVM = vm.ValveStructsVM;
               lps = OldValveStructsVM;
           }
         

           pumpStation = new Pump_Station();
           ValvesVM = new ObservableCollection<Pump_Struct>();

           pump_Struct = new Pump_Struct();
           ValveStructsVM = new ObservableCollection<Pump_Struct>();
           // UpDate();

           ValveStructsVM.Clear();
           foreach (var p in lps)
           {
               ValveStructsVM.Add(p);
           }

           ValvesVM.Clear();
        //   ValvesVM.Add(lps.FirstOrDefault(x => x.Pump.ID == ps.ID));
            if (GlobalVariable.ComboBoxValveStationG == null)
            {
                ValvesVM.Add(lps.FirstOrDefault(x => x.Pump.ID == ps.ID));// lps[0].Pump.ID));
                GlobalVariable.ComboBoxValveStationG = lps[0];
            }
            else
            {
                ValvesVM.Add(lps.FirstOrDefault(x => x.Pump.ID == GlobalVariable.ComboBoxValveStationG.Pump.ID));
            }
        }

      
        public void AddValveStructVM(Pump_Struct ps)
        {
            //ListSectionModel section = new ListSectionModel { Num = num, Id = id, Name = sectionName };
            ValvesVM = new ObservableCollection<Pump_Struct>();
         //   ValvesVM.Clear();
            this.ValvesVM.Add(ps);
        
        }
        public void AddValveStructVM(List<Pump_Struct> ps)
        {

          
            ValveStructsVM = new ObservableCollection<Pump_Struct>();

           // ValveStructsVM.Clear();
            foreach (var p in ps)
            {
                ValveStructsVM.Add(p);
            }


        }
        public void RemoveValveStructVM(string PSVMName)
        {
            //ValvesVM.Remove(new MainVM() { Name = PSVMName });
        }
        public void ClearValveStructVM()
        {
            ValveStructsVM.Clear();
        }

        private ObservableCollection<Pump_Struct> ValveStructVMs;
        public ObservableCollection<Pump_Struct> ValveStructsVM
        {
            get { return ValveStructVMs; }
            set
            {
                ValveStructVMs = value;
                onpropertychanged("ValveStructsVM");
            }
        }






        private ObservableCollection<Pump_Struct> ValveVMs;
        public ObservableCollection<Pump_Struct> ValvesVM
        {
            get { return ValveVMs; }
            set
            {
                ValveVMs = value;
                onpropertychanged("ValvesVM");
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
