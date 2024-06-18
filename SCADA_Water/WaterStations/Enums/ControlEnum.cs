using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reporter.Localization.Parameter;

namespace ReporterWPF.WaterStations.Enums
{

    [TypeConverter(typeof (EnumDescriptionTypeConverter))]
    public enum Cont_Mode
    {
        
        [LocalizedDescription("Auto", typeof(ControlModeParameterResource))]
        Auto = 0,
        [LocalizedDescription("SERVER", typeof(ControlModeParameterResource))]
        SERVER = 1,
        [LocalizedDescription("SMS", typeof(ControlModeParameterResource))]
        SMS = 2,
        [LocalizedDescription("Time", typeof(ControlModeParameterResource))]
        Time = 3,

    }

    public class ControlMode
    {
        public Cont_Mode ContMode(byte cont_Mode)
        {
            var cm = cont_Mode & 0x0F;
            switch (cm)
            {
                case 0:
                    return Cont_Mode.Auto;
                case 1:
                    return Cont_Mode.SERVER;
                case 2:
                    return Cont_Mode.SMS;
                case 3:
                    return Cont_Mode.Time;
            }
            return Cont_Mode.Auto;
        }
    }
}

