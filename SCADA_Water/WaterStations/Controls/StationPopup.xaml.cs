﻿using System;
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
    /// Interaction logic for StationPopup.xaml
    /// </summary>
    public partial class StationPopup : UserControl
    {
        public StationPopup()
        {
            InitializeComponent();
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PopupStation.IsOpen = false;
        }
    }
}