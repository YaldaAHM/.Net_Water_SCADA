using GMap.NET;
using GMap.NET.WindowsPresentation;
using ReporterWPF.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
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
// using Microsoft.Maps.MapControl.WPF;

namespace ReporterWPF.WaterStations
{
    /// <summary>
    /// Interaction logic for Stations1.xaml
    /// </summary>
    public partial class StationsWindow : Window
    {
        public StationsWindow(UsersABFA c_user)
        {
            InitializeComponent();
           InitiateMap();
          
            C_User = c_user;
            Station_Update();
        }

        public void InitiateMap()
        {
            MainWindow0 mainWindow00 = Application.Current.Windows.OfType<MainWindow0>().FirstOrDefault();
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            //// choose your provider here
            Map.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            Map.CacheLocation = @"cache";
            Map.ShowCenter = false;
            Map.MinZoom = 2;
            Map.MaxZoom = 17;
            //// whole world zoom
            Map.Zoom = 10;
            double lat = 32.6775175;
            double longt = 50.8589507;
            
            Map.CanDragMap = true;
            // lets the user drag the map with the left mouse button
           
            Map.DragButton =  MouseButton.Left;
           

            Map.Position = new GMap.NET.PointLatLng(lat, longt);
            
            Map.BoundsOfMap = new RectLatLng(lat + 2, longt - 2, 4, 4);


             Map.MouseWheelZoomType=MouseWheelZoomType.MousePositionWithoutCenter;

        }



        private void Map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Map_OnPositionChanged(PointLatLng point)
        {
            //Map.CanDragMap = true;
            if (point.Lat >= 37.55555 || point.Lat <= 22.5555 || point.Lng <= 51.55555 || point.Lng >= 55.5555)
            {
                
                double lat = double.Parse("32.6775175", CultureInfo.InvariantCulture);
                double lng = double.Parse("50.8589507", CultureInfo.InvariantCulture);
                
                Map.Position = new PointLatLng(lat, lng);
                Map.Zoom = 5;
            }
           

        }

        public void AddMarker(double lat, double lng)
        {
            GMapMarker marker = new GMapMarker(
               new PointLatLng(lat, lng));
          //  marker.Shape = new ShapStation(C_User,StationsABFA);
       
            marker.Offset.Offset(5, 5);
          
          
            Map.Markers.Add(marker);
            
        }

        public void AddMarker(StationsABFA stationsAbfa, bool isEnable)
        {
            GMapMarker marker = new GMapMarker(
               new PointLatLng(stationsAbfa.latitude.Value, stationsAbfa.Longitude.Value));



            marker.Shape = new ShapStation(C_User, stationsAbfa) { SetText = stationsAbfa.Name, StationId = stationsAbfa.ID_Station.ToString() };

            marker.Shape.IsEnabled = isEnable;
            marker.Offset.Offset(5, 5);
            marker.Tag = new MarkerInfo() { Id = stationsAbfa.ID_Station, Name = stationsAbfa.Name, Tag = stationsAbfa.Longitude.ToString() };


            Map.Markers.Add(marker);

        }

        

        List<StationsABFA> Station_list = new List<StationsABFA>();
       
        ToolTip toolTip1 = new ToolTip();
        private UsersABFA C_User;
        
       





       

      

        //////////private void Node_Click(object sender, EventArgs e)
        //////////{
        //////////    PictureBox snd = sender as PictureBox;
        //////////    if (snd != null)
        //////////    {
        //////////        var q = from u in Station_list
        //////////                where u.ID_Station == (long)snd.Tag
        //////////                select u;
        //////////        if (q.Any())
        //////////        {
        //////////            WS_Panel box = new WS_Panel(q.First(), C_User);
        //////////            box.ShowDialog();
        //////////        }
        //////////    }
        //////////}
        private void Station_Update()
        {
            Station_list.Clear();
            //try
            //{
                using (var db = new ABFAEntities())
                {

                var q = from u in db.StationsABFAs
                            orderby u.Name
                            select u;
                    //  Console.WriteLine("select");

                    if (q.Any())
                    {

                        foreach (var st in q)
                        {
                            Station_list.Add(st);
                            bool isEnable = ((C_User.Role < 20) ||
                                             (C_User.Role < 30 && C_User.Role >= 20 && st.ID_State == C_User.ID_State)
                                             || (C_User.Role < 40 && C_User.Role >= 30 && st.Name == C_User.Station));
                          //  AddMarker((double)st.latitude, (double)st.Longitude, st.ID_Station,st.Name,st.Longitude.ToString(),isEnable);
                        AddMarker( st, isEnable);

                        //Left = x_cal((double)st.Longitude),
                        //Top = y_cal((double)st.latitude),
                        //Tag = st.ID_Station,
                        ////Enabled = (C_User.Role < 10) || (C_User.Role<30 && C_User.Role>20 && st.ID_State ==C_User.ID_State),

                        ////////////////if ((C_User.Role < 20) || (C_User.Role < 30 && C_User.Role >= 20 && st.ID_State == C_User.ID_State)
                        ////////////////    || (C_User.Role < 40 && C_User.Role >= 30 && st.Name == C_User.Station))
                        ////////////////    pic.Click += new System.EventHandler(this.Node_Click);
                        ////////////////toolTip1.SetToolTip(pic, st.Name);
                        //////////////////this.Controls.Add(pic);
                        ////////////////pictureBox1.Controls.Add(pic);
                        //////////////////node_list.Add(pic);

                    }
                       ReporterWPF.Management.GlobalVariabe.Station_list = Station_list;
                    }
                }

            //}
            //catch (Exception ex)
            //{
            //    //Logger.SERVER.Error("Login error: " + ex.Message + "\n\n" + ex.StackTrace);
            //    WpfMessageBox.Show
            //          ("پیغام", ex.Message+ "\n\n Inner: " + ex.InnerException,
            //              MessageBoxButton.OK, Utils.MessageBoxImage.Warning);
            //    WpfMessageBox.Show
            //           ("پیغام", ".دسترسی به سرور امکان‌پذیر نیست. از اتصال به شبکه مطمئن شوید",
            //               MessageBoxButton.OK, Utils.MessageBoxImage.Warning);
            //    return;
            //}
          
        }

        

       

 public class MarkerInfo
 {
     public long Id { get; set; }
     public string Name { get; set; }
     public string Tag { get; set; }          
 }


        //private void Map_OnOnMapDrag()
        //{
        //    //        Map.SelectedArea=new RectLatLng(35,48,5,5);
        //      //Map.BoundsOfMap = new RectLatLng(35,48,5,50);
        //}
        private void ResetZoomBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Map.Zoom = 10;
        }
    }
}
