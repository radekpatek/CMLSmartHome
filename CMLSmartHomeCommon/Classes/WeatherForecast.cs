using System;

namespace CMLSmartHomeCommon.Classes
{
    /// <summary>
    /// Předpověď počasí
    /// </summary>
    public class WeatherForecast
    {
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
    /// Struktura hodinové předpovědi
    /// </summary>
    public struct HourlyForecast
    {
        /// <summary>
        /// Pořadí hodiny
        /// </summary>
        public String[] Hour;

        /// <summary>
        /// Hodnota předpovědi
        /// </summary>
        public double[] Values;
    }
}