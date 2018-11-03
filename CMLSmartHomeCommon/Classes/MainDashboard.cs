using System;
using System.Collections.Generic;
using System.Text;

namespace CMLSmartHomeCommon.Classes
{
    /// <summary>
    /// Hlavní nástěnka pro zobtazení hlavních uakazatelů
    /// </summary>
    public class MainDashboard
    {
        /// <summary>
        /// Vnitřní teplota
        /// </summary>
        public double? InternalTemperature;

        /// <summary>
        /// Vnitřní vhlkost
        /// </summary>
        public double? InternalHumidity;

        /// <summary>
        /// Venkovní teplota
        /// </summary>
        public double? OutdoorTemperature;

        /// <summary>
        /// Venkovní vhlkost
        /// </summary>
        public double? OutdoorHumidity;
    }
}
