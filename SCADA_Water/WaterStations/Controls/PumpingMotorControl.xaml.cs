using System;
using System.Windows;
using System.Windows.Controls;
//arn
//using Reporter.Server.Protocols;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace ReporterWPF.WaterStations.Controls
{
    /// <summary>
    /// Interaction logic for PumpingMotor.xaml
    /// </summary>
    public partial class PumpingMotorControl : UserControl
    {
        public static readonly DependencyProperty ON_PROPERTY = DependencyProperty.Register(
            "On", typeof(bool), typeof(PumpingMotorControl), new PropertyMetadata(false));

        public static readonly DependencyProperty HAS_SPEED_PROPERTY = DependencyProperty.Register(
            "HasSpeed", typeof(bool), typeof(PumpingMotorControl), new PropertyMetadata(true));

        public static readonly DependencyProperty SPEED_PROPERTY = DependencyProperty.Register(
            "Speed", typeof(int), typeof(PumpingMotorControl), new PropertyMetadata(0));

        public static readonly DependencyProperty HAS_FAULT_PROPERTY = DependencyProperty.Register(
            "HasFault", typeof(bool), typeof(PumpingMotorControl), new PropertyMetadata(false));

        public static readonly DependencyProperty FAULTS_PROPERTY = DependencyProperty.Register(
            "Faults", typeof(object), typeof(PumpingMotorControl), new PropertyMetadata(null));

        public PumpingMotorControl()
        {
            InitializeComponent();
        }

        //public ControlType RequestType
        //{
        //    get
        //    {
        //        return AutomaticB.IsChecked.GetValueOrDefault()
        //            ? ControlType.Automatic
        //            : ControlType.Manual;
        //    }
        //    set
        //    {
        //        if (value == ControlType.Automatic) AutomaticB.IsChecked = true;
        //        else ManualB.IsChecked = true;
        //    }
        //}

        public bool On
        {
            get { return (bool)GetValue(ON_PROPERTY); }
            set { SetValue(ON_PROPERTY, value); }
        }

        public bool HasSpeed
        {
            get { return (bool)GetValue(HAS_SPEED_PROPERTY); }
            set { SetValue(HAS_SPEED_PROPERTY, value); }
        }

        public int Speed
        {
            get { return (int)GetValue(SPEED_PROPERTY); }
            set { SetValue(SPEED_PROPERTY, value); }
        }

        public bool HasFault
        {
            get { return (bool)GetValue(HAS_FAULT_PROPERTY); }
            set { SetValue(HAS_FAULT_PROPERTY, value); }
        }

        public object Faults
        {
            get { return GetValue(FAULTS_PROPERTY); }
            set { SetValue(FAULTS_PROPERTY, value); }
        }

        //private void SettingsButtonClick(object sender, RoutedEventArgs e)
        //{
        //    SettingsPopup.IsOpen = true;
        //}

        //public event Action<object> OnSettingsChanged;

        private void ToggleButtonChecked(object sender, RoutedEventArgs e)
        {
            //if (SettingsPopup.IsOpen && OnSettingsChanged != null)
            //    OnSettingsChanged(this);
        }
        private void CheckBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("sRotation") as Storyboard;
            Storyboard.SetTarget(sb, this.Cf);
            sb.Begin();
        }

        private void CheckBox1_Checked(object sender, RoutedEventArgs e)
        {
             Storyboard sb = this.FindResource("sRotation") as Storyboard;
            Storyboard.SetTarget(sb, this.Cf);
            sb.Begin();
        }
        //private void ApplySpeedButtonClick(object sender, RoutedEventArgs e)
        //{
        //    if (SettingsPopup.IsOpen && OnSettingsChanged != null)
        //        OnSettingsChanged(this);
        //}

        //private void IndicatorMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (HasFault && Faults != null)
        //    {
        //        FaultPopup.IsOpen = true;
        //    }
        //}
    }
}
