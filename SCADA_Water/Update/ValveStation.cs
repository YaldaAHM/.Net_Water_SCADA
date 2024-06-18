using Reporter.Database;
using Reporter.Server.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Reporter.Localization.Message;
using ReporterWPF.Utils;
using ReporterWPF.ViewModel;
using ReporterWPF.WaterStations;

namespace ReporterWPF.Update
{
    public class ValveStationUpdate
    {
        private ConnectionCheck connectionCheck = new ConnectionCheck();

        public List<Pump_Struct> Pump_Update(ushort station_id)
        {
             List<Pump_Struct> pump_list = new List<Pump_Struct>();

            try
            {
                using (var db = new ABFAEntities())
                {
                    var q = from u in db.Node_Mapping
                            where (u.ID_Station == station_id && u.IsPumpStation == true
                            && u.ID == 9001

                            )
                            select u;
                    if (q.Any())
                    {
                        pump_list.Clear();
                        foreach (var st in q)
                        {
                            var q1 = from u in db.Pump_Station
                                     where u.ID == st.ID
                                     orderby u.DateTime descending
                                     select u;
                            var qdatasend = from u in db.DataSends
                                            where u.ID == st.ID
                                            orderby u.DateTime descending
                                            select u;
                            if (q1.Any())
                            {
                                pump_list.Add(new Pump_Struct { Name = st.Name, Pump = q1.First(), DataSend = qdatasend.FirstOrDefault() });
                            }
                            else
                                pump_list.Add(new Pump_Struct { Name = st.Name, Pump = new Pump_Station { ID = st.ID }, DataSend = qdatasend.FirstOrDefault() });
                        }
                    }
                }
               
                return pump_list;
            }
            catch (Exception ex)
            {
                Application.Current?.Dispatcher.Invoke((Action)delegate
                {
                    TabPWSWindow tabPWSWindow =
                        Application.Current?.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                    EValveWindow sValveWindow =
                        Application.Current?.Windows.OfType<EValveWindow>().FirstOrDefault();
                    if (tabPWSWindow != null && tabPWSWindow.tabsubVlv != null && sValveWindow != null
                        )
                    {
                        sValveWindow.TextBlockErrorConnection.Text = MessageResource.ConnectionErrorInline;
                    }
                });
                Logger.SERVER.Error("Login error: " + ex.Message + "\n\n" + ex.StackTrace);
                 return null; // pump_list; 
            }
        }

        

        public async void UpdateEValveWindow()
        {
            try
            {

                var pst = await Task.Run(() => Pump_Update(Convert.ToUInt16(GlobalVariable.StationIdCurrent)));


                Application.Current?.Dispatcher.Invoke((Action)delegate
                {
                    TabPWSWindow tabPWSWindow =
                        Application.Current?.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                    EValveWindow sValveWindow =
                        Application.Current?.Windows.OfType<EValveWindow>().FirstOrDefault();
                    if (tabPWSWindow != null && tabPWSWindow.tabsubVlv != null && sValveWindow != null &&
                        sValveWindow.ComboBoxValveStation.SelectedValue != null && GlobalVariable.ComboBoxValveStationG.Pump != null)
                    {
                        sValveWindow.TextBlockErrorConnection.Text = "";
                        tabPWSWindow.tabsubVlv.DataContext = new ValveStructVM(pst,
                            GlobalVariable.ComboBoxValveStationG.Pump);
                        tabPWSWindow.tabsubVlv.DataContext = new ValveStructVM(pst, comboBoxPumpStationSelected.Pump);
                    }
                });
            }
            catch (Exception ex)
            {
                Application.Current?.Dispatcher.Invoke((Action)delegate
                {
                    TabPWSWindow tabPWSWindow =
                        Application.Current?.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                    EValveWindow sValveWindow =
                        Application.Current?.Windows.OfType<EValveWindow>().FirstOrDefault();
                    if (tabPWSWindow != null && tabPWSWindow.tabsubVlv != null && sValveWindow != null
                        )
                    {
                        sValveWindow.TextBlockErrorConnection.Text = MessageResource.ConnectionErrorInline;
                    }
                });
                return;
            }
        }

        private readonly HttpClient _httpClient = new HttpClient();


        public async Task<int> GetDotNetCountAsync()
        {
            // Suspends GetDotNetCountAsync() to allow the caller (the web server)
            // to accept another request, rather than blocking on this one.
            var html = await _httpClient.GetStringAsync("http://dotnetfoundation.org");

            return Regex.Matches(html, @"\.NET").Count;
        }

        public async Task InitialEValveWindow()
        {
            try
            {
               
                var pst = await Task.Run(() => Pump_Update(Convert.ToUInt16(GlobalVariable.StationIdCurrent)));
                

                Application.Current?.Dispatcher.Invoke((Action)delegate
                {
                    TabPWSWindow tabPWSWindow =
                        Application.Current?.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                    EValveWindow sValveWindow =
                        Application.Current?.Windows.OfType<EValveWindow>().FirstOrDefault();
     

                    if (tabPWSWindow != null && tabPWSWindow.tabsubVlv != null && sValveWindow != null && pst != null && pst.Count != 0 && pst[0] != null)
                    {
                       
                        sValveWindow.TextBlockErrorConnection.Text = "";
                        tabPWSWindow.tabsubVlv.DataContext = new ValveStructVM(pst, pst[0].Pump);
                    }
                });
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    TabPWSWindow tabPWSWindow =
                        Application.Current?.Windows.OfType<TabPWSWindow>().FirstOrDefault();
                    EValveWindow sValveWindow =
                        Application.Current?.Windows.OfType<EValveWindow>().FirstOrDefault();
                    if (tabPWSWindow != null && tabPWSWindow.tabsubVlv != null && sValveWindow != null
                        )
                    {
                        sValveWindow.TextBlockErrorConnection.Text = MessageResource.ConnectionErrorInline;
                    }
                });
                return;
            }

        }


        
    }
}
