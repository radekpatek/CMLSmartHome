using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMLSmartHome.Models
{
    /// <summary>
    /// Zařízení, které zajišťuje řízení prvků 
    /// </summary>
    public class SmartHomeController : NetworkDevice
    {
        /// <summary>
        /// Seznam kolektorů se senzory
        /// </summary>
        public ICollection<Collector> Collectors { get; set; } = new List<Collector>();
    }
}
