using CMLSmartHomeCollector.Config;
using CMLSmartHomeCollector.Enums;
using Microsoft.Extensions.Configuration;

namespace CMLSmartHomeCollector
{
    public class Sensor
    {
        private IConfigurationSection section;

        public Sensor(IConfigurationSection section)
        {
            SensorType type; 

            switch (section.GetValue<string>("Type"))
            {
                case "Temperature":
                    type = SensorType.Temperature;
                    break;
                case "Humidity":
                    type = SensorType.Humidity;
                    break;
                default:
                    throw new ConfigurationValueMissingException(
                            string.Format("Invalid sensor config value type \'{0}\'", section.GetValue<string>("Type")));
            }

            Type = type;
            ReadingFrequency = section.GetValue<int>("ReadingFrequency");
        }

        /// <summary>
        /// Jednoynačný identifikátor senzoru
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Typ senzoru
        /// </summary>
        public SensorType Type { get; set; }
        
        /// <summary>
        /// Frekvence čtení hodnot ze senzoru. Hodnota se uvádí v sekundách
        /// </summary>
        public int ReadingFrequency { get; set; }
    }
}
