using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMLSmartHomeController.JobScheduler.Jobs.OpenWeather
{
    /// <summary>
    /// Struktura dat stavu a předpovědí počasí služby OpenWeatherMap (One Call API)
    /// https://openweathermap.org/api/one-call-api
    /// </summary>
    public class WeatherInfo
    {
        public class Weather
        {
            /// <summary>
            /// Identifikace počasí
            /// </summary>
            [JsonProperty("id")]
            public int Id { get; set; }

            /// <summary>
            /// Převládající počasí (Rain, Snow, Extreme etc)
            /// </summary>
            [JsonProperty("main")]
            public string Main { get; set; }

            /// <summary>
            /// Popis počasí
            /// </summary>
            [JsonProperty("description")]
            public string Description { get; set; }

            /// <summary>
            /// Ikona stavu počasí
            /// </summary>
            [JsonProperty("icon")]
            public string Icon { get; set; }
        }

        /// <summary>
        /// Precipitation
        /// </summary>
        public class Precipitation
        {
            /// <summary>
            /// Výše srážek (mm)
            /// </summary>
            [JsonProperty("1h")]
            public double Per1h { get; set; }

        }

        /// <summary>
        /// Denní teploty
        /// </summary>
        public class DailyTemp
        {
            /// <summary>
            /// Denní teplota
            /// </summary>
            [JsonProperty("day")]
            public double DayTemperature { get; set; }

            /// <summary>
            /// Minimální denní teplota
            /// </summary>
            [JsonProperty("min")]
            public double MinDailyTemperature { get; set; }

            /// <summary>
            /// Maximální denní teplota
            /// </summary>
            [JsonProperty("max")]
            public double MaxDailyTemperature { get; set; }

            /// <summary>
            /// Noční teplota
            /// </summary>
            [JsonProperty("night")]
            public double NightTemperature { get; set; }

            /// <summary>
            /// Večerní teplota
            /// </summary>
            [JsonProperty("eve")]
            public double EveningTemperature { get; set; }

            /// <summary>
            /// Ranní teplota
            /// </summary>
            [JsonProperty("morn")]
            public double MorningTemperature { get; set; }
        }

        public class CurrentState
        {
            /// <summary>
            /// Datum a čas měření
            /// </summary>
            [JsonProperty("dt")]
            public long DateTime { get; set; }

            /// <summary>
            /// Datum a čas východu Slunce 
            /// </summary>
            [JsonProperty("sunrise")]
            public long SunriseTime { get; set; }

            /// <summary>
            /// Datum a čas západu Slunce 
            /// </summary>
            [JsonProperty("sunset")]
            public long SunsetTime { get; set; }

            /// <summary>
            /// Teplota
            /// </summary>
            [JsonProperty("temp")]
            public double Temperature { get; set; }

            /// <summary>
            /// Pocitová teplota
            /// </summary>
            [JsonProperty("feels_like")]
            public double FeelsLikeTemperature { get; set; }

            /// <summary>
            /// Tlak
            /// </summary>
            [JsonProperty("pressure")]
            public int Pressure { get; set; }

            /// <summary>
            /// Vlhkost
            /// </summary>
            [JsonProperty("humidity")]
            public int Humidity { get; set; }

            /// <summary>
            /// Teplota rosného bodu 
            /// </summary>
            [JsonProperty("dew_point")]
            public double AtmosphericTemperature { get; set; }

            /// <summary>
            /// UV Index
            /// </summary>
            [JsonProperty("uvi")]
            public double UVIndex { get; set; }

            /// <summary>
            /// Oblačnost (m)
            /// </summary>
            [JsonProperty("clouds")]
            public int Cloudiness { get; set; }

            /// <summary>
            /// Průměrná viditelnost (m)
            /// </summary>
            [JsonProperty("visibility")]
            public int AverageVisibility { get; set; }

            /// <summary>
            /// Rychlost větru (m/s)
            /// </summary>
            [JsonProperty("wind_speed")]
            public double WindSpeed { get; set; }

            /// <summary>
            /// Směr větru (stupně)
            /// </summary>
            [JsonProperty("wind_deg")]
            public int WindDirection { get; set; }

            /// <summary>
            /// Počasí
            /// </summary>
            [JsonProperty("weather")]
            public List<Weather> WeatherList { get; set; }

            /// <summary>
            /// Dešťová srážky (mm)
            /// </summary>
            [JsonProperty("rain")]
            public Precipitation Rain { get; set; }

            /// <summary>
            /// Sněhové srážky (mm)
            /// </summary>
            [JsonProperty("snow")]
            public Precipitation Snow { get; set; }
        }


        public class HourlyState
        {
            /// <summary>
            /// Datum a čas měření
            /// </summary>
            [JsonProperty("dt")]
            public long DateTime { get; set; }

            /// <summary>
            /// Teplota
            /// </summary>
            [JsonProperty("temp")]
            public long Temperature { get; set; }

            /// <summary>
            /// Pocitová teplota
            /// </summary>
            [JsonProperty("feels_like")]
            public long FeelsLikeTemperature { get; set; }

            /// <summary>
            /// Tlak
            /// </summary>
            [JsonProperty("pressure")]
            public int Pressure { get; set; }

            /// <summary>
            /// Vlhkost
            /// </summary>
            [JsonProperty("humidity")]
            public int Humidity { get; set; }

            /// <summary>
            /// Teplota rosného bodu 
            /// </summary>
            [JsonProperty("dew_point")]
            public double AtmosphericTemperature { get; set; }

            /// <summary>
            /// Oblačnost (m)
            /// </summary>
            [JsonProperty("clouds")]
            public int Cloudiness { get; set; }

            /// <summary>
            /// Rychlost větru (m/s)
            /// </summary>
            [JsonProperty("wind_speed")]
            public double WindSpeed { get; set; }

            /// <summary>
            /// Směr větru (stupně)
            /// </summary>
            [JsonProperty("wind_deg")]
            public int WindDirection { get; set; }

            /// <summary>
            /// Počasí
            /// </summary>
            [JsonProperty("weather")]
            public virtual ICollection<Weather> WeatherList { get; set; }

            /// <summary>
            /// Dešťová srážky (mm)
            /// </summary>
            [JsonProperty("rain")]
            public Precipitation Rain { get; set; }

            /// <summary>
            /// Sněhové srážky (mm)
            /// </summary>
            [JsonProperty("snow")]
            public Precipitation Snow { get; set; }
        }


        public class DailyState
        {
            /// <summary>
            /// Datum a čas měření
            /// </summary>
            [JsonProperty("dt")]
            public long DateTime { get; set; }

            /// <summary>
            /// Datum a čas východu Slunce 
            /// </summary>
            [JsonProperty("sunrise")]
            public long SunriseTime { get; set; }

            /// <summary>
            /// Datum a čas západu Slunce 
            /// </summary>
            [JsonProperty("sunset")]
            public long SunsetTime { get; set; }

            /// <summary>
            /// Teplota
            /// </summary>
            [JsonProperty("temp")]
            public DailyTemp Temperature { get; set; }

            /// <summary>
            /// Pocitová teplota
            /// </summary>
            [JsonProperty("feels_like")]
            public DailyTemp FeelsLikeTemperature { get; set; }

            /// <summary>
            /// Tlak
            /// </summary>
            [JsonProperty("pressure")]
            public int Pressure { get; set; }

            /// <summary>
            /// Vlhkost
            /// </summary>
            [JsonProperty("humidity")]
            public int Humidity { get; set; }

            /// <summary>
            /// Teplota rosného bodu 
            /// </summary>
            [JsonProperty("dew_point")]
            public double AtmosphericTemperature { get; set; }

            /// <summary>
            /// UV Index
            /// </summary>
            [JsonProperty("uvi")]
            public double UVIndex { get; set; }

            /// <summary>
            /// Oblačnost (m)
            /// </summary>
            [JsonProperty("clouds")]
            public int Cloudiness { get; set; }

            /// <summary>
            /// Průměrná viditelnost (m)
            /// </summary>
            [JsonProperty("visibility")]
            public int AverageVisibility { get; set; }

            /// <summary>
            /// Rychlost větru (m/s)
            /// </summary>
            [JsonProperty("wind_speed")]
            public double WindSpeed { get; set; }

            /// <summary>
            /// Směr větru (stupně)
            /// </summary>
            [JsonProperty("wind_deg")]
            public int WindDirection { get; set; }

            /// <summary>
            /// Počasí
            /// </summary>
            [JsonProperty("weather")]
            public virtual ICollection<Weather> WeatherList { get; set; }

            /// <summary>
            /// Dešťové srážky (mm)
            /// </summary>
            [JsonProperty("rain")]
            public double Rain { get; set; }

            /// <summary>
            /// Sněhové srážky (mm)
            /// </summary>
            [JsonProperty("snow")]
            public double Snow { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// Souřadnice polohy požadovaného místa - zeměpisná šířka 
            /// </summary>
            [JsonProperty("lat")]
            public string Latitude;

            /// <summary>
            /// Souřadnice polohy požadovaného místa - zeměpisná délka
            /// </summary>
            [JsonProperty("lon")]
            public string Longitude;

            /// <summary>
            /// Časová zóna požadovaného místa
            /// </summary>
            [JsonProperty("timezone")]
            public string Timezone;

            /// <summary>
            /// Aktuální stav počasí
            /// </summary>
            [JsonProperty("current")]
            public CurrentState Current;

            /// <summary>
            /// Předpověď púočasí - po hodinách
            /// </summary>
            [JsonProperty("hourly")]
            public ICollection<HourlyState> Hourly;

            /// <summary>
            /// Předpověď púočasí - po dnech
            /// </summary>
            [JsonProperty("daily")]
            public ICollection<DailyState> Daily;

        }
    }
}
