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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReporterWPF.WaterStations.Controls
{
    /// <summary>
    /// Interaction logic for PumpingMotor1Control.xaml
    /// </summary>
    public partial class PumpingValveControl : UserControl
    {
        public PumpingValveControl()
        {
            InitializeComponent();
        }
        private void TextBoxUpdateRTU_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxUpdateRTU.Text == "") //if (((Storyboard)Resources["Storyboard"]) != null)
            //    {
            //        ((Storyboard)Resources["Storyboard"]).Begin();

            //        ((Storyboard)Resources["Storyboard"]).SetSpeedRatio(0);
                    return;
                //};
            byte rtu = Convert.ToByte(TextBoxUpdateRTU.Text);
            bool main_cnt = (rtu & 1 << 7) != (1 << 7);
            bool delta_cnt = (rtu & 1 << 3) != (1 << 3);


            if (main_cnt && delta_cnt)
            {
                if (((Storyboard)Resources["Storyboard"]) != null)
                {
                    ((Storyboard)Resources["Storyboard"]).Begin();

                    ((Storyboard)Resources["Storyboard"]).SetSpeedRatio(60);
                }
            }
            else
            {
                if (((Storyboard)Resources["Storyboard"]) != null)
                {
                    ((Storyboard)Resources["Storyboard"]).Begin();

                    ((Storyboard)Resources["Storyboard"]).SetSpeedRatio(0);
                }
            }
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
    }
}
