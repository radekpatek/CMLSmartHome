using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMLSmartHome.Models
{
    /// <summary>
    /// Zařízení, které je registrovat v síti
    /// </summary>
    public class NetworkDevice
    {
        /// <summary>
        /// Jednoznačný identifikátor
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Název zařízení
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// IP adresa zařízení
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// MAC adresa zařízení
        /// </summary>
        public string MACAddress { get; set; }

        /// <summary>
        /// Popis zařízení
        /// </summary>
        public string Description { get; set; }
    }
}
