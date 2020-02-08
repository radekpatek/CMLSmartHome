
using System.Collections.Generic;

namespace CMLSmartHomeCommon.Model
{
    /// <summary>
    /// Konfigurace nástěnky
    /// </summary>
    public class Dashboard
    {

        /// <summary>
        /// Jednoynačný identifikátor záznamu
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Venkovní kolektor
        /// </summary>
        public Collector OutdoorCollector { get; set; }

    }
}
