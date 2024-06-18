using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Reporter.Localization.Parameter;

namespace ReporterWPF.WaterStations.Enums
{  [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum PumpParameter
    {
        [LocalizedDescription("MotorStatus", typeof(PumpParameterResource))]
        MotorStatuse = 0,
        [LocalizedDescription("ControlStatus", typeof(PumpParameterResource))]
        ControlStatus = 1,
        [LocalizedDescription("PhaseControl", typeof(PumpParameterResource))]
        PhaseControl = 2,
        [LocalizedDescription("Bimetal", typeof(PumpParameterResource))]
        Bimeta = 3,
        [LocalizedDescription("Fuze", typeof(PumpParameterResource))]
        Fuze = 4,
        [LocalizedDescription("RFStatus", typeof(PumpParameterResource))]
        RFStatus = 5,
        [LocalizedDescription("EnergicStatus", typeof(PumpParameterResource))]
        EnergicStatus = 6,
        [LocalizedDescription("VIn", typeof(PumpParameterResource))]
        VIn = 7,
        [LocalizedDescription("VBatt", typeof(PumpParameterResource))]
        VBatt = 8,
        [LocalizedDescription("RTUStatus", typeof(PumpParameterResource))]
        RTUStatus = 9,

    }
   
}
