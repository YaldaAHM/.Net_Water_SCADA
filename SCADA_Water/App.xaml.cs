using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using DevExpress.Xpf.Core;
using Reporter.Localization.Message;
//using ReporterWPF.Resoures;
using ReporterWPF.Update;
using ReporterWPF.Utils;
using ReporterWPF.WaterStations;

namespace ReporterWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var vCulture = new CultureInfo("fr");

            Thread.CurrentThread.CurrentUICulture = vCulture;
            CultureInfo.DefaultThreadCurrentUICulture = vCulture;
            FrameworkElement.LanguageProperty.OverrideMetadata(
          typeof(FrameworkElement),
          new FrameworkPropertyMetadata(
       XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
            base.OnStartup(e);


        }
        SupplyStationUpdate supplyStationUpdate = new SupplyStationUpdate();
        PumpStationUpdate pumpStationUpdate = new PumpStationUpdate();
        ValveStationUpdate valveStationUpdate = new ValveStationUpdate();
        private int count = 0;
        private int timeTick = 5000;
        public int timeTickFast = 5000;
        public int timeTickSlow = 180000;
        public App()
        {

            ApplicationThemeHelper.UseLegacyDefaultTheme = true;
            Update(timeTickFast);




        }

        public async void Update(int time)
        {
            timeTick = time;
            while (true)
            {
                await Task.Delay(timeTick);

                if (GlobalVariable.Ok == 1)
                {
                    count++;
                    try
                    {
                        Update_Check();
                    }
                    catch (Exception ex)

                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            WpfMessageBox.Show(MessageResource.ConnectionError);
                        }), DispatcherPriority.ContextIdle);
                    }
                }
                //}
            }

        }

        public void Update_Check()
        {
            if (count == 3)
            {
                count = 0;
            }
            try
            {

                if (GlobalVariable.ComboBoxPumpStationG == null)
                {
                    pumpStationUpdate.InitialPSWindow();
                }
                if (GlobalVariable.ComboBoxValveStationG == null &&
                   GlobalVariable.IsValve
                    )
                {
                    valveStationUpdate.InitialEValveWindow();
                }
                if (GlobalVariable.ComboBoxSupplyStationG == null)
                {
                    supplyStationUpdate.InitialSSWindow();
                }
                if (GlobalVariable.ComboBoxPumpStationG != null)
                {
                    pumpStationUpdate.UpdatePSWindow();
                }
                if (GlobalVariable.ComboBoxValveStationG != null)
                {
                    valveStationUpdate.UpdateEValveWindow();
                }
                if (GlobalVariable.ComboBoxSupplyStationG != null)
                {
                    supplyStationUpdate.UpdateSSWindow();
                }
                if (GlobalVariable.ComboBoxPumpStationG != null &&
                    GlobalVariable.ComboBoxSupplyStationG != null
                   )
                {
                    if (!GlobalVariable.IsValve || (GlobalVariable.IsValve && GlobalVariable.ComboBoxValveStationG != null))
                        timeTick = timeTickSlow;
                }

            }
            catch (Exception ex)
            {
                if (count == 3)
                {
                    count = 0;
                    Dispatcher.Invoke(new Action(() =>
                    {
                        WpfMessageBox.Show(MessageResource.ConnectionError);
                    }), DispatcherPriority.ContextIdle);
                }
            }
        }

    }
}
