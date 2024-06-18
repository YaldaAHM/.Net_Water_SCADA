using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
using DevExpress.Data.XtraReports.Wizard;
using DevExpress.Xpf.Charts;
using Reporter.Localization.Message;
using ReporterWPF.Utils;

namespace ReporterWPF.WaterStations.Report
{
    /// <summary>
    /// Interaction logic for ReportSelectionsControl.xaml
    /// </summary>
    public partial class ReportSelectionsControl : UserControl
    {
        private DateConverter dateConverter;
        private string titleSeries = "";
        private string SelectedSeries = "";
      public  DateTime dateTime1 = DateTime.Now;
      public  DateTime dateTime2 = DateTime.Now;

        public ReportSelectionsControl()
        {
            InitializeComponent();
            dateConverter = new DateConverter();
        }


        private DateTime _LastOnManipulationDelta;


        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
         
            
        }

        private static T GetFrameworkElementByName<T>(FrameworkElement referenceElement) where T : FrameworkElement

        {

            FrameworkElement child = null;

            for (Int32 i = 0; i < VisualTreeHelper.GetChildrenCount(referenceElement); i++)

            {

                child = VisualTreeHelper.GetChild(referenceElement, i) as FrameworkElement;

                System.Diagnostics.Debug.WriteLine(child);

                if (child != null && child.GetType() == typeof(T))

                { break; }

                else if (child != null)

                {

                    child = GetFrameworkElementByName<T>(child);

                    if (child != null && child.GetType() == typeof(T))

                    {

                        break;

                    }

                }

            }

            return child as T;

        }




        private void CheckBoxPumpParameter_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        public List<Control> AllChildren(DependencyObject parent)
        {
            var _List = new List<Control> {};
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);
                if (_Child is Control)
                    _List.Add(_Child as Control);
                _List.AddRange(AllChildren(_Child));
            }
            return _List;
        }

        


        private void BtnReportShow_OnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = this.IsVisible ? Visibility.Hidden : Visibility.Visible;
        }

        public bool ValidateDate()
        {
            bool istrue = true;
            try
            {

                dateTime1 = CultureInfo.DefaultThreadCurrentUICulture.Equals(new CultureInfo("fr")) ?
                     dateConverter.ToGeorgianDateTime(displayDateDatePicker1.Text) : displayDateDatePicker3.SelectedDate.Value;
                dateTime2 = CultureInfo.DefaultThreadCurrentUICulture.Equals(new CultureInfo("fr")) ?
                        dateConverter.ToGeorgianDateTime(displayDateDatePicker2.Text) : displayDateDatePicker4.SelectedDate.Value;

                dateTime2 = dateTime2.AddDays(1);
            }
            catch (Exception ex)
            {
                istrue = false;
                WpfMessageBox.Show
                    (MessageResource.Message, MessageResource.WarningDate,
                        MessageBoxButton.OK, Utils.MessageBoxImage.Warning);
                return false;
            }
            if (dateTime2 < dateTime1)
            {
                istrue = false;
                WpfMessageBox.Show
                    (MessageResource.Message, MessageResource.WarningDate,
                        MessageBoxButton.OK, Utils.MessageBoxImage.Warning);
                return false;
            }
            if (dateTime1.Year <= 2015)
            {
                istrue = false;
                WpfMessageBox.Show
                    (MessageResource.Message, MessageResource.WarningRangeDate,
                        MessageBoxButton.OK, Utils.MessageBoxImage.Warning);
                return false;
            }
            return istrue;
        }

        private void BtnReportDemand_Click(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
