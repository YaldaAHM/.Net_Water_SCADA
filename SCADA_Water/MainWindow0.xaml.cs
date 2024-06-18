using ReporterWPF.Utils;
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
using Reporter.Localization.Message;
using ReporterWPF.Management.Users;
using ReporterWPF.WaterStations;

namespace ReporterWPF
{
    /// <summary>
    /// Interaction logic for Window0.xaml
    /// </summary>
    public partial class MainWindow0 : Window
    {
        private UsersABFA C_User;
        public MainWindow0(UsersABFA c_user)
        {
          
               C_User = c_user;
            InitializeComponent();
            MenuItemManagements.IsEnabled = false;
            TabSubStation.IsEnabled = false;

            TabSubStation.IsSelected = true;
            MenuItemStations_Click(null, null);
            MenuItemManagements.IsEnabled = (C_User.Role < 40);
           
            TabSubUsersManagement.IsEnabled= (C_User.Role < 40);
            MenuItemStationsManagement.IsEnabled = (C_User.Role < 10);
            TabSubManagement.IsEnabled = (C_User.Role < 10);
            TabSubStation.IsEnabled = (C_User.Role < 10);
        }
        public void MenuItemStations_Click(object sender, RoutedEventArgs e)
        {
            TabSubManagement.IsSelected = false;
            TabSubUsersManagement.IsSelected = false;
            TabSubStation.IsSelected = true;
            TabSubSimCardManagement.IsSelected = false;



            MenuItemStations.IsChecked = true;
            MenuItemManagements.IsChecked = false;


            object content = null;
             MainWindow0 mainWindow00 = Application.Current.Windows.OfType<MainWindow0>().FirstOrDefault();

    


            StationsWindow st = new StationsWindow(C_User);
           
            content = st.Content;

            tab1.Visibility = Visibility.Visible;
            TabSubStation.Header = "Complexes";

            TabSubStation.Content = content;
            TabSMWindow tabSMWindow = Application.Current.Windows.OfType<TabSMWindow>().FirstOrDefault();
            if (tabSMWindow != null)
            {
                tabSMWindow.Close();
            }

        }




        private void DataWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
            var messageBoxResult = WpfMessageBox.Show
                (MessageResource.Message, MessageResource.CloseSoftWare,
                    MessageBoxButton.YesNo, Utils.MessageBoxImage.Warning);

            if (messageBoxResult != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;


               Application.Current.Shutdown();
            }
            



        }

        private void MenuItemStationsManagementn_Click(object sender, RoutedEventArgs e)
        {
           var f = TabSubManagement.IsEnabled;
            TabSubStation.IsSelected = false;
            TabSubUsersManagement.IsSelected = false;
            TabSubManagement.IsSelected = true;
            TabSubSimCardManagement.IsSelected = false;

            MenuItemStations.IsChecked = false;
            MenuItemManagements.IsChecked = true;

            object content = null;
            MainWindow0 mainWindow00 = Application.Current.Windows.OfType<MainWindow0>().FirstOrDefault();

        


            TabSMWindow st = new TabSMWindow(C_User);

            content = st.Content;

            tab1.Visibility = Visibility.Visible;
            TabSubManagement.Header = "Management";

            TabSubManagement.Content = content;
        }

        private void MenuItemUsersManagementn_Click(object sender, RoutedEventArgs e)
        {
            TabSubStation.IsSelected = false;
            TabSubUsersManagement.IsSelected = true;
            TabSubManagement.IsSelected =false ;
            TabSubSimCardManagement.IsSelected = false;
            MenuItemStations.IsChecked = false;
            MenuItemManagements.IsChecked = true;

            object content = null;
            MainWindow0 mainWindow00 = Application.Current.Windows.OfType<MainWindow0>().FirstOrDefault();

         

            AddUser st = new AddUser(C_User,ReporterWPF.Management.GlobalVariabe.Station_list);

            content = st.Content;

            tab1.Visibility = Visibility.Visible;
            TabSubUsersManagement.Header = "Management";

            TabSubUsersManagement.Content = content;
        }

        private void MenuItemSIMCardManagementn_Click(object sender, RoutedEventArgs e)
        {
            var f = TabSubManagement.IsEnabled;
            TabSubStation.IsSelected = false;
            TabSubUsersManagement.IsSelected = false;
            TabSubManagement.IsSelected = false;
            TabSubSimCardManagement.IsSelected = true;

            MenuItemStations.IsChecked = false;
            MenuItemManagements.IsChecked = true;

            object content = null;
            MainWindow0 mainWindow00 = Application.Current.Windows.OfType<MainWindow0>().FirstOrDefault();



            TabSMWindow st = new TabSMWindow(C_User);

            content = st.Content;

            tab1.Visibility = Visibility.Visible;
            TabSubManagement.Header = "Management";

            TabSubManagement.Content = content;
        }
    }
}

