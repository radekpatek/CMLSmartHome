
using CMLSmartHomeCollector.DHT;
using CMLSmartHomeCollector.Sensors.DHT;
using CMLSmartHomeCommon.Enums;
using log4net;
using System.Threading;
using Unosquare.RaspberryIO;

namespace CMLSmartHomeCollector.Sensors
{
    /// <summary>
    /// Humidity sensor
    /// </summary>
    public class HumiditySensor : SensorCollector
    {
        private ILog _logger;
        private DHTSensor _sensor;

        /// <summary>
        /// Constructor of Humidity sensor
        /// </summary>
        /// <param name="readingFrequency"></param>
        public HumiditySensor(ILog logger)
        {
            Type = SensorType.Humidity;
            Unit = UnitType.Percent;
            _logger = logger;
            _sensor = new DHTSensor(Pi.Gpio.Pin07, DHTSensorTypes.DHT11);
            Thread.Sleep(2000); //Inicializace sensoru před měřením
        }

        /// <summary>
        /// Measures the value of the sensor
        /// </summary>
        /// <returns></returns>
        public override double Measure()
        {
            _logger.Info("HumiditySensor.Measure");
            var measured = false;
            double data = 0;

            while (!measured)
            {
                try
                {
                    data = _sensor.ReadData().Humidity;
                    measured = true;
                }
                catch
                {
                    Thread.Sleep(2000);
                }
            }

            return data;
        }

        public override double Measure(SensorType type)
        {
            return Measure();
        }
    }
}
