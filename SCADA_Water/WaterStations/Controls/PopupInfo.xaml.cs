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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReporterWPF.WaterStations.Controls
{
    /// <summary>
    /// Interaction logic for PopupInfo.xaml
    /// </summary>
    public partial class PopupInfo : UserControl
    {
  //      public object Value
  //      {
  //          get { return (object)GetValue(ValueProperty); }
  //          set { SetValue(ValueProperty, value); }
  //      }
 
  // public static readonly DependencyProperty ValueProperty =
  //    DependencyProperty.Register("Value", typeof(object),
  //      typeof(PopupInfo), new PropertyMetadata(null));


  //      public String Label
  //      {
  //          get { return (String)GetValue(LabelProperty); }
  //          set { SetValue(LabelProperty, value); }
  //      }
 
  //public static readonly DependencyProperty LabelProperty =
  //    DependencyProperty.Register("Label", typeof(string),
  //      typeof(PopupInfo), new PropertyMetadata(""));
        public PopupInfo()
        {
            InitializeComponent();
        }

         private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Popup1.IsOpen = false;
        }
    }
}
