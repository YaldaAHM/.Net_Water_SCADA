using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporterWPF.Update
{
    public class WindowsUpdate
    {
        private static readonly string CONNECTION_STRING_KEY = "DatabaseEntities2";
        private static readonly string CONNECTION_STRING =
            ConfigurationManager.ConnectionStrings[CONNECTION_STRING_KEY].ConnectionString;


        private void LoadData()
        {
            ServerHandler.Instance.DatabaseUpdated += DatabaseUpdated;
            ServerHandler.Instance.Start();
        }
        private void DatabaseUpdated()
        {
            Task.Run(new Action(DatabaseUpdated2));
        }
        private void DatabaseUpdated2()
        {
           
        }
    }
}
