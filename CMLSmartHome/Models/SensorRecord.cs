using CMLSmartHome.Enums;

namespace CMLSmartHome.Models
{
    /// <summary>
    /// Záznam měření senzorem
    /// </summary>
    public class SensorRecord
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

}
}
