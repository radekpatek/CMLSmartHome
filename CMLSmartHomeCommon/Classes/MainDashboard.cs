using CMLSmartHomeCommon.Model;

namespace CMLSmartHomeCommon.Classes
{
    /// <summary>
    /// Hlavní nástěnka pro zobtazení hlavních ukazatelů
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
        public string GenerationDateTime;

        /// <summary>
        /// Datum a čas východu slunce
        /// </summary>
        public string Sunrise;

        /// <summary>
        /// Datum a čas západu slunce
        /// </summary>
        public string Sunset;

        /// <summary>
        /// Venkovní teplota rosného bodu
        /// </summary>
        public double OutdoorDewpointTemperature;

        /// <summary>
        /// Vnitřní teplota rosného bodu
        /// </summary>
        public double IndoorDewpointTemperature;

        /// <summary>
        /// Předpověd teploty po hodinách
        /// </summary>
        public HourlyForecast TemperatureForecast;

        /// <summary>
        /// Předpověd srážek po hodinách
        /// </summary>
        public HourlyForecast PrecipitationForecast;

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


}
