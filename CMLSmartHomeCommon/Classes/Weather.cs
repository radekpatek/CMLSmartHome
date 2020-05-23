using System;
using System.Collections.Generic;
using System.Text;

namespace CMLSmartHomeCommon.Classes
{
    public class Weather
    {
        /// <summary>
        /// Výpočet hodnoty rosného bodu
        /// </summary>
        /// <param name="relativeHumidity"></param>
        /// <param name="temperature"></param>
        /// <returns></returns>
        public static double DewpointTemperatureCalculate(double relativeHumidity, double temperature)
        {
            double dewpointTemperature;

            double VapourPressureValue = relativeHumidity * 0.01 * Math.Exp((17.67 * temperature) / (temperature + 243.5));
            double Numerator = 243.5 * Math.Log(VapourPressureValue);
            double Denominator = 17.67 - (Math.Log(VapourPressureValue));
            dewpointTemperature = Math.Round( Numerator / Denominator, 1);

            return dewpointTemperature;
        }
    }
}
