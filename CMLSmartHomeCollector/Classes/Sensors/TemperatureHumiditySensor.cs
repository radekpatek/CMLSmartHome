using CMLSmartHomeCollector.DHT;
using CMLSmartHomeCollector.Sensors.DHT;
using CMLSmartHomeCommon.Enums;
using log4net;
using System;
using Unosquare.RaspberryIO;

namespace CMLSmartHomeCollector.Sensors
{
    /// <summary>
    /// Temperature and Humidity Sensor
    /// </summary>
    public class TemperatureHumiditySensor : SensorCollector
    {
        private ILog _logger;
        private DHTSensor _sensor;

        /// <summary>
        /// Constructor of Temperature and Humidity sensor
        /// </summary>
        /// <param name="readingFrequency"></param>
        public TemperatureHumiditySensor(ILog logger)
        {
            Type = SensorType.Humidity;
            Unit = UnitType.Percent;
            _logger = logger;
            _sensor = new DHTSensor(Pi.Gpio.Pin07, DHTSensorTypes.DHT11);
        }

        public override double Measure()
        {
            throw new NotImplementedException();
        }

        public override double Measure(SensorType type)
        {
            _logger.Info(String.Format("TemperatureHumiditySensor.Measure ", type));

            switch (type)
            {
                case SensorType.Temperature:
                    _logger.Info(String.Format("TemperatureHumiditySensor.Measure Temperature"));
                    return _sensor.ReadData().TempCelcius;
                case SensorType.Humidity:
                    _logger.Info(String.Format("TemperatureHumiditySensor.Measure Humidity"));
                    return _sensor.ReadData().Humidity;
                default:
                    throw new NotImplementedException();
            }

        }
    }
}
