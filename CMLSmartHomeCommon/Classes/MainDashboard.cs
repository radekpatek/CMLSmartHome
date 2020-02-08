using CMLSmartHomeCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMLSmartHomeCommon.Classes
{
    /// <summary>
    /// Hlavní nástěnka pro zobtazení hlavních uakazatelů
    /// </summary>
    public class MainDashboard
    {

        /// <summary>
        /// Venkovní hodnoty sebsorů
        /// </summary>
        public SensorValue[] OutdoorSensorsValue;

        /// <summary>
        /// Vnitřní hodnoty
        /// </summary>
        public CollectorValues[] IndoorCollectors;

        /// <summary>
        ///  Datum a čas sestavení boardu
        /// </summary>
        public String GenerationDateTime;


    }

    /// <summary>
    /// Záznam na sensoru
    /// </summary>
    public class SensorValue
    {
        /// <summary>
        /// Sensor
        /// </summary>
        public Sensor Sensor;

        /// <summary>
        /// Hodnota
        /// </summary>
        public double Value;
    }

    /// <summary>
    /// Záznam na sensoru
    /// </summary>
    public class CollectorValues
    {
        /// <summary>
        /// Umístnění zařízení se senzory
        /// </summary>
        public string Location;

        /// <summary>
        /// Seznam senzorů
        /// </summary>
        public ICollection<SensorValue> Sensors { get; set; } = new List<SensorValue>();
    }

}
