using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using ReporterWPF.Update;

namespace ReporterWPF
{
    public static class GlobalVariable
    {
        public static int Ok = 0;
        public static ushort StationIdCurrent;
        public static Pump_Struct ComboBoxPumpStationG;
        public static Pump_Struct ComboBoxValveStationG;
        public static Supply_Struct ComboBoxSupplyStationG;
        public static int Language = 0;
        public static bool IsValve=true;
    }
}