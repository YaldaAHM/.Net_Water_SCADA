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
    public class InfoStationsVM : INotifyPropertyChanged
    {
        private InfoStations infoStations;
        public InfoStationsVM()
        {
            infoStations = new InfoStations();
            InfoStationssVM = new ObservableCollection<InfoStations>();
          //  UpDate();
        }
        public InfoStationsVM(List<StationsABFA> stationsABFA)
        {
            try
            {

            
            var OldMain = new InfoStations();
            TabSMWindow tabSMWindow = Application.Current.Windows.OfType<TabSMWindow>().FirstOrDefault();
            InfoStationsVM vm = tabSMWindow.tabsubInfoS.DataContext as InfoStationsVM;
            if (this.InfoStationssVM == null || this.infoStationsVMs.Count == 0)
            {
                infoStationsVMs = new ObservableCollection<InfoStations>();
             
            }

      
            ObservableCollection<InfoStation> newstations= new ObservableCollection<InfoStation>();
            newstations.Add(new InfoStation() {Name= MessageResource.NewStation });
            if (stationsABFA.Any())
            {
                foreach (var st in stationsABFA)
                {
                    newstations.Add(new InfoStation() { Name = st.Name});
                }
            }

            this.InfoStationssVM.Clear();
            this.InfoStationssVM.Add(new InfoStations()
            {
                Stations = newstations,


            });
}
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }




        private ObservableCollection<InfoStations> infoStationsVMs;

        public ObservableCollection<InfoStations> InfoStationssVM
        {
            get { return infoStationsVMs; }
            set
            {
                infoStationsVMs = value;
                onpropertychanged("InfoStationssVM");
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
