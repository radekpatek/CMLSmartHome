using CMLSmartHomeCollector.Classes.Sensors;
using CMLSmartHomeCollector.Config;
using Microsoft.Extensions.Configuration;

namespace CMLSmartHomeCollector.Factories
{
    static class SensorFactory
    {
        /// <summary>
        /// Decides which class of Sensor to instantiate.
        /// </summary>
        public static SensorCollector Get(IConfigurationSection section)
        {
            var readingFrequency = section.GetValue<int>("ReadingFrequency");
            var type = section.GetValue<string>("Type");

            switch (type)
            {
                case "Temperature":
                    return new TemperatureSensor(readingFrequency);
                case "Humidity":
                    return new HumiditySensor(readingFrequency);
                default:
                    throw new ConfigurationValueMissingException(
                            string.Format("Invalid sensor config value type \'{0}\'", type));
            }
        }
    }
}
