using ReporterWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DevExpress.Xpf.Charts;
using FinancialManagement.Repository;
using FinancialManagement.Model;
using Reporter.Localization.Message;
using ReporterWPF.Update;
using Reporter.Server.Utils;
using ReporterWPF.Utils;
using ReporterWPF.WaterStations.Enums;
using ReporterWPF.WaterStations.Report;
using CheckBox = System.Windows.Controls.CheckBox;
using Control = System.Windows.Controls.Control;
using Excel = Microsoft.Office.Interop.Excel;
using ListBox = System.Windows.Controls.ListBox;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace ReporterWPF.WaterStations
{
    /// <summary>
    /// Interaction logic for PSWindow.xaml
    /// </summary>
    public partial class PSWindow : Window
    {
        List<GdpSeriesInput> gdps = new List<GdpSeriesInput>();
        private UsersABFA C_User;
        private long StationId;

        public PSWindow()
        {
            InitializeComponent();
        }

        public PSWindow(long stationId, UsersABFA c_user)
        {
            C_User = c_user;
            StationId = stationId;
            InitializeComponent();
            SettingExpander.IsEnabled = (C_User.other1 == "1");
            ReportSelectionsControl.listBoxPumpParameter.ItemsSource = new PumpParameterVM();
            ReportSelectionsControl.BtnReport.Click += new RoutedEventHandler(BtnReportClick);
            ReportSelectionsControl.BtnReportShow.Click += new RoutedEventHandler(BtnReportShowClick);
            ReportControl1.CloseIcon.PreviewMouseDown += new MouseButtonEventHandler(CloseIcon);

            ReportSelectionsControl.Excel1Button.Click += new RoutedEventHandler(Excel1Button_Click);


        }

        private void CloseIcon(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            ReportControl1.Visibility = Visibility.Hidden;
        }

        private void BtnReportShowClick(object sender, RoutedEventArgs routedEventArgs)
        {
            ReportControl1.Visibility = ReportControl1.IsVisible ? Visibility.Hidden : Visibility.Visible;
        }

        private void BtnReportClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (ComboBoxPumpStation.SelectedValue == null)
                return;

            if (!ReportSelectionsControl.ValidateDate())
            {
                return;
            }
            var _listBox = ReportSelectionsControl.listBoxPumpParameter as ListBox;

            gdps.Clear();
            var Tstart = ReportSelectionsControl.dateTime1;
            var Tstop = ReportSelectionsControl.dateTime2;


            Pump_Struct comboBoxPumpStationSelected = (Pump_Struct)(ComboBoxPumpStation.SelectedValue);
            if (comboBoxPumpStationSelected == null && comboBoxPumpStationSelected.Pump == null)
                return;
            ushort id = (ushort)comboBoxPumpStationSelected.Pump.ID;

            var st_name = comboBoxPumpStationSelected.Name;


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
            var d = Enum.GetValues(typeof(PumpParameter));
            ReportControl1.AxisX2D1.Range = new AxisRange();
            ReportControl1.AxisX2D1.Range.SetAuto();

            ReportControl1.AxisY2D1.Range = new AxisRange();
            ReportControl1.AxisY2D1.Range.SetAuto();
            ReportControl1.AxisY2D1.ActualWholeRange.SetValue(AxisY2D.AlwaysShowZeroLevelProperty, false);

            try
            {
                using (var db = new ABFAEntities())
                {
                    var q = from u in db.Pump_Station
                            where (u.ID == id && u.DateTime >= Tstart && u.DateTime < Tstop)
                            orderby u.DateTime ascending
                            select u;

                    if (q.Any())
                    {
                        foreach (var st in q)
                        {
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.MotorStatuse])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = ((st.RTU & 1 << 7) != (1 << 7)) ? 1 : 0
                                });

                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.ControlStatus])
                                ?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = ((st.RTU & 1 << 6) != (1 << 6)) ? 0 : 1
                                });
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.PhaseControl])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = ((st.RTU & 1 << 5) != (1 << 5)) ? 1 : 0
                                });
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.Bimeta])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = ((st.RTU & 1 << 4) != (1 << 4)) ? 1 : 0
                                });
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.Fuze])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = ((st.RTU & 1 << 0) != (1 << 0)) ? 1 : 0
                                });
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.RFStatus])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = st.RF
                                });
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.EnergicStatus])
                                ?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = ((st.Motor & 1 << 4) == (1 << 4)) ? 1 : 0
                                });
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.VIn])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = (float)Math.Round(st.Voltage_Input)
                                });
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.VBatt])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = (float)Math.Round(st.Voltage_Batt)
                                });

                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.RTUStatus])
                                ?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = (byte)(st.Motor & 0x07)
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
                ReportControl1.Visibility = Visibility.Visible;
                var gdpsReport = new ChartViewModel(gdps);
                ReportControl1.DataContext = gdpsReport;
            }
            else
                WpfMessageBox.Show(MessageResource.Message, MessageResource.NoData);

            return;

        }
        private void BtnReportDemandClick(object sender, RoutedEventArgs e)
        {
            if (ComboBoxPumpStation.SelectedValue == null)
                return;

            if (!ReportSelectionsControl.ValidateDate())
            {
                return;
            }
            var _listBox = ReportSelectionsControl.listBoxPumpParameter as ListBox;


            gdps.Clear();
            var Tstart = ReportSelectionsControl.dateTime1;
            var Tstop = ReportSelectionsControl.dateTime2;


            Pump_Struct comboBoxPumpStationSelected = (Pump_Struct)(ComboBoxPumpStation.SelectedValue);
            if (comboBoxPumpStationSelected == null && comboBoxPumpStationSelected.Pump == null)
                return;
            ushort id = (ushort)comboBoxPumpStationSelected.Pump.ID;
            var st_name = comboBoxPumpStationSelected.Name;


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
            var d = Enum.GetValues(typeof(PumpParameter));
            ReportControl1.AxisX2D1.Range = new AxisRange();
            ReportControl1.AxisX2D1.Range.SetAuto();

            ReportControl1.AxisY2D1.Range = new AxisRange();
            ReportControl1.AxisY2D1.Range.SetAuto();
            ReportControl1.AxisY2D1.ActualWholeRange.SetValue(AxisY2D.AlwaysShowZeroLevelProperty, false);

            try
            {
                using (var db = new ABFAEntities())
                {
                    var q = from u in db.Pump_Station
                            where (u.ID == id && u.DateTime >= Tstart && u.DateTime < Tstop)
                            orderby u.DateTime ascending
                            select u;

                    if (q.Any())
                    {
                        foreach (var st in q)
                        {
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.MotorStatuse])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = ((st.RTU & 1 << 7) != (1 << 7)) ? 1 : 0
                                });

                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.ControlStatus])
                                ?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = ((st.RTU & 1 << 6) != (1 << 6)) ? 0 : 1
                                });
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.PhaseControl])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = ((st.RTU & 1 << 5) != (1 << 5)) ? 1 : 0
                                });
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.Bimeta])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = ((st.RTU & 1 << 4) != (1 << 4)) ? 1 : 0
                                });
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.Fuze])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = ((st.RTU & 1 << 0) != (1 << 0)) ? 1 : 0
                                });
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.RFStatus])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = st.RF
                                });
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.EnergicStatus])
                                ?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = ((st.Motor & 1 << 4) == (1 << 4)) ? 1 : 0
                                });
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.VIn])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = (float)Math.Round(st.Voltage_Input)
                                });
                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.VBatt])?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = (float)Math.Round(st.Voltage_Batt)
                                });

                            gdps?.FirstOrDefault(x => x.ItemName == checkBoxListItems[(int)PumpParameter.RTUStatus])
                                ?
                                .Values.Add(new GdpInput()
                                {
                                    Date = st.DateTime.Value,
                                    Value = (byte)(st.Motor & 0x07)
                                });

                        }
                    }

                    gdps.Add(new GdpSeriesInput()
                    {
                        ItemName = "demand",
                        Values = new List<GdpInput>(),
                    });
                    DemandRepository demandRepository = new DemandRepository();
                    foreach (var demand in demandRepository.FindDemands("", DateTime.Now, DateTime.Now))
                    {
                        gdps?.FirstOrDefault(x => x.ItemName == "demand")?
                                                        .Values.Add(new GdpInput()
                                                        {
                                                            Date = demand.dateTime,
                                                            Value = demandRepository.ConvertRangeValue(demand.Demand),
                                                        });

                    }
                }

            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(MessageResource.Message, MessageResource.ConnectionError);


                return;
            }

            //   this.UseWaitCursor = false;
            if (gdps.Count > 0 && gdps.Count(x => x.Values.Count > 0) > 0)
            {
                ReportControl1.Visibility = Visibility.Visible;
                var gdpsReport = new ChartViewModel(gdps);
                ReportControl1.DataContext = gdpsReport;
            }
            else
                WpfMessageBox.Show(MessageResource.Message, MessageResource.NoData);

            return;
        }
        private void ComboBoxPumpStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxPumpStation.SelectedValue == null)
                return;
            try
            {
                Pump_Struct comboBoxPumpStationSelected = (Pump_Struct)(ComboBoxPumpStation.SelectedValue);
                GlobalVariable.ComboBoxPumpStationG = comboBoxPumpStationSelected;
                TabPWSWindow tabPWSWindow = System.Windows.Application.Current.Windows.OfType<TabPWSWindow>().FirstOrDefault();


                tabPWSWindow.tabsubPS.DataContext = new PumpStructVM(comboBoxPumpStationSelected.Pump);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void toggleSwitch1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSendSetting_Click(object sender, RoutedEventArgs e)
        {
            Pump_Struct comboBoxPumpStationSelected = (Pump_Struct)(ComboBoxPumpStation.SelectedValue);


            if (comboBoxPumpStationSelected == null)
                return;

            byte DBO0 = 0, DBO1 = 0;
            DBO0 |= (byte)((EnergySsavingCbx.IsChecked.Value) ? 4 : 0);
            DBO0 |= (byte)((StatusMotorTS.IsChecked.Value) ? 2 : 0);
            DBO1 = (byte)(ComboBoxControlMode.SelectedIndex + 1);
            int id = (int)comboBoxPumpStationSelected.Pump.ID;

            try
            {
                using (var db = new ABFAEntities())
                {

                    var q = from u in db.DataSends
                            where (u.ID == id)
                            select u;

                    if (q.Any())
                    {
                        q.First().BO0 = DBO0;
                        q.First().BO1 = DBO1;
                        q.First().BO2 = (byte)(C_User.ID_User >> 8);
                        q.First().BO3 = (byte)(C_User.ID_User >> 0);
                        q.First().DateTime = DateTime.Now;
                        db.SaveChanges();
                        //var ds = q.First();
                        Logger.SERVER.Information("Comman Send: " + DBO0.ToString("X2"));
                    }
                    else
                    {
                        DataSend ds = new DataSend();
                        ds.ID = id;
                        ds.ID_Station = (int)comboBoxPumpStationSelected.Pump.ID_station;
                        ds.BO0 = DBO0;
                        ds.BO1 = DBO1;
                        ds.BO2 = (byte)(C_User.ID_User >> 8);
                        ds.BO3 = (byte)(C_User.ID_User >> 0);

                        ds.DateTime = DateTime.Now;
                        db.DataSends.Add(ds);
                        db.SaveChanges();
                        Logger.SERVER.Information("Comman Send: " + DBO0.ToString("X2"));
                    }
                }

                WpfMessageBox.Show(MessageResource.Message, MessageResource.SuccessSava, MessageBoxButton.OK);
                comboBoxPumpStationSelected.Pump.Motor &= 0xE8;
                comboBoxPumpStationSelected.Pump.Motor |=
                    (byte)(EnergySsavingCbx.IsChecked.Value ? (1 << 4) : (0 << 4));
                comboBoxPumpStationSelected.Pump.Motor |= DBO1;


                PumpStationUpdate pumpStationUpdate = new PumpStationUpdate();
                pumpStationUpdate.UpdatePSWindow();

            }
            catch (Exception ex)
            {
                Logger.SERVER.Error("Command Send1: " + ex.Message + "\n\n" + ex.StackTrace);
                WpfMessageBox.Show(MessageResource.Message, MessageResource.ConnectionError);
                return;
            }
            //this.UseWaitCursor = false;
        }

        private void Button2_OnClick(object sender, RoutedEventArgs e)
        {
            var d = new ChartViewModel();
            ReportControl1.DataContext = d;
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
            PumpStationUpdate pumpStationUpdate = new PumpStationUpdate();
            pumpStationUpdate.UpdatePSWindow();
            SupplyStationUpdate supplyStationUpdate = new SupplyStationUpdate();
            supplyStationUpdate.UpdateSSWindow();
        }



        public Pump_Struct up()
        {
            Pump_Struct comboBoxPumpStationSelected = (Pump_Struct)(ComboBoxPumpStation.SelectedValue);
            return comboBoxPumpStationSelected;
        }

        private void PSWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            SupplyStationUpdate su = new SupplyStationUpdate();
            su.UpdateSSWindow();
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

                }
                catch (Exception ex)
                {

                    WpfMessageBox.Show(MessageResource.Message, MessageResource.ErrorFile + ex.Message);
                }

            }


        }

        public void FExportExcel(string filePath, List<GdpSeriesInput> result, int chartType)
        {
            Excel.Application xlApp = null;
            Excel.Workbook xlWorkBook = null;
            Excel.Worksheet xlWorkSheet = null;
            object misValue = System.Reflection.Missing.Value;
            var dt = new PersianDateConverter();
            var dtc = new DateConverter();
            try
            {
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


                var startCell = (Excel.Range)xlWorkSheet.Cells[2, 1];
                var endCell = (Excel.Range)xlWorkSheet.Cells[result[0].Values.Count + 2, result.Count + 3];
                var writeRange = xlWorkSheet.Range[startCell, endCell];
                var result2 = new object[result[0].Values.Count + 2, result.Count + 3];
                result2[0, 0] = MessageResource.Pump;
                result2[1, 0] = MessageResource.Time;
                for (int i = 1; i <= result[0].Values.Count; i++)
                {
                    if (i > 1)
                    {
                        result2[i, 0] = dtc.ToPersianDateString(result[0].Values[i - 1].Date) as string;
                    }
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

                    if (i > 1)
                    {
                        result2[i, result.Count + 1] = dtc.ToPersianDateString(result[0].Values[i - 1].Date).Substring(0, 10) as string;
                        result2[i, result.Count + 2] = dtc.ToPersianDateString(result[0].Values[i - 1].Date).Substring(10) as string;
                    }

                }


                writeRange.Value2 = result2;
                xlWorkSheet.Columns.AutoFit();

                //Charting...
                Excel.Range chartRange;
                Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
                Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add((result.Count + 1) * 50 + 150, 80, 300, 250);
                Excel.Chart chartPage = myChart.Chart;

                chartRange = xlWorkSheet.get_Range("A2",
                    ((char)(((int)'A') + ((result.Count + 1) - 1))).ToString() + (result[0].Values.Count - 1));
                chartPage.SetSourceData(chartRange, misValue);

                switch (chartType)
                {
                    case 0:
                        chartPage.ChartType = Excel.XlChartType.xlColumnClustered;
                        break;
                    case 1:
                        chartPage.ChartType = Excel.XlChartType.xlLine;
                        break;
                    case 2:
                        chartPage.ChartType = Excel.XlChartType.xlArea;
                        break;
                    case 3:
                        chartPage.ChartType = Excel.XlChartType.xlAreaStacked;
                        break;
                    default:
                        break;
                }

                xlWorkBook.SaveAs(filePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            }
            catch (Exception ex)
            {
                //FinishProgress();
                WpfMessageBox.Show(MessageResource.Message, MessageResource.ErrorFile);
            }
            finally
            {
                WpfMessageBox.Show(MessageResource.Message, MessageResource.OutPutFile);
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

                WpfMessageBox.Show(MessageResource.Message, "Exception Occured while releasing object " + ex.ToString() + "خطا");
            }
            finally
            {
                GC.Collect();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            SettingExpander.IsExpanded = false;
        }
    }

}
