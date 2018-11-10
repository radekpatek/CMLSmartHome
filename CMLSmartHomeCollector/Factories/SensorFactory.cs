using CMLSmartHomeCollector.Sensors;
using CMLSmartHomeCollector.Config;
using Microsoft.Extensions.Configuration;
using log4net;

namespace CMLSmartHomeCollector.Factories
{
    public class SensorFactory
    {
        private readonly ILog _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public SensorFactory(ILog logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Decides which class of Sensor to instantiate.
        /// </summary>
        public SensorCollector Get(IConfigurationSection section)
        {
            var type = section.GetValue<string>("Type");

            switch (type)
            {
                case "Temperature":
                    return new TemperatureSensor(_logger);
                case "Humidity":
                    return new HumiditySensor(_logger);
                default:
                    throw new ConfigurationValueMissingException(
                            string.Format("Invalid sensor config value type \'{0}\'", type));
            }
        }
    }
}
