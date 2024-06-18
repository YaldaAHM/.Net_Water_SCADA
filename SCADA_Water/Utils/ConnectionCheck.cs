using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ReporterWPF.Utils
{
    public class ConnectionCheck
    {
        public bool PingTest()
        {
            try
            {

            
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            var ip = ConfigurationManager.AppSettings["ServerLocalAddress"];
            System.Net.NetworkInformation.PingReply pingStatus =
                ping.Send(ip, 1000);

            if (pingStatus.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
    }
}
