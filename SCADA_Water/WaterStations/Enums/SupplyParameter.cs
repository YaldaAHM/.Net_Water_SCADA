using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporter.Localization.Parameter;

namespace ReporterWPF.WaterStations.Enums
{  [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum SupplyParameter
    {
        [LocalizedDescription("DeviceStatuse", typeof(SupplyParameterResource))]
        DeviceStatuse = 0,
        [LocalizedDescription("FlotterStatus", typeof(SupplyParameterResource))]
        FlotterStatus = 1,
        [LocalizedDescription("RFStatus", typeof(SupplyParameterResource))]
        RFStatus = 2,
        [LocalizedDescription("VIn", typeof(SupplyParameterResource))]
        VIn = 3,
        [LocalizedDescription("VBatt", typeof(SupplyParameterResource))]
        VBatt = 4,
        [LocalizedDescription("SupplyLevel", typeof(SupplyParameterResource))]
        SupplyLevel = 5,
        [LocalizedDescription("Temp", typeof(SupplyParameterResource))]
        Temp = 6,

    }
   
}
