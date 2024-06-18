using ReporterWPF.WaterStations;
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

namespace ReporterWPF.Management.VMs
{
    public class PumpStationsVM : INotifyPropertyChanged
    {
        private PumpStations pumpStations;

        public PumpStationsVM()
        {
            pumpStations = new PumpStations();
            if(PumpStationssVM==null)
            PumpStationssVM = new ObservableCollection<PumpStations>();
            //  UpDate();
        }

        public PumpStationsVM(IQueryable<Node_Mapping> stationsABFA)
        {
            var OldMain = new PumpStations();
            TabSMWindow tabSMWindow = Application.Current.Windows.OfType<TabSMWindow>().FirstOrDefault();
            PumpStationsVM vm = tabSMWindow.tabsubPS.DataContext as PumpStationsVM;
            if (this.PumpStationssVM == null || this.pumpStationsVMs.Count == 0)
            {
                pumpStationsVMs = new ObservableCollection<PumpStations>();
               
            }

            ObservableCollection<PumpStation> newstations = new ObservableCollection<PumpStation>();
            newstations.Add(new PumpStation() {Name= MessageResource.NewStation });
            if (stationsABFA.Any())
            {
                foreach (var st in stationsABFA)
                {
                    if (st.IsPumpStation == true)
                        newstations.Add(new PumpStation() { Name = st.Name});
                }
            }

            this.PumpStationssVM.Clear();
            this.PumpStationssVM.Add(new PumpStations()
            {
          
                PStations = newstations,


            });


        }


        private ObservableCollection<PumpStations> pumpStationsVMs;

        public ObservableCollection<PumpStations> PumpStationssVM
        {
            get { return pumpStationsVMs; }
            set
            {
                pumpStationsVMs = value;
                onpropertychanged("PumpStationssVM");
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