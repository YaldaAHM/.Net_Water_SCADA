using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GMap.NET;
using ReporterWPF.Update;
using ReporterWPF.ViewModel;
using System.Windows.Automation.Provider;

namespace ReporterWPF.WaterStations
{
    /// <summary>
    /// Interaction logic for ShapStation.xaml
    /// </summary>
    public partial class ShapStation : UserControl
    {
        private UsersABFA C_User;
        private StationsABFA StationsAbfa;
        public ShapStation(UsersABFA c_user, StationsABFA stationsAbfa)
        {
            InitializeComponent();
            C_User = c_user;
            StationsAbfa = stationsAbfa;
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
           MainWindow0 mw = Application.Current.Windows.OfType<MainWindow0>().FirstOrDefault();
            
            StationsWindow stationsWindow = Application.Current.Windows.OfType<StationsWindow>().FirstOrDefault();

                  TabPWSWindow tabPWSWindow = new TabPWSWindow(StationsAbfa, C_User);
            tabPWSWindow.ShowDialog();

         
        }

        

      

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
           // StationPopup.PopupStation.IsOpen = true;
        }


        public static readonly DependencyProperty
         SetTextProperty = DependencyProperty.Register("SetText", typeof(string),
         typeof(ShapStation), new PropertyMetadata("", new PropertyChangedCallback(OnSetTextChanged)));

        public string SetText
        {
            get { return (string)GetValue(SetTextProperty); }
            set { SetValue(SetTextProperty, value); }
        }

        private static void OnSetTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ShapStation UserControl1Control = d as ShapStation;
            UserControl1Control.OnSetTextChanged(e);
        }

        private void OnSetTextChanged(DependencyPropertyChangedEventArgs e)
        {

            StationImage.ToolTip = e.NewValue.ToString();
        }





        public static readonly DependencyProperty
        StationIdProperty = DependencyProperty.Register("StationId", typeof(string),
        typeof(ShapStation), new PropertyMetadata("", new PropertyChangedCallback(OnStationIdChanged)));

        public string StationId
        {
            get { return (string)GetValue(StationIdProperty); }
            set { SetValue(StationIdProperty, value); }
        }

        private static void OnStationIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ShapStation UserControl1Control = d as ShapStation;
            UserControl1Control.OnStationIdChanged(e);
        }

        private void OnStationIdChanged(DependencyPropertyChangedEventArgs e)
        {
           // tbTest.Text = e.NewValue.ToString();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            GlobalVariable.StationIdCurrent = Convert.ToUInt16(StationId);
            GlobalVariable.Ok = 1;
            TabPWSWindow tabPWSWindow = new TabPWSWindow(StationsAbfa, C_User);
            tabPWSWindow.ShowDialog();

        }
    }
}
