using Reporter.Database;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using DevExpress.Mvvm.POCO;
using Reporter.Localization.Login;
using Reporter.Server.Utils;
using Main0Resource =Reporter.Localization.Main0;
using ManagementResource = Reporter.Localization.Management;
using ReporterWPF.Utils;

namespace ReporterWPF.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public DispatcherTimer timerLogin = new DispatcherTimer();
        private int count = 0;
        private byte st = 0;
        private SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
        public Login()
        {
            InitializeComponent();


        }

        private void NumberError(object sender, ValidationErrorEventArgs e)
        {
      
        }

        public void Log(string s, bool nl)
        {
            MessageBox.Show(s + "  " + nl.ToString());
        }
        
        private void btnlog_Click(object sender, RoutedEventArgs e)
        {
            ////Logger.SERVER.Error("Login ");
            //IpPottSetting ipPottSetting=new IpPottSetting();
            //if (TxtPort.Text.Trim() != "")
            //    IpPottSetting.UpdateSetting("ServerPort", TxtPort.Text.Trim());
            //if (TxtIP.Text.Trim() != "...")
            //{
            //    IpPottSetting.UpdateSetting("ServerLocalAddress", TxtIP.Text.Trim());
            //    ipPottSetting.changeConnectionSettings(TxtIP.Text.Trim());

            //}



            timerLogin.Tick += new EventHandler(timerLogin_Tick);
            timerLogin.Interval = new TimeSpan(0, 0, 0, 5, 0);
           // timerLogin.Start();
            WrongLoginText.Text = "";
            GridLogin.IsEnabled = false;
            LoadingIndicator.IsActive = true;
            txtuser.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            
            if (txtpass.Password.Trim() == "")
            {
                WrongLoginText.Text = "";
                GridLogin.IsEnabled = true;
                LoadingIndicator.IsActive = false;
                tbpass.Visibility = Visibility.Visible;
                return;
            }
            if (txtuser.Text.Trim() == "" )
            {
                WrongLoginText.Text = "";
                GridLogin.IsEnabled = true;
                LoadingIndicator.IsActive = false;
                return;
            }
        

            WrongLoginText.Text = "";
            GridLogin.IsEnabled = false;
            LoadingIndicator.IsActive = true;


          

                //try
                //{
                    using (var db = new ABFAEntities())
                    {
                        var username = txtuser.Text.Trim();

                        var q = from u in db.UsersABFAs
                                where u.UserAbfa == username
                                select u;

                        if (q.Any())
                        {
                            var password = Encoding.Unicode.GetBytes(txtpass.Password.Trim());
                            var bytes = sha1.ComputeHash(password);
                            var hash = Convert.ToBase64String(bytes);

                            var user = q.First();
                            if (user.PassAbfa == hash)
                            {
                                var C_User = user;
                                var sw = new SplashWindow(st, C_User);
                                Close();
                                sw.Show();

                                return;
                            }
                            
                            WrongLoginText.Text= FieldResource.WrongLogin;// ".رمز عبور اشتباه است";
                            GridLogin.IsEnabled = true;
                            LoadingIndicator.IsActive = false;
                            return;
                            
                        }
                        else
                        {
                            WrongLoginText.Text = FieldResource.WrongLogin;// ".چنین کاربری در سیسنم وجود ندارد";
                        GridLogin.IsEnabled = true;
                        LoadingIndicator.IsActive = false;
                        return;
                        }
                    }

                //}
                //catch (Exception ex)
                //{
                //    //Logger.SERVER.Error
                //  //  Utils.ShowError("Login error: " + ex.Message + "\n\n" + ex.StackTrace + "\n\n" + ex.InnerException);
                //    WpfMessageBox.Show(
                //        Reporter.Localization.Message.MessageResource.Message,
                //        Reporter.Localization.Message.MessageResource.ConnectionError, MessageBoxButton.OK, Utils.MessageBoxImage.Warning);
                //      //    MessageBoxButton.OK, Utils.MessageBoxImage.Warning);
                //    GridLogin.IsEnabled = true;
                //    LoadingIndicator.IsActive = false;
                
                
                //}
            
            
        }

        public void timerLogin_Tick(object sender, EventArgs e)
        {
            try
            {
                count++;
                if (count == 10)
                {
                    GridLogin.IsEnabled = true;
                    LoadingIndicator.IsActive = false;
                    count = 0;
                }
                else if (count == 20)
                {
                    WpfMessageBox.Show(
                       Reporter.Localization.Message.MessageResource.Message,
                       Reporter.Localization.Message.MessageResource.ConnectionError, MessageBoxButton.OK, Utils.MessageBoxImage.Warning);
                }
               
            }
            catch (Exception ex)
            {
            }

        }

        private void SendRequestLogin()
        {

            Thread.Sleep(TimeSpan.FromSeconds(5));

            this.Dispatcher.BeginInvoke(new Action(() =>
            {



            }));

        }

     
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //  this.Close();
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MyData data = new MyData("", "");
            txtuser.DataContext = data;
            txtpass.DataContext = data;
            txtuser.Focus();
            txtuser.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            //txtpass.GetBindingExpression(TextBox.TextProperty).UpdateSource();

        }



        
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            if (txtpass.Password.Trim() == "")
            {
                tbpass.Visibility = Visibility.Visible;
                // btnlog.IsEnabled = false;
            }
            else
            {
                tbpass.Visibility = Visibility.Hidden;
                // btnlog.IsEnabled = true;
            }
              }

        public SecureString SecurePassword { private get; set; }

        private void FrRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            GlobalVariable.Language = 0;
            Application.Current.Resources["FlowDirectionRL"] = FlowDirection.RightToLeft;
            Application.Current.Resources["FlowDirectionLR"] = FlowDirection.LeftToRight;
            var vCulture = new CultureInfo("fr");
            if (UserNameLabel==null)
            {
                return;
            }
          //  Thread.CurrentThread.CurrentCulture = vCulture;
            Thread.CurrentThread.CurrentUICulture = vCulture;
        //    CultureInfo.DefaultThreadCurrentCulture = vCulture;
            CultureInfo.DefaultThreadCurrentUICulture = vCulture;
            ScadaTextBlock.Text = FieldResource.SCADA;
            UserNameLabel.Content = FieldResource.UserName;
            PasswordLabel.Content = FieldResource.Password;
            btnlog.Content = FieldResource.Login;
            SettingLabel.Content = Main0Resource.FieldResource.Setting;
            if (IPLabel == null) return;
            IPLabel.Content = ManagementResource.FieldResource.IP;
            PortLabel.Content = ManagementResource.FieldResource.Port;
                    }

        private void EnRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            GlobalVariable.Language = 1;
            Application.Current.Resources["FlowDirectionRL"] = FlowDirection.LeftToRight;
            Application.Current.Resources["FlowDirectionLR"] = FlowDirection.RightToLeft;
            var vCulture = new CultureInfo("en");

         //   Thread.CurrentThread.CurrentCulture = vCulture;
            Thread.CurrentThread.CurrentUICulture = vCulture;
         //   CultureInfo.DefaultThreadCurrentCulture = vCulture;
            CultureInfo.DefaultThreadCurrentUICulture = vCulture;
            ScadaTextBlock.Text = FieldResource.SCADA;
            UserNameLabel.Content = FieldResource.UserName;
            PasswordLabel.Content = FieldResource.Password;
            btnlog.Content = FieldResource.Login;
            SettingLabel.Content = Main0Resource.FieldResource.Setting;
            if (IPLabel == null) return;
            IPLabel.Content = ManagementResource.FieldResource.IP;
            PortLabel.Content = ManagementResource.FieldResource.Port;
              }


        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }

    public class MyData
    {
        public object FirstNumber // FirstNumber Property
        { set; get; }

        public object SecondNumber // SecondNumber Property
        { get; set; }

        public object Division // Division Property
        {
            get
            {
                try
                {
                    double n1 = Convert.ToDouble(FirstNumber.ToString());
                    double n2 = Convert.ToDouble(SecondNumber.ToString());
                    double n3 = n1/n2;
                    return n3;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public MyData(object FirstNumber, object SecondNumber) // Constructor
        {
            this.FirstNumber = FirstNumber;
            this.SecondNumber = SecondNumber;
        }


    }

    public class NumberValidator : ValidationRule
    {
        public override ValidationResult Validate
            (object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null || value.ToString() == "")
                return new ValidationResult(false, "*");
            
            return new ValidationResult(true, null);
        }
    }

    public class Customer
    {
        private string _name;


        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (String.IsNullOrEmpty(value))
                {
                    return;
                    }
            }
        }
    }


    public static class PasswordHelper
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password",
                typeof(string), typeof(PasswordHelper),
                new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach",
                typeof(bool), typeof(PasswordHelper), new PropertyMetadata(false, Attach));

        private static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached("IsUpdating", typeof(bool),
                typeof(PasswordHelper));


        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }

        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }

        public static string GetPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }

        private static bool GetIsUpdating(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsUpdatingProperty);
        }

        private static void SetIsUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(IsUpdatingProperty, value);
        }

        private static void OnPasswordPropertyChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            passwordBox.PasswordChanged -= PasswordChanged;

            if (!(bool)GetIsUpdating(passwordBox))
            {
                passwordBox.Password = (string)e.NewValue;
            }
            passwordBox.PasswordChanged += PasswordChanged;
        }

        private static void Attach(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;

            if (passwordBox == null)
                return;

            if ((bool)e.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }

            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            SetIsUpdating(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }
    }

}