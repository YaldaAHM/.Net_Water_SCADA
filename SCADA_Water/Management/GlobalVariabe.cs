using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporterWPF.Management
{
    public static class GlobalVariabe
    {
        public static string StationIdManagement;
        public static ushort StationIdManagementOrig;
        public static List<StationsABFA> Station_list = new List<StationsABFA>();
        public static List<Node_Mapping> Nodes = new List<Node_Mapping>();
    }
}
