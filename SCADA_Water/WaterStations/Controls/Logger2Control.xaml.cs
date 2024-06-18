using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReporterWPF.WaterStations.Controls
{
    /// <summary>
    /// Interaction logic for HumidityWindow.xaml
    /// </summary>
    public partial class Logger2Control : UserControl
    {
        public Logger2Control()
        {
            InitializeComponent();
        }

       
        private void PopupOpen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PopupInfo.Popup1.IsOpen = true;

        }

        public static readonly DependencyProperty
        TypeRateProperty = DependencyProperty.Register("TypeRate", typeof(string),
        typeof(Logger2Control), new PropertyMetadata("", new PropertyChangedCallback(OnTypeRateChanged)));

        public string TypeRate
        {
            get { return (string)GetValue(TypeRateProperty); }
            set { SetValue(TypeRateProperty, value); }
        }

        private static void OnTypeRateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Logger2Control UserControl1Control = d as Logger2Control;
            UserControl1Control.OnTypeRateChanged(e);
        }

        private void OnTypeRateChanged(DependencyPropertyChangedEventArgs e)
        {
            TBTypeRate.Text = e.NewValue.ToString();
        }
    }
}
