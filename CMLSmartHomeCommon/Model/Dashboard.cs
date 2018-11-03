
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
        /// Vnitřní kollektor
        /// </summary>
        public Collector InternalCollector { get; set; }

        /// <summary>
        /// Venkovní kollektor
        /// </summary>
        public Collector OutdoorCollector { get; set; }

    }
}
