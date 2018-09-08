using CMLSmartHome.Enums;

namespace CMLSmartHome.Models
{
    /// <summary>
    /// Čidlo pro snímání fyzikálních veličin
    /// </summary>
    public class Sensor
    {
        /// <summary>
        /// Jednoynačný identifikátor záznamu
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Název senzoru
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Typ senzoru
        /// </summary>
        public SensorType Type { get; set; }
    }
}
