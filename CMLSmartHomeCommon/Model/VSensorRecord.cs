using CMLSmartHomeCommon.Enums;

namespace CMLSmartHomeController.Model
{
    /// <summary>
    /// Archív záznam měření senzorem
    /// </summary>
    public class VSensorRecord
    {
        /// <summary>
        /// Jednoznačný identifikátor měření
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Jednoznačný identifikátor senzoru
        /// </summary>
        public long SensorId { get; set; }

        /// <summary>
        /// Jednoznačný identifikátor kolektoru senzorů 
        /// </summary>
        public long CollectorId { get; set; }

        /// <summary>
        /// Hodnota měření
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Jenotka měření
        /// </summary>
        public UnitType Unit { get; set; }

        /// <summary>
        /// Datum měření
        /// </summary>
        public System.DateTime DateTime { get; set; }


    }
}