using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMLSmartHomeCommon.Model
{
    /// <summary>
    /// Záznam z přepodovědi počasí
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Jednoznačný identifikátor záznamu
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Datum záznamu
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Aktuální stav počasí
        /// </summary>
        public CurrentState Current { get; set; }

        /// <summary>
        /// Předpověď počasí - po hodinách
        /// </summary>
        public virtual ICollection<HourlyState> Hourly { get; set; }

        /// <summary>
        /// Předpověď počasí - po dnech
        /// </summary>
        public virtual ICollection<DailyState> Daily { get; set; }

         public class Weather
        {
            /// <summary>
            /// Jednoznačný identifikátor záznamu
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            /// Identifikace počasí
            /// </summary>
            public int WeatherConditionId { get; set; }

            /// <summary>
            /// Převládající počasí (Rain, Snow, Extreme etc)
            /// </summary>
            public string Main { get; set; }

            /// <summary>
            /// Popis počasí
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// Ikona stavu počasí
            /// </summary>
            public string Icon { get; set; }

        }


        /// <summary>
        /// Denní teploty
        /// </summary>
        public class DailyTemp
        {
            /// <summary>
            /// Jednoznačný identifikátor záznamu
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            /// Záznam denního stavu
            /// </summary>
            public DailyState DailyStateTemperature { get; set; }

            /// <summary>
            /// ID záznamu denního stavu
            /// </summary>
            public long? DailyStateTemperatureId { get; set; }
         
            /// <summary>
            /// Záznam denního stavu
            /// </summary>
            public DailyState DailyStateFeelsLikeTemperature { get; set; }

            /// <summary>
            /// ID záznamu denního stavu
            /// </summary>
            public long? DailyStateFeelsLikeTemperatureId { get; set; }
          
            /// <summary>
            /// Denní teplota
            /// </summary>
            public double DayTemperature { get; set; }

            /// <summary>
            /// Minimální denní teplota
            /// </summary>
            public double MinDailyTemperature { get; set; }

            /// <summary>
            /// Maximální denní teplota
            /// </summary>
            public double MaxDailyTemperature { get; set; }

            /// <summary>
            /// Noční teplota
            /// </summary>
            public double NightTemperature { get; set; }

            /// <summary>
            /// Večerní teplota
            /// </summary>
            public double EveningTemperature { get; set; }

            /// <summary>
            /// Ranní teplota
            /// </summary>
            public double MorningTemperature { get; set; }
        }

        public class State
        {
            /// <summary>
            /// Jednoznačný identifikátor záznamu
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            /// Záznam z přepodovědi počasí
            /// </summary>
            public WeatherForecast WeatherForecast { get; set; }

            /// <summary>
            /// ID záznamu z přepodovědi počasí
            /// </summary>
            public long WeatherForecastId { get; set; }

            /// <summary>
            /// Datum a čas měření
            /// </summary>
            public DateTime DateTime { get; set; }

            /// <summary>
            /// Teplota rosného bodu 
            /// </summary>
            public double AtmosphericTemperature { get; set; }

            /// <summary>
            /// Tlak
            /// </summary>
            public int Pressure { get; set; }

            /// <summary>
            /// Vlhkost
            /// </summary>
            public int Humidity { get; set; }

            /// <summary>
            /// Oblačnost (m)
            /// </summary>
            public int Cloudiness { get; set; }

            /// <summary>
            /// Rychlost větru (m/s)
            /// </summary>
            public double WindSpeed { get; set; }

            /// <summary>
            /// Směr větru (stupně)
            /// </summary>
            public int WindDirection { get; set; }

        }


        public class CurrentState : State
        {
            /// <summary>
            /// Datum a čas východu Slunce 
            /// </summary>
            public DateTime SunriseTime { get; set; }

            /// <summary>
            /// Datum a čas západu Slunce 
            /// </summary>
            public DateTime SunsetTime { get; set; }

            /// <summary>
            /// Teplota
            /// </summary>
            public double Temperature { get; set; }

            /// <summary>
            /// Pocitová teplota
            /// </summary>
            public double FeelsLikeTemperature { get; set; }

            /// <summary>
            /// UV Index
            /// </summary>
            public double UVIndex { get; set; }

            /// <summary>
            /// Průměrná viditelnost (m)
            /// </summary>
            public int AverageVisibility { get; set; }

            /// <summary>
            /// Počasí
            /// </summary>
            public virtual ICollection<Weather> WeatherList { get; set; }

            /// <summary>
            /// Dešťové srážky (mm)
            /// </summary>
            public double? Rain { get; set; }

            /// <summary>
            /// Sněhové srážky (mm)
            /// </summary>
            public double? Snow { get; set; }
        }

        public class HourlyState : State
        {
            /// <summary>
            /// Teplota
            /// </summary>
            public double Temperature { get; set; }

            /// <summary>
            /// Pocitová teplota
            /// </summary>
            public double FeelsLikeTemperature { get; set; }

            /// <summary>
            /// Počasí
            /// </summary>
            public virtual ICollection<Weather> WeatherList { get; set; }

            /// <summary>
            /// Dešťové srážky (mm)
            /// </summary>
            public double Rain { get; set; }

            /// <summary>
            /// Sněhové srážky (mm)
            /// </summary>
            public double Snow { get; set; }
        }

        public class DailyState : State
        {
            
            /// <summary>
            /// Datum a čas východu Slunce 
            /// </summary>
            public DateTime SunriseTime { get; set; }

            /// <summary>
            /// Datum a čas západu Slunce 
            /// </summary>
            public DateTime SunsetTime { get; set; }

            /// <summary>
            /// Teplota
            /// </summary>
            public DailyTemp Temperature { get; set; }
/*
            /// <summary>
            /// ID teploty
            /// </summary>
            public long? TemperatureId { get; set; }
  */          
            /// <summary>
            /// Pocitová teplota
            /// </summary>
            public DailyTemp FeelsLikeTemperature { get; set; }
/*
            /// <summary>
            /// ID pocitové teploty
            /// </summary>
            public long? FeelsLikeTemperatureId { get; set; }
  */          

            /// <summary>
            /// UV Index
            /// </summary>
            public double UVIndex { get; set; }

            /// <summary>
            /// Průměrná viditelnost (m)
            /// </summary>
            public int AverageVisibility { get; set; }

            /// <summary>
            /// Počasí
            /// </summary>
            public virtual ICollection<Weather> WeatherList { get; set; }

            /// <summary>
            /// Dešťové srážky (mm)
            /// </summary>
            public double Rain { get; set; }

            /// <summary>
            /// Sněhové srážky (mm)
            /// </summary>
            public double Snow { get; set; }
        }

    }
}
