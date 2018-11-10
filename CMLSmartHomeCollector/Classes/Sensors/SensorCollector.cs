using System;
using CMLSmartHomeCommon.Enums;
using CMLSmartHomeCommon.Model;

namespace CMLSmartHomeCollector.Sensors
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
        /// Jenotka měření
        /// </summary>
        public UnitType Unit { get; set; }

        /// <summary>
        /// Measures the value of the sensor
        /// </summary>
        /// <returns></returns>
        public abstract double Measure();

        /// <summary>
        /// Measures the value of the sensor with the sensor type
        /// </summary>
        /// <returns></returns>
        public abstract double Measure(SensorType type);

        public void Synchronize(Sensor sensor)
        {

        }
    }
}
