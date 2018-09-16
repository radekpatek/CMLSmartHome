using CMLSmartHomeCommon.Enums;
using CMLSmartHomeCommon.Models;

namespace CMLSmartHomeCollector
{
    public abstract class SensorCollector
    {

        /// <summary>
        /// Unique sensor identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Sensor type
        /// </summary>
        public SensorType Type { get; set; }

        /// <summary>
        /// Sensor reading frequency. The value is given in seconds
        /// </summary>
        public int ReadingFrequency { get; set; }

        /// <summary>
        /// Jenotka měření
        /// </summary>
        public UnitType Unit { get; set; }

        /// <summary>
        /// Measures the value of the sensor
        /// </summary>
        /// <returns></returns>
        public abstract double Measure();

        public void Synchronize(Sensor sensor)
        {

        }
    }
}
