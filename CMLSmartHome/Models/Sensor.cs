using CMLSmartHome.Enums;
using Microsoft.Extensions.Configuration;

namespace CMLSmartHome.Models
{
    /// <summary>
    /// Čidlo pro snímání fyzikálních veličin
    /// </summary>
    public class Sensor
    {
        public Sensor(IConfigurationSection configurationSection)
        {
            var type = configurationSection.GetValue<string>("Type");
            var readingFrequency = configurationSection.GetValue<int>("ReadingFrequency");
        }

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
