using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Reporter.Localization.Message;
using ReporterWPF.WaterStations;

namespace ReporterWPF.Management.VMs
{
    public class SupplyStationsVM : INotifyPropertyChanged
    {
        private SupplyStations supplyStations;

        public SupplyStationsVM()
        {
            supplyStations = new SupplyStations();
            SupplyStationssVM = new ObservableCollection<SupplyStations>();
            //  UpDate();
        }

        public SupplyStationsVM(IQueryable<Node_Mapping> stationsABFA)
        {
            TabSMWindow tabSMWindow = Application.Current.Windows.OfType<TabSMWindow>().FirstOrDefault();
            SupplyStationsVM vm = tabSMWindow.tabsubSS.DataContext as SupplyStationsVM;
            if (this.SupplyStationssVM == null || this.supplyStationsVMs.Count == 0)
            {
                supplyStationsVMs = new ObservableCollection<SupplyStations>();
                //    UpDate();
            }

            ObservableCollection<SupplyStation> newstations = new ObservableCollection<SupplyStation>();
            newstations.Add(new SupplyStation() { Name = MessageResource.NewStation });
            if (stationsABFA.Any())
            {
                foreach (var st in stationsABFA)
                {
                    if (st.IsPumpStation != true)
                        newstations.Add(new SupplyStation() { Name = st.Name });
                }
            }

            this.SupplyStationssVM.Clear();
            this.SupplyStationssVM.Add(new SupplyStations()
            {
                //  Stations = OldMain.Stations,
                SStations = newstations,


            });


        }

        private ObservableCollection<SupplyStations> supplyStationsVMs;

        public ObservableCollection<SupplyStations> SupplyStationssVM
        {
            get { return supplyStationsVMs; }
            set
            {
                supplyStationsVMs = value;
                onpropertychanged("SupplyStationssVM");
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