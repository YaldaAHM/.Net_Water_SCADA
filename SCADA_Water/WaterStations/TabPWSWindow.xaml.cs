using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GMap.NET;
using ReporterWPF.Update;
using ReporterWPF.ViewModel;

namespace ReporterWPF.WaterStations
{
    /// <summary>
    /// Interaction logic for TabPWSWindow.xaml
    /// </summary>
    public partial class TabPWSWindow : Window
    {
        private UsersABFA C_User=new UsersABFA();
  
        private StationsABFA stationsAbfa=new StationsABFA();
        public TabPWSWindow(StationsABFA stationsAbfa, UsersABFA c_user)
        {     InitializeComponent();
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.8);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.9);
     
            C_User = c_user;
            UserNameTextBlock.Text = C_User.Name;
            StationNameTextBlock.Text = stationsAbfa.Name;
       
            tabsubPS.IsSelected = true;
            MenuItemPumpStations_Click(null, null);
            if (stationsAbfa.ID_Station != 90)
            {
                MenuItemValve.IsEnabled = false;
                MenuItemValve.Background = Brushes.LightGray;
                GlobalVariable.IsValve = false;
            }
           else
            {
                MenuItemValve.IsEnabled = true;
                GlobalVariable.IsValve = true;
            }

        }

        public TabPWSWindow(UsersABFA c_user)
        {
            C_User = c_user;
            InitializeComponent();
          
        }

        public async void MenuItemPumpStations_Click(object sender, RoutedEventArgs e)
        {
         
            tabsub1.IsSelected = false;
            tabsubWS.IsSelected = false;
            tabsubPS.IsSelected = true;
            tabsubVlv.IsSelected = false;


            MenuItemPumpStations.IsChecked = true;
            MenuItemWaterSupply.IsChecked = false;
            MenuItemValve.IsChecked = false;


            if (tabsubPS.Content != null)
            {
             //   pu.UpdatePSWindow();
                return;
            }
            object content = null;
            

            PSWindow ps = new PSWindow(stationsAbfa.ID_Station,C_User);

         //   content = ps.Content;

            tab1.Visibility = Visibility.Visible;
            tabsubPS.Header = "Pumping Station";

            tabsubPS.Content = ps.Content;


            WSWindow ws = new WSWindow(C_User);

            content = ws.Content;
            tabsubWS.Content = content;

            EValveWindow ev = new EValveWindow(stationsAbfa.ID_Station, C_User);

            content = ev.Content;
            tabsubVlv.Content = content;
            
        }

        
        public void MenuItemWaterSupply_Click(object sender, RoutedEventArgs e)
        {
            tabsub1.IsSelected = false;
            tabsubWS.IsSelected = true;
            tabsubPS.IsSelected = false;
            tabsubVlv.IsSelected = false;

            MenuItemPumpStations.IsChecked = false;
            MenuItemWaterSupply.IsChecked = true;
            MenuItemValve.IsChecked = false;

            if (tabsubWS.Content != null)
            {
              
                return;
            }
            object content = null;
           

            WSWindow ws = new WSWindow();

            content = ws.Content;

            tab1.Visibility = Visibility.Visible;
            tabsubWS.Header = "Reservoir";

            tabsubWS.Content = content;
     
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GlobalVariable.Ok = 0;
            GlobalVariable.ComboBoxPumpStationG = null;
            GlobalVariable.ComboBoxSupplyStationG = null;
            GlobalVariable.ComboBoxValveStationG = null;
            (Application.Current as App).Update((Application.Current as App).timeTickFast);
        }

        private async void TabPWSWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItemValve_Click(object sender, RoutedEventArgs e)
        {
            tabsub1.IsSelected = false;
            tabsubWS.IsSelected = false;
            tabsubPS.IsSelected = false;
            tabsubVlv.IsSelected = true;



            MenuItemPumpStations.IsChecked = false;
            MenuItemWaterSupply.IsChecked = false;
            MenuItemValve.IsChecked = true;

            if (tabsubWS.Content != null)
            {
                // su.UpdateSSWindow();
                return;
            }
            object content = null;
           

            EValveWindow ev = new EValveWindow();

            content = ev.Content;

            tab1.Visibility = Visibility.Visible;
            tabsubVlv.Header = "Electric Faucet";

            tabsubVlv.Content = content;
        }
    }
}