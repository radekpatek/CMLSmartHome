using System.Collections.Generic;

namespace CMLSmartHomeCommon.Classes
{
    /// <summary>
    /// Hodnoty kolektorů
    /// </summary>
    public class CollectorsValues
    {
        /// <summary>
        /// Hodnoty kolektorů
        /// </summary>
        public CollectorValues[] Collectors;
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