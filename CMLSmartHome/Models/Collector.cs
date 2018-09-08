using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMLSmartHome.Models
{
    /// <summary>
    /// Zařízení, které zajišťuje sběr hodnot ze senzorů
    /// </summary>
    public class Collector : NetworkDevice
    {
        /// <summary>
        /// Umístnění zařízení se senzory
        /// </summary>
        public string Location;

        /// <summary>
        /// Seznam senzorů
        /// </summary>
        public ICollection<Sensor> Sensors {get; set; } = new List<Sensor>();
    }
}
