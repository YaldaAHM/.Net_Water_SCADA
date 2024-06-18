using System.Windows;
using System.Windows.Controls;

namespace ReporterWPF.WaterStations.Controls
{
    /// <summary>
    /// Interaction logic for RatingWifi.xaml
    /// </summary>
    public partial class RatingWifiGSM : UserControl
    {
        public RatingWifiGSM()
        {
            InitializeComponent();
           // DataContext = this;
        }

        public int RatingGSMValue
        {
            get { return (int)GetValue(RatingGSMValueProperty); }
            set { SetValue(RatingGSMValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RatingValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RatingGSMValueProperty =
            DependencyProperty.Register("RatingGSMValue", typeof(int), typeof(RatingWifiGSM), new UIPropertyMetadata(0));
    }
}
