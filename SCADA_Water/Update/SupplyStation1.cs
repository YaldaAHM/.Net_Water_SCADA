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
using ReporterWPF.Utils;
using ReporterWPF.ViewModel;
using ReporterWPF.WaterStations;

namespace ReporterWPF.Update
{
    public class SupplyStationUpdate1
    {
        private ConnectionCheck connectionCheck = new ConnectionCheck();
        private List<Supply_Struct> supply_list = new List<Supply_Struct>();

        public async Task Supply_Update(ushort station_id)
        {

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
                                supply_list.Add(new Supply_Struct {Name = st.Name, Supply = q1.First()});
                            }
                            else
                                supply_list.Add(new Supply_Struct {Name = "", Supply = new Water_Supply {ID = st.ID}});
                        }
                    }
                }
                //  return supply_list;// OK


                // MainWindow0 mainWindow00 = Application.Current.Windoss.OfType<MainWindow0>().FirstOrDefault();
                //StationsWindow stationsWindow = Application.Current.Windoss.OfType<StationsWindow>().FirstOrDefault();
                //mainWindow00.tabsub2.DataContext = new Main1VM(GlobalVariable.sp.Devices);
                //  stationsWindow.DataContext = new SupplyStationVM(supply_list);
                //WSPanel wSPanel = Application.Current.Windoss.OfType<WSPanel>().FirstOrDefault();
                ////wSPanel.DataContext = supply_list;
                //wSPanel.DataContext = new Supply_StructVM();
                // wSPanel.DataContext = new Supply_StructVM();
                ////////Supply_StructVM vm = new Supply_StructVM();
                ////////vm = wSPanel.DataContext as Supply_StructVM;
                //////////  ObservableCollection<ListVM> listVM = new ObservableCollection<ListVM>();

                ////////    vm.AddSupply_StructVM("jhgb");

                // return supply_list;
                // return Task.CurrentId;
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
                        wSWindow.TextBlockErrorConnection.Text = "خطا! از اتصال به شبکه مطمئن شوید...";
                    }
                });
                Logger.SERVER.Error("Login error: " + ex.Message + "\n\n" + ex.StackTrace);
                //MessageBox.Show(".دسترسی به سرور امکان‌پذیر نیست. از اتصال به شبکه مطمئن شوید");
                //  return null;// supply_list; 
            }
        }


        public async void UpdateSSWindow()
        {
            try
            {


                //    if (!connectionCheck.PingTest()) return;
                //  var pumList= Pump_Update(stationId);
                await Supply_Update(Convert.ToUInt16(GlobalVariable.StationIdCurrent));
                var sst = supply_list;
                //  if (!connectionCheck.PingTest()) return;
                Application.Current.Dispatcher.Invoke((Action) delegate
                {
                    TabPWSWindow tabPWSWindow =
                        Application.Current.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                    WSWindow wSWindow =
                        Application.Current.Windows.OfType<WSWindow>().FirstOrDefault();
                    if (tabPWSWindow != null && tabPWSWindow.tabsubWS != null && wSWindow != null)
                    {
                        //Supply_Struct comboBoxSupplyStationSelected =
                        //    (Supply_Struct) (wSWindow.ComboBoxSupplyStation.SelectedValue);
                        //t/**/abPWSWindow.tabsubWS.DataContext = new SupplyStructVM(sst, comboBoxSupplyStationSelected);
                        //    if (!connectionCheck.PingTest()) return;
                        wSWindow.TextBlockErrorConnection.Text = "";
                        tabPWSWindow.tabsubWS.DataContext = new SupplyStructVM(sst,
                            GlobalVariable.ComboBoxSupplyStationG);
                    }
                });
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
                        wSWindow.TextBlockErrorConnection.Text = "خطا! از اتصال به شبکه مطمئن شوید...";
                    }
                });
                return;
            }
        }

        public async Task InitialSSWindow()
        {
            try
            {

                //  var pumList= Pump_Update(stationId);
                //   if (!connectionCheck.PingTest()) return;
                await Supply_Update(Convert.ToUInt16(GlobalVariable.StationIdCurrent));
                var sst = supply_list;
                if (!connectionCheck.PingTest()) return;
                Application.Current.Dispatcher.Invoke((Action) delegate
                {
                    TabPWSWindow tabPWSWindow =
                        Application.Current.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                    WSWindow wSWindow =
                        Application.Current.Windows.OfType<WSWindow>().FirstOrDefault();
                    if (tabPWSWindow != null && tabPWSWindow.tabsubWS != null && wSWindow != null && sst[0] != null)
                    {
                        wSWindow.TextBlockErrorConnection.Text = "";
                        tabPWSWindow.tabsubWS.DataContext = new SupplyStructVM(sst, sst[0]);
                    }
                });
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
                        wSWindow.TextBlockErrorConnection.Text = "خطا! از اتصال به شبکه مطمئن شوید...";
                    }
                });
                return;
            }
        }
    }

    public class Supply_Struct1
    {
        public string Name { get; set; }
        public Water_Supply Supply { get; set; }
    }




}
