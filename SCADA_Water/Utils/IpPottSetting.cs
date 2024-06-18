using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReporterWPF.Utils
{
    public class IpPottSetting
    {
        public static void UpdateSetting(string key, string value)
        {
            System.Configuration.Configuration configuration = ConfigurationManager.
                OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }
        public String changeConnStringItem(string connString, string option, string value)
        {
            String[] conItems = connString.Split(';');
            String result = "";
            foreach (String item in conItems)
            {
                if (item.StartsWith(option))
                {
                    result += option + "=" + value + ";";
                }
                else
                {
                    result += item + ";";
                }
            }
            return result;
        }
        public void changeConnectionSettings(string ip)
        {
            var cnSection = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            String connString = cnSection.ConnectionStrings.ConnectionStrings["ABFAEntities"].ConnectionString;
            connString = changeConnStringItem(connString, "provider connection string=\"data source", ip);
            connString = changeConnStringItem(connString, "provider connection string=\"server", ip);
            cnSection.ConnectionStrings.ConnectionStrings["ABFAEntities"].ConnectionString = connString;
            cnSection.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
