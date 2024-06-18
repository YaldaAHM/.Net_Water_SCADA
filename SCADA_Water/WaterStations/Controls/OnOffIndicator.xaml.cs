using System.Windows;
using System.Windows.Controls;

namespace ReporterWPF.WaterStations.Controls
{
    /// <summary>
    /// Interaction logic for OnOffIndicator.xaml
    /// </summary>
    public partial class OnOffIndicator : UserControl
    {
        public static readonly DependencyProperty IS_ON_PROPERTY = DependencyProperty.Register(
           "IsOn", typeof(bool?), typeof(OnOffIndicator), new PropertyMetadata(false));

        public OnOffIndicator()
        {
            InitializeComponent();
        }

        public bool? IsOn
        {
            get { return (bool?)GetValue(IS_ON_PROPERTY); }
            set { SetValue(IS_ON_PROPERTY, value); }
        }
    }
}
