
using CMLSmartHomeCommon.Enums;

namespace CMLSmartHomeCollector.Classes.Sensors
{
    /// <summary>
    /// Temperature sensor
    /// </summary>
    public class TemperatureSensor : SensorCollector
    {
        /// <summary>
        /// Constructor of Temperature sensor
        /// </summary>
        /// <param name="readingFrequency"></param>
        public TemperatureSensor(int readingFrequency)
        {
            Type = SensorType.Temperature;
            Unit = UnitType.CelsiusDegree;
            ReadingFrequency = readingFrequency;
        }

        /// <summary>
        /// Measures the value of the sensor
        /// </summary>
        /// <returns></returns>
        public override double Measure()
        {
            return 20;
        }
    }
}
