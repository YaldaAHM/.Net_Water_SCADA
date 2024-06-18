using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Core.ConditionalFormattingManager;
using Microsoft.Win32;
//using ReporterWPF.Resoures.FlowDirection;
using ReporterWPF.Update;
using ReporterWPF.Utils;
using ReporterWPF.ViewModel;
using ReporterWPF.WaterStations.Enums;
using ReporterWPF.WaterStations.Report;
using Reporter.Localization.Message;

namespace ReporterWPF.WaterStations
{
    /// <summary>
    /// Interaction logic for WaterSupplyWindow.xaml
    /// </summary>
    public partial class WSWindow : Window
    {
        List<GdpSeriesInput> gdps = new List<GdpSeriesInput>();
        private UsersABFA C_User;
        public WSWindow()
        {
            InitializeComponent();
            ReportSelectionsControl2.listBoxPumpParameter.ItemsSource = new SupplyParameterVM();
            ReportSelectionsControl2.BtnReport.Click += new RoutedEventHandler(BtnReportClick);
            ReportSelectionsControl2.BtnReportShow.Click += new RoutedEventHandler(BtnReportShowClick);
            ReportControl2.CloseIcon.PreviewMouseDown += new MouseButtonEventHandler(CloseIcon);

            ReportSelectionsControl2.Excel1Button.Click += new RoutedEventHandler(Excel1Button_Click);

        }
        public WSWindow(UsersABFA c_user)
        {
            C_User = c_user;
            InitializeComponent();
            ReportSelectionsControl2.listBoxPumpParameter.ItemsSource = new SupplyParameterVM();
            ReportSelectionsControl2.BtnReport.Click += new RoutedEventHandler(BtnReportClick);
            ReportSelectionsControl2.BtnReportShow.Click += new RoutedEventHandler(BtnReportShowClick);
            ReportControl2.CloseIcon.PreviewMouseDown += new MouseButtonEventHandler(CloseIcon);
            ReportSelectionsControl2.Excel1Button.Click += new RoutedEventHandler(Excel1Button_Click);



        }
        private void CloseIcon(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            ReportControl2.Visibility = Visibility.Hidden;
        }

        private void BtnReportShowClick(object sender, RoutedEventArgs routedEventArgs)
        {
            ReportControl2.Visibility = ReportControl2.IsVisible ? Visibility.Hidden : Visibility.Visible;
        }

        private void BtnReportClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (ComboBoxSupplyStation.SelectedValue == null)
                return;
            if (!ReportSelectionsControl2.ValidateDate())
            {
                return;
            }
            var _listBox = ReportSelectionsControl2.listBoxPumpParameter as ListBox;


            var Tstart = ReportSelectionsControl2.dateTime1;
            var Tstop = ReportSelectionsControl2.dateTime2;
            gdps.Clear();

            Supply_Struct comboBoxSupplyStationSelected = (Supply_Struct)(ComboBoxSupplyStation.SelectedValue);
            if (comboBoxSupplyStationSelected == null && comboBoxSupplyStationSelected.Supply == null)
                return;
            ushort id = (ushort)comboBoxSupplyStationSelected.Supply.ID;

            var st_name = comboBoxSupplyStationSelected.Name;


            CheckBox _CheckBoxControl = new CheckBox();
            List<string> checkBoxListItems = new List<string>();
            foreach (var li in _listBox.Items)
            {
                var _Container = _listBox.ItemContainerGenerator
                    .ContainerFromItem(li);
                var _Children = AllChildren(_Container);
                var _Name = "CheckBoxPumpParameter";
                _CheckBoxControl = (CheckBox)_Children
                    .First(c => c.Name == _Name);
                checkBoxListItems.Add(_CheckBoxControl.Content.ToString());
                if (_CheckBoxControl.IsChecked == true)
                {
                    gdps.Add(new GdpSeriesInput()
                    {
                        ItemName = _CheckBoxControl.Content.ToString(),
                        Values = new List<GdpInput>(),
                    });
                }
            }


            ReportControl2.AxisX2D1.Range = new AxisRange();
            ReportControl2.AxisX2D1.Range.SetAuto();
            ReportControl2.AxisY2D1.Range = new AxisRange();
            ReportControl2.AxisY2D1.Range.SetAuto();

            ReportControl2.AxisY2D1.ActualWholeRange.SetValue(AxisY2D.AlwaysShowZeroLevelProperty, false);
            try
            {
                using (var db = new ABFAEntities())
                {
                    var q = from u in db.Water_Supply
                            where (u.ID == id && u.DateTime >= Tstart && u.DateTime < Tstop)
                            orderby u.DateTime ascending
                            select u;

                    if (q.Any())
                    {
                        foreach (var st in q)
                        {
                            gdps.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)SupplyParameter.DeviceStatuse])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = ((st.Logger & 1 << 0) == (1 << 0)) ? 1 : 0,
                                });

                            gdps.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)SupplyParameter.FlotterStatus])
                                ?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = ((st.Logger & 1 << 1) == (1 << 1)) ? 1 : 0
                                });

                            gdps.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)SupplyParameter.RFStatus])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = st.RF
                                });
                            gdps.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)SupplyParameter.VIn])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = (float)Math.Round(st.Voltage_Panel)
                                });
                            gdps.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)SupplyParameter.VBatt])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = (float)Math.Round(st.Voltage_Batt)
                                });
                            gdps.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)SupplyParameter.SupplyLevel])?
                                .Values.Add(new GdpInput() { Date = st.DateTime.Value, Value = (float)Math.Round(st.Level_water) });
                            gdps.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)SupplyParameter.Temp])
                                ?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = (float)Math.Round(st.TEMP)
                                });

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(MessageResource.Message, MessageResource.ConnectionError);


                return;
            }
            if (gdps.Count > 0 && gdps.Count(x => x.Values.Count > 0) > 0)
            {
                ReportControl2.Visibility = Visibility.Visible;
                var gdpsReport = new ChartViewModel(gdps);
                ReportControl2.DataContext = gdpsReport;
            }
            else
                WpfMessageBox.Show(MessageResource.Message, MessageResource.NoData);

            return;






        }

        private void ComboBoxWaterStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxSupplyStation.SelectedValue == null)
                return;
            Supply_Struct comboBoxSupplyStationSelected = (Supply_Struct)(ComboBoxSupplyStation.SelectedValue);
            GlobalVariable.ComboBoxSupplyStationG = comboBoxSupplyStationSelected;
            TabPWSWindow tabPWSWindow = Application.Current.Windows.OfType<TabPWSWindow>().FirstOrDefault();
            tabPWSWindow.tabsubWS.DataContext = new SupplyStructVM(comboBoxSupplyStationSelected);
        }


        public List<Control> AllChildren(DependencyObject parent)
        {
            var _List = new List<Control> { };
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);
                if (_Child is Control)
                    _List.Add(_Child as Control);
                _List.AddRange(AllChildren(_Child));
            }
            return _List;
        }
        private void refresh_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SupplyStationUpdate supplyStationUpdate = new SupplyStationUpdate();
            supplyStationUpdate.UpdateSSWindow();
            PumpStationUpdate pumpStationUpdate = new PumpStationUpdate();
            pumpStationUpdate.UpdatePSWindow();
        }

        private void Excel1Button_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel Worksheets (*.xls)|*.xls";
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                try
                {
                    FExportExcel(dialog.FileName, gdps, 1);
                    WpfMessageBox.Show(MessageResource.Message, MessageResource.OutPutFile);

                }
                catch (Exception ex)
                {
                    // PEnd();
                    WpfMessageBox.Show(MessageResource.Message, MessageResource.ErrorFile + ex.Message);
                }

            }

        }

        public void FExportExcel(string filePath, List<GdpSeriesInput> result, int chartType)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = null;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = null;
            object misValue = System.Reflection.Missing.Value;
            var dt = new PersianDateConverter();
            var dtc = new DateConverter();
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);



                var startCell = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[2, 1];
                var endCell = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[result[0].Values.Count + 2, result.Count + 1];
                var writeRange = xlWorkSheet.Range[startCell, endCell];
                var result2 = new object[result[0].Values.Count + 2, result.Count + 1];
                result2[0, 0] = MessageResource.Supply;
                result2[1, 0] = MessageResource.Time;
                for (int i = 1; i <= result[0].Values.Count; i++)
                {
                    if (i > 1)
                        result2[i, 0] = dtc.ToPersianDateString(result[0].Values[i - 1].Date) as string;
                    for (int j = 1; j < result.Count + 1; j++)
                    {
                        if (i == 1)
                        {
                            result2[i, j] = result[j - 1].ItemName;
                        }
                        else
                        {
                            result2[i, j] = result[j - 1].Values[i - 1].Value;
                        }

                    }

                }


                writeRange.Value2 = result2;
                xlWorkSheet.Columns.AutoFit();

                //Charting...
                Microsoft.Office.Interop.Excel.Range chartRange;
                Microsoft.Office.Interop.Excel.ChartObjects xlCharts = (Microsoft.Office.Interop.Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
                Microsoft.Office.Interop.Excel.ChartObject myChart = (Microsoft.Office.Interop.Excel.ChartObject)xlCharts.Add((result.Count + 1) * 50 + 150, 80, 300, 250);
                Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;

                chartRange = xlWorkSheet.get_Range("A2",
                    ((char)(((int)'A') + ((result.Count + 1) - 1))).ToString() + (result[0].Values.Count + 1));
                chartPage.SetSourceData(chartRange, misValue);

                switch (chartType)
                {
                    case 0:
                        chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlColumnClustered;
                        break;
                    case 1:
                        chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
                        break;
                    case 2:
                        chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlArea;
                        break;
                    case 3:
                        chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlAreaStacked;
                        break;
                    default:
                        break;
                }

                xlWorkBook.SaveAs(filePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            }
            catch (Exception ex)
            {
                //FinishProgress();
                WpfMessageBox.Show(MessageResource.Message, ex.ToString());
            }
            finally
            {
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            }
        }


        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                WpfMessageBox.Show(MessageResource.Message, "Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString =
                      "data source=185.105.121.46;initial catalog=ABFA;user id=paya;password=paya;";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                DateTime date1 = new DateTime(2021, 01, 04, 13, 20, 0);
                int i = 0;
                float beforelevel = 0;
                using (var db = new ABFAEntities())
                {
                    var q = from u in db.Water_Supply
                            where (u.ID == 2101 && u.DateTime > date1)
                            orderby u.DateTime ascending
                            select u;

                    if (q.Any())
                    {
                        foreach (var st in q)
                        {
                            i++;


                            if (i % 2 == 0)
                            {


                                string query = "Update [Water_Supply] SET Level_water=" + beforelevel +
                                               " Where ID=2101 and DateTime=@DateTime";//+ st.DateTime.ToString();

                                SqlCommand myCommand = new SqlCommand(query, connection);
                                myCommand.Parameters.AddWithValue("@DateTime", st.DateTime);
                                myCommand.ExecuteNonQuery();

                            }
                            else
                            {
                                beforelevel = st.Level_water;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                int t = 0;
            }
        }
    }
}
