using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Reporter.Database;
using Timer = System.Timers.Timer;

namespace ReporterWPF.Login
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        private byte Frm;
        private UsersABFA C_User;
        public SplashWindow(byte frm, UsersABFA c_user)
        {
            Frm = frm;
            C_User = c_user;
            InitializeComponent();
        }


        private void WindowContentRendered(object sender, EventArgs e)
        {
            if (Frm == 2)
            {
                var mw = new MainWindow();
                mw.ContentRendered += MainWindowContentRendered;
                mw.Show();
            }
            else
            {

                Thread.Sleep(1000);
                this.Visibility = Visibility.Hidden;

                var ws = new MainWindow0(C_User);

                ws.Show();
            }
        }


        private void MainWindowContentRendered(object sender, EventArgs e)
        {
            var t = new Timer
            {
                Interval = 500,
                AutoReset = false
            };
            t.Elapsed += TimerElapsed;
            t.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(Close);
        }
    }
}