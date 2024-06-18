using System;
using System.Collections.Generic;
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
using DevExpress.Xpf.Charts;
using ReporterWPF.Utils;

namespace ReporterWPF.WaterStations.Report
{
    /// <summary>
    /// Interaction logic for ReportControl.xaml
    /// </summary>
    public partial class ReportControl : UserControl
    {
        private string SelectedSeries = "";
        public ReportControl()
        {
            InitializeComponent();
         
            //var d=new ChartViewModel();
            //DataContext =d;
        }

        private List<SolidColorBrush> _brushes;

        private void InitBrushes()
        {
            _brushes = new List<SolidColorBrush>();
            var props = typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var propInfo in props)
            {
                _brushes.Add((SolidColorBrush)propInfo.GetValue(null, null));
            }
        }

        private Random _rand = new Random();

        private SolidColorBrush GetRandomBrush()
        {
            return _brushes[_rand.Next(_brushes.Count)];
        }

        private XYDiagram2D diagram = new XYDiagram2D();


        ////private LineSeries2D DrawLineSeries2D()
        ////{
        ////    LineSeries2D series = new LineSeries2D();
        ////    series.DisplayName = titleSeries;
        ////    //   series.ShowInLegend = true;
        ////    //series.Label=new SeriesLabel();
        ////    //series.Label.TextPattern="efdddd";
        ////    //series.LabelsVisibility = true;
        ////    series.ToolTip = titleSeries;
        ////    Random rand = new Random();
        ////    SolidColorBrush brush =
        ////        new SolidColorBrush(Color.FromRgb((byte)rand.Next(0, 256), (byte)rand.Next(0, 256),
        ////            (byte)rand.Next(0, 256)));
        ////    // series.Brush = Brushes.BlueViolet;
        ////    //series.Brush = GetRandomBrush();

        ////    series.Brush = brush;
        ////    ChartControl1.Diagram = diagram;
        ////    diagram.ActualAxisX.Label = new AxisLabel();
        ////    diagram.ActualAxisX.Label.Angle = 90;
        ////    ChartControl1.Diagram.Series.Add(series);
        ////    diagram.EnableAxisXNavigation = true;
        ////    diagram.EnableAxisYNavigation = true;
        ////    ChartControl1.Legend = new Legend();
        ////    //ChartControl1.Legend.Title.Visible = true;
        ////    //   ChartControl1.Legend.Items.FirstOrDefault(x=>x.Legend.n).Visible = true;
        ////    //   ChartControl1.IsManipulationEnabled = true;

        ////    series.LineStyle = new LineStyle();
        ////    series.LineStyle.Thickness = 2;
        ////    diagram.AxisX = new AxisX2D();

        ////    diagram.AxisX.GridLinesVisible = true;
        ////    //diagram.AxisY.Strips.Add(new Strip() { AxisLabelText = "", MinLimit = -40, MaxLimit = 0, Brush = new SolidColorBrush(Color.FromRgb(255, 219, 219)), BorderThickness = new Thickness(0.0), BorderColor = Color.FromArgb(0, 255, 255, 255) });
        ////    //diagram.AxisY.Strips.Add(new Strip()
        ////    //{
        ////    //    AxisLabelText = "",
        ////    //    MinLimit = 0,
        ////    //    MaxLimit = 20,
        ////    //    Brush = 
        ////    //    new SolidColorBrush(Color.FromRgb(214, 246, 200)),
        ////    //    BorderThickness = new Thickness(0.0),BorderColor =Color.FromArgb(0,255,255,255) 
        ////    //});
        ////    //diagram.AxisY.Strips.Add(new Strip() { AxisLabelText = "", MinLimit = 20, MaxLimit = 60, Brush = new SolidColorBrush(Color.FromRgb(255, 219, 219)), BorderThickness = new Thickness(0.0), BorderColor = Color.FromArgb(0, 255, 255, 255) });
        ////    //   ChartControl1.IsManipulationEnabled = true;
        ////    return series;
        ////}

        public void RemoveIfExist(string SelectedSeriesDisplayName)
        {
            if (ChartControl1.Diagram != null && ChartControl1.Diagram.Series != null)
            {
                var series =
                    ChartControl1.Diagram.Series.FirstOrDefault(xx => xx.DisplayName == SelectedSeriesDisplayName);
                if (series != null)
                    ChartControl1.Diagram.Series.Remove(series);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            //var s = ChartControl1.Diagram.Series.FirstOrDefault(x=>x.DisplayName==titleSeries);
            //ChartControl1.Diagram.Series.Remove(s);
            if (ChartControl1.Diagram.Series == null) return;
            ChartControl1.Diagram.Series.Clear();
            //ReportChart.Series.Clear();
        }

        private void chart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Right)
            {
                menu.Visibility = Visibility.Hidden;
                return;
            }
            ChartHitInfo info = null;
            Point clickPosition = e.GetPosition(ChartControl1);
            for (int i = 0; i < 8; i++)
            {
                Point cp = new Point(clickPosition.X, clickPosition.Y + i + 4);
                if (cp.Y <= ChartControl1.Height)
                {
                    info = ChartControl1.CalcHitInfo(cp);
                    if (info.Series != null)
                        break;
                }
                cp = new Point(clickPosition.X, clickPosition.Y - i + 4);
                //if (cp.Y >= 0)
                {
                    info = ChartControl1.CalcHitInfo(cp);
                    if (info.Series != null)
                        break;
                }
            }

            //if (info.SeriesPoint == null)
            //    return;
            //menu.DataContext = info.SeriesPoint;
            if (info.Series == null)
                return;
            menu.DataContext = info.Series.DisplayName;
            menu.Visibility = Visibility.Visible;
            SelectedSeries = info.Series.DisplayName;
            Canvas.SetLeft(menu, clickPosition.X);
            Canvas.SetTop(menu, clickPosition.Y);
        }







        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            menu.Visibility = Visibility.Hidden;
            if (e.ChangedButton == MouseButton.Right)
            {
                return;
            }
            var series = ChartControl1.Diagram.Series.FirstOrDefault(xx => xx.DisplayName == SelectedSeries);
            ChartControl1.Diagram.Series.Remove(series);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = ChartControl1.Visibility;
        }

        
    }



    public class GdpSeriesInput
    {
        public string ItemName { get; set; }
        public List<GdpInput> Values { get; set; }
    }

    public class GdpInput
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }

    public class GdpSeries
    {
        public string ItemName { get; set; }
        public IEnumerable<Gdp> Values { get; set; }
    }

    public class Gdp
    {
        public DateTime Year { get; set; }
        public double Value { get; set; }
    }

    public class ChartViewModel
    {
        public List<GdpSeries> GdpSeries { get; private set; }
        public ChartViewModel()
        {
            GdpSeries = new List<GdpSeries>() {
//            new GdpSeries {
//                ItemName = "USA",
//                Values = new List<Gdp>() {
//                    new Gdp { Year = "1", Value = 18.037},
//                    new Gdp { Year = "2", Value = 17.393},
//                    new Gdp { Year = "3", Value = 16.692},
//                    new Gdp { Year = "4", Value = 16.155},
//                    new Gdp { Year = "5", Value = 15.518},
//                    new Gdp { Year = "6", Value = 14.964},
//                    new Gdp { Year = "7", Value = 14.419},
//                    new Gdp { Year = "8", Value = 14.719}
//                }
//            },
//            new GdpSeries {
//                ItemName = "China",
//                Values = new List<Gdp>() {
//                    new Gdp { Year = "1", Value = 11.065},
//                    new Gdp { Year = "2", Value = 10.482},
//                    new Gdp { Year = "3", Value = 9.607},
//                    new Gdp { Year = "4", Value = 8.561},
//                    new Gdp { Year = "5", Value = 7.573},
//                    new Gdp { Year = "6", Value = 6.101},
//                    new Gdp { Year = "7", Value = 5.11},
//                    new Gdp { Year = "8", Value = 4.598}
//                }
//            },
//            new GdpSeries {
//                ItemName = "Japan",
//                Values = new List<Gdp>() {
//                    new Gdp { Year = "1", Value = 4.383},
//                    new Gdp { Year = "2", Value = 4.849},
//                    new Gdp { Year = "3", Value = 5.156},
//                    new Gdp { Year = "4", Value = 6.203},
//                    new Gdp { Year = "5", Value = 6.157},
//                    new Gdp { Year = "6", Value = 5.7},
//                    new Gdp { Year = "7", Value = 5.231},
//                    new Gdp { Year = "8", Value = 5.038}
//                }
       //     }
        };
        }

        public ChartViewModel(List<GdpSeriesInput> gdps)
        {
            var dateConverter = new DateConverter();
            //  ReportControl reportControl = Application.Current.Windows.OfType<ReportControl>().FirstOrDefault();
            //var dc=  reportControl?.ChartControl1.DataContext;

            //if (pSWindow != null)
            List<Gdp> gl;   GdpSeries = new List<GdpSeries>();
            foreach (var gdp in gdps)
            {
                gl = new List<Gdp>();
                foreach (var g in gdp.Values)
                {
                    gl.Add(new Gdp()
                    {
                        Year = g.Date,
                        Value = g.Value
                    });
                }


            
                GdpSeries.Add(new GdpSeries()
                {
                    ItemName = gdp.ItemName,
                    Values = gl,


                });
            }
        }
    }

    



  
}

   