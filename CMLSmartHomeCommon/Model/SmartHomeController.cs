using CMLSmartHomeCommon.Models;
using System.Collections.Generic;

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
