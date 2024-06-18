using Reporter.Database;
using Reporter.Server.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Reporter.Localization.Message;
using ReporterWPF.Utils;
using ReporterWPF.ViewModel;
using ReporterWPF.WaterStations;

namespace ReporterWPF.Update
{
    public class SupplyStationUpdate
    {
        ConnectionCheck connectionCheck = new ConnectionCheck();

        public List<Supply_Struct> Supply_Update(ushort station_id)
        {
            List<Supply_Struct> supply_list = new List<Supply_Struct>();
            try
            {
                using (var db = new ABFAEntities())
                {
                    var q = from u in db.Node_Mapping
                            where (u.ID_Station == station_id && u.IsPumpStation == false)
                            select u;
                    if (q.Any())
                    {
                        supply_list.Clear();
                        foreach (var st in q)
                        {
                            var q1 = from u in db.Water_Supply
                                     where u.ID == st.ID
                                     orderby u.DateTime descending
                                     select u;
                            if (q1.Any())
                            {
                                supply_list.Add(new Supply_Struct { Name = st.Name, Supply = q1.First() });
                            }
                            else
                            {
                                supply_list.Add(new Supply_Struct
                                {
                                    Name = st.Name,
                                    Supply = new Water_Supply { ID = st.ID },

                                });
                            }

                        }
                    }
                }


                return supply_list;
                // return Task.CurrentId;
            }
            catch (Exception ex)
            {
                Application.Current?.Dispatcher.Invoke((Action)delegate
                {
                    TabPWSWindow tabPWSWindow =
                        Application.Current?.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                    WSWindow wSWindow =
                        Application.Current?.Windows.OfType<WSWindow>().FirstOrDefault();
                    if (tabPWSWindow != null && tabPWSWindow.tabsubWS != null && wSWindow != null)
                    {
                        wSWindow.TextBlockErrorConnection.Text = MessageResource.ConnectionErrorInline;
                    }
                });
                Logger.SERVER.Error("Login error: " + ex.Message + "\n\n" + ex.StackTrace);
                return null;// supply_list; 
            }
        }


        public async void UpdateSSWindow()
        {
            try
            {


                var sst = await Task.Run(() => Supply_Update(Convert.ToUInt16(GlobalVariable.StationIdCurrent)));
                Application.Current?.Dispatcher.Invoke((Action)delegate
            {
                TabPWSWindow tabPWSWindow =
                    Application.Current?.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                WSWindow wSWindow =
                    Application.Current?.Windows.OfType<WSWindow>().FirstOrDefault();
                if (tabPWSWindow != null && tabPWSWindow.tabsubWS != null && wSWindow != null
                && GlobalVariable.ComboBoxSupplyStationG != null)
                {

                    if (wSWindow.ComboBoxSupplyStation.SelectedValue == null)
                        wSWindow.ComboBoxSupplyStation.SelectedValue = GlobalVariable.ComboBoxSupplyStationG;
                    wSWindow.TextBlockErrorConnection.Text = "";
                    tabPWSWindow.tabsubWS.DataContext = new SupplyStructVM(sst, GlobalVariable.ComboBoxSupplyStationG);
                }
            });
            }
            catch (Exception ex)
            {
                Application.Current?.Dispatcher.Invoke((Action)delegate
                {
                    TabPWSWindow tabPWSWindow =
                        Application.Current?.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                    WSWindow wSWindow =
                        Application.Current?.Windows.OfType<WSWindow>().FirstOrDefault();
                    if (tabPWSWindow != null && tabPWSWindow.tabsubWS != null && wSWindow != null)
                    {
                        wSWindow.TextBlockErrorConnection.Text = MessageResource.ConnectionErrorInline;
                    }
                });
                return;
            }
        }
        public async Task InitialSSWindow()
        {
            try
            {


                var sst = await Task.Run(() => Supply_Update(Convert.ToUInt16(GlobalVariable.StationIdCurrent)));
                Application.Current?.Dispatcher.Invoke(delegate
               {
                   TabPWSWindow tabPWSWindow =
                       Application.Current?.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                   var wSWindow =
                       Application.Current?.Windows.OfType<WSWindow>().FirstOrDefault();
                   if (tabPWSWindow != null && tabPWSWindow.tabsubWS != null && wSWindow != null && sst != null && sst.Count != 0 && sst[0] != null)
                   {
                       wSWindow.TextBlockErrorConnection.Text = "";
                       tabPWSWindow.tabsubWS.DataContext = new SupplyStructVM(sst, sst[0]);
                   }
               });
            }
            catch (Exception ex)
            {
                Application.Current?.Dispatcher.Invoke((Action)delegate
                {
                    TabPWSWindow tabPWSWindow =
                        Application.Current?.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                    WSWindow wSWindow =
                        Application.Current?.Windows.OfType<WSWindow>().FirstOrDefault();
                    if (tabPWSWindow != null && tabPWSWindow.tabsubWS != null && wSWindow != null)
                    {
                        wSWindow.TextBlockErrorConnection.Text = MessageResource.ConnectionErrorInline;
                    }
                });
                return;
            }
        }
    }
    public class Supply_Struct
    {
        public string Name { get; set; }
        public Water_Supply Supply { get; set; }
    }




    public class SupplyStationVM : INotifyPropertyChanged
    {
       private Water_Supply supplyStation;
         public SupplyStationVM()
        {
            supplyStation = new Water_Supply();
            // speedConverter = new SpeedConverter();

            SupplyStationsVM = new ObservableCollection<Water_Supply>();
            UpDate();
        }


    public void UpDate()
        { 
            SupplyStationsVM.Clear();
            SupplyStationsVM.Add(new Water_Supply()
            {
            });
        }
        public void UpDate(Water_Supply ss)
        {
            SupplyStationsVM.Clear();
            SupplyStationsVM.Add(ss);
        }
   
        public void AddSupplyStationVM(string supplyStationVMName)
        {
            //SupplyStation supplyStationVM = new SupplyStation { Name = supplyStationVMName };
            //this.SupplyStationsVM.Add(supplyStationVM);
        }
        public void RemoveSupplyStationVM(string supplyStationVMName)
        {
            //SupplyStationsVM.Remove(new SupplyStation() { Name = supplyStationVMName });
        }
        public void ClearSupplyStationsVM()
        {
            SupplyStationsVM.Clear();
        }

        private ObservableCollection<Water_Supply> supplyStationVMs;
        public ObservableCollection<Water_Supply> SupplyStationsVM
        {
            get { return supplyStationVMs; }
            set
            {
                supplyStationVMs = value;
                onpropertychanged("SupplyStationsVM");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void onpropertychanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler(this, new PropertyChangedEventArgs(name));
        }
    }







    public class Supply_StructVM : INotifyPropertyChanged
    {
        private Supply_Struct supply_Struct;
        public Supply_StructVM()
        {
            supply_Struct = new Supply_Struct();
            // speedConverter = new SpeedConverter();

            Supply_StructsVM = new ObservableCollection<Supply_Struct>();
            UpDate();
        }

        public Supply_StructVM(List<Supply_Struct> ss)
        {
            supply_Struct = new Supply_Struct();
            // speedConverter = new SpeedConverter();

            Supply_StructsVM = new ObservableCollection<Supply_Struct>();
            UpDate(ss);
        }

        public void UpDate()
        {
            Supply_StructsVM.Clear();
            Supply_StructsVM.Add(new Supply_Struct()
            {
                Name = "gggggg",
                Supply = new Water_Supply()
                {
                    ID = 1,
                    ID_station = 1,
                }
            });
        }
        public void UpDate(List<Supply_Struct> ss)
        {
            // Supply_StructsVM.Clear();
            foreach (var p in ss)
            {
                Supply_StructsVM.Add(p);
                break;
            }

        }
        public void AddSupply_StructVM(string supply_StructVMName)
        {
            Supply_StructsVM.Clear();
            Supply_StructsVM.Add(new Supply_Struct()
            {
                Name = "gggggg",
                Supply = new Water_Supply()
                {
                    ID = 1,
                    ID_station = 1,
                }
            });
        }
        public void RemoveSupply_StructVM(string supply_StructVMName)
        {
            //SupplyStationsVM.Remove(new SupplyStation() { Name = supplyStationVMName });
        }
        public void ClearSupply_StructsVM()
        {
            Supply_StructsVM.Clear();
        }

        private ObservableCollection<Supply_Struct> supply_StructVMs;
        public ObservableCollection<Supply_Struct> Supply_StructsVM
        {
            get { return supply_StructVMs; }
            set
            {
                supply_StructVMs = value;
                onpropertychanged("Supply_StructsVM");
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
