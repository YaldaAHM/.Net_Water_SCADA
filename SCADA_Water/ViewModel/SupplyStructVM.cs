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
   public class SupplyStructVM : INotifyPropertyChanged
    {
        private Supply_Struct supply_Struct;
        private Supply_Struct waterSupply;
        public SupplyStructVM()
        {
            waterSupply = new Supply_Struct();
            SSsVM = new ObservableCollection<Supply_Struct>();
         
            supply_Struct = new Supply_Struct();
            SupplyStructsVM = new ObservableCollection<Supply_Struct>();
            UpDate();
        }

       public void UpDate()
       {

           SupplyStructsVM.Clear();
           SupplyStructsVM.Add(new Supply_Struct()
           {
               Name = "Data not available",
               Supply = new Water_Supply()
               {
                   ID_station = 00,
                   ID = 000
               }

           });
           SSsVM.Clear();
           SSsVM.Add(new Supply_Struct()
           {
               Name = "Data not available",
               Supply = new Water_Supply()
               {
                   ID_station = 00,
                   ID = 000
               }

           });

       }

       public SupplyStructVM(List<Supply_Struct> lps, Supply_Struct ps)
       {
           try
           {
               waterSupply = new Supply_Struct();
               SSsVM = new ObservableCollection<Supply_Struct>();

               supply_Struct = new Supply_Struct();
               SupplyStructsVM = new ObservableCollection<Supply_Struct>();
               // UpDate();
               //   GlobalVariable.ComboBoxSupplyStationG = lps[0];
               SupplyStructsVM.Clear();
               foreach (var p in lps)
               {
                   SupplyStructsVM.Add(p);
               }

               SSsVM.Clear();
               if (GlobalVariable.ComboBoxSupplyStationG == null)
               {
                   SSsVM.Add(lps.FirstOrDefault(x => x.Supply.ID == lps[0].Supply.ID));
                   GlobalVariable.ComboBoxSupplyStationG = lps[0];
               }
               else
               {
                   SSsVM.Add(lps.FirstOrDefault(x => x.Supply.ID == GlobalVariable.ComboBoxSupplyStationG.Supply.ID));
               }
           }
           catch (Exception ex)
           {
               Application.Current.Dispatcher.Invoke((Action) delegate
               {
                   TabPWSWindow tabPWSWindow =
                       Application.Current.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                   WSWindow wSWindow =
                       Application.Current.Windows.OfType<WSWindow>().FirstOrDefault();
                   if (tabPWSWindow != null && tabPWSWindow.tabsubWS != null && wSWindow != null)
                   {
                       wSWindow.TextBlockErrorConnection.Text = MessageResource.ConnectionErrorInline;
                   }
               });
               return;
           }
       }

       public SupplyStructVM( Supply_Struct ps)
        {
            if (this.SupplyStructsVM == null || this.SupplyStructsVM.Count == 0)
            {
                SupplyStructsVM = new ObservableCollection<Supply_Struct>();
            }
            ObservableCollection<Supply_Struct> lps = new ObservableCollection<Supply_Struct>();
            TabPWSWindow tabPWSWindow = Application.Current.Windows.OfType<TabPWSWindow>().FirstOrDefault();
            if (tabPWSWindow != null && tabPWSWindow.tabsubWS != null)
            {
                SupplyStructVM vm = tabPWSWindow.tabsubWS.DataContext as SupplyStructVM;
                var OldSupplyStructsVM = vm.SupplyStructsVM;
                lps = OldSupplyStructsVM;
            }
            waterSupply = new Supply_Struct();
            SSsVM = new ObservableCollection<Supply_Struct>();

            supply_Struct = new Supply_Struct();
            SupplyStructsVM = new ObservableCollection<Supply_Struct>();
            // UpDate();

            SupplyStructsVM.Clear();
            foreach (var p in lps)
            {
                SupplyStructsVM.Add(p);
            }

            SSsVM.Clear();
            SSsVM.Add(lps.FirstOrDefault(x => x.Supply.ID == ps.Supply.ID));

        }
       
        public void AddSupplyStructVM(Supply_Struct ps)
        {
             SSsVM = new ObservableCollection<Supply_Struct>();
      
            this.SSsVM.Add(ps);
           
        }
        public void AddSupplyStructVM(List<Supply_Struct> ps)
        {

           
            SupplyStructsVM = new ObservableCollection<Supply_Struct>();

            foreach (var p in ps)
            {
                SupplyStructsVM.Add(p);
            }


        }
        public void RemoveSupplyStructVM(string PSVMName)
        {
            
        }
        public void ClearSupplyStructVM()
        {
            SupplyStructsVM.Clear();
        }

        private ObservableCollection<Supply_Struct> SupplyStructVMs;
        public ObservableCollection<Supply_Struct> SupplyStructsVM
        {
            get { return SupplyStructVMs; }
            set
            {
                SupplyStructVMs = value;
                onpropertychanged("Supply_StructsVM");
            }
        }






        private ObservableCollection<Supply_Struct> SSVMs;
        public ObservableCollection<Supply_Struct> SSsVM
        {
            get { return SSVMs; }
            set
            {
                SSVMs = value;
                onpropertychanged("SSsVM");
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
