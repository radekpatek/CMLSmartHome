
using CMLSmartHomeCommon.Enums;

namespace CMLSmartHomeCollector.Classes.Sensors
{
    /// <summary>
    /// Humidity sensor
    /// </summary>
    public class HumiditySensor : SensorCollector
    {
        /// <summary>
        /// Constructor of Humidity sensor
        /// </summary>
        /// <param name="readingFrequency"></param>
        public HumiditySensor(int readingFrequency)
        {
            Type = SensorType.Humidity;
            Unit = UnitType.Percent;
            ReadingFrequency = readingFrequency;
        }

        /// <summary>
        /// Measures the value of the sensor
        /// </summary>
        /// <returns></returns>
        public override double Measure()
        {
            return 10;
        }
    }
}
