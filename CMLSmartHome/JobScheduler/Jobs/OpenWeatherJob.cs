using CMLSmartHomeCommon.Model;
using CMLSmartHomeController.JobScheduler.Jobs.OpenWeather;
using CMLSmartHomeController.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static CMLSmartHomeCommon.Model.WeatherForecast;

namespace CMLSmartHomeController.JobScheduler.Jobs
{
    /// <summary>
    /// OpenWeatherJob
    /// </summary>
    [DisallowConcurrentExecution]
    public class OpenWeatherJob : IJob
    {
        // Inject the DI provider
        // private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _provider;
        private readonly ILogger<OpenWeatherJob> _logger;

        public OpenWeatherJob(ILogger<OpenWeatherJob> logger, IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // Create a new scope
            var scope = _provider.CreateScope();

            // Resolve the Scoped service
            var _context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            _logger.LogInformation("OpenWeatherJob - Started");

            try
            {
                using HttpClient httpClient = new();

                const string appid = "49778d403a36e87cf3f7f5b430570a06";
                const string units = "metric";
                const string lon = "14.86";
                const string lat = "50.07";

                string url = string.Format("http://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&units={2}&APPID={3}", lat, lon, units, appid);

                var json = await httpClient.GetStringAsync(url);

                var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo.Root>(json);
                var dashboard = _context.Dashboards.Include(t => t.OutdoorCollector.Sensors)
                                   .First();

                if (weatherInfo != null)
                {
                    var weatherForecast = _context.WeatherForecast
                        .Include(t => t.Current)
                        .Include(t => t.Current.WeatherList)
                        .Include(t => t.Hourly)
                        .Include(t => t.Daily)
                        .OrderBy(t => t.Id)
                        .FirstOrDefault();

                    if (weatherForecast != null)
                    {
                        _context.WeatherForecast.Remove(weatherForecast);
                        _context.SaveChanges();
                    }

                    weatherForecast = new WeatherForecast();

                    weatherForecast.DateTime = DateTime.Now;

                    // CurrentState
                    if (weatherForecast.Current == null)
                    {
                        weatherForecast.Current = new WeatherForecast.CurrentState();
                    }
                    weatherForecast.Current.WeatherForecast = weatherForecast;
                    weatherForecast.Current.WeatherForecastId = weatherForecast.Id;
                    weatherForecast.Current.DateTime = DateTimeOffset.FromUnixTimeSeconds(weatherInfo.Current.DateTime).LocalDateTime;
                    weatherForecast.Current.SunriseTime = DateTimeOffset.FromUnixTimeSeconds(weatherInfo.Current.SunriseTime).LocalDateTime;
                    weatherForecast.Current.SunsetTime = DateTimeOffset.FromUnixTimeSeconds(weatherInfo.Current.SunsetTime).LocalDateTime;
                    weatherForecast.Current.Temperature = weatherInfo.Current.Temperature;
                    weatherForecast.Current.FeelsLikeTemperature = weatherInfo.Current.FeelsLikeTemperature;
                    weatherForecast.Current.Pressure = weatherInfo.Current.Pressure;
                    weatherForecast.Current.Humidity = weatherInfo.Current.Humidity;
                    weatherForecast.Current.AtmosphericTemperature = weatherInfo.Current.AtmosphericTemperature;
                    weatherForecast.Current.UVIndex = weatherInfo.Current.UVIndex;
                    weatherForecast.Current.Cloudiness = weatherInfo.Current.Cloudiness;
                    weatherForecast.Current.AverageVisibility = weatherInfo.Current.AverageVisibility;
                    weatherForecast.Current.WindSpeed = weatherInfo.Current.WindSpeed;
                    weatherForecast.Current.WindDirection = weatherInfo.Current.WindDirection;
                    weatherForecast.Current.Rain = weatherInfo.Current.Rain?.Per1h;
                    weatherForecast.Current.Snow = weatherInfo.Current.Snow?.Per1h;

                    if (weatherInfo.Current.WeatherList != null)
                    {
                        if (weatherForecast.Current.WeatherList == null)
                        {
                            weatherForecast.Current.WeatherList = new List<Weather>();
                        }

                        foreach (var weather in weatherInfo.Current.WeatherList)
                        {
                            var weatherInformation = new Weather();

                            weatherInformation.WeatherConditionId = weather.Id;
                            weatherInformation.Main = weather.Main;
                            weatherInformation.Description = weather.Description;
                            weatherInformation.Icon = weather.Icon;

                            weatherForecast.Current.WeatherList.Add(weatherInformation);
                        }

                    }

                    //HourlyState
                    if (weatherForecast.Hourly == null)
                    {
                        weatherForecast.Hourly = new List<WeatherForecast.HourlyState>();
                    }


                    foreach (var hourlyState in weatherInfo.Hourly)
                    {
                        var hourly = new WeatherForecast.HourlyState();

                        hourly.AtmosphericTemperature = hourlyState.AtmosphericTemperature;
                        hourly.Cloudiness = hourlyState.Cloudiness;
                        hourly.DateTime = DateTimeOffset.FromUnixTimeSeconds(hourlyState.DateTime).LocalDateTime;
                        hourly.FeelsLikeTemperature = hourlyState.FeelsLikeTemperature;
                        hourly.Humidity = hourlyState.Humidity;
                        hourly.Pressure = hourlyState.Pressure;
                        hourly.Temperature = hourlyState.Temperature;
                        hourly.WindDirection = hourlyState.WindDirection;
                        hourly.WindSpeed = hourlyState.WindSpeed;
                        hourly.Rain = hourlyState.Rain == null ? 0 : hourlyState.Rain.Per1h;
                        hourly.Snow = hourlyState.Snow == null ? 0 : hourlyState.Snow.Per1h;

                        if (hourlyState.WeatherList != null)
                        {
                            hourly.WeatherList = new List<Weather>();

                            foreach (var weather in hourlyState.WeatherList)
                            {
                                var weatherInformation = new Weather();

                                weatherInformation.WeatherConditionId = Convert.ToInt32(weather.Id);
                                weatherInformation.Main = weather.Main;
                                weatherInformation.Description = weather.Description;
                                weatherInformation.Icon = weather.Icon;

                                hourly.WeatherList.Add(weatherInformation);
                            }
                        }

                        weatherForecast.Hourly.Add(hourly);
                    }

                    //Daily
                    if (weatherForecast.Daily == null)
                    {
                        weatherForecast.Daily = new List<DailyState>();
                    }

                    foreach (var dailyState in weatherInfo.Daily)
                    {
                        var daily = new DailyState();

                        daily.AtmosphericTemperature = dailyState.AtmosphericTemperature;
                        daily.AverageVisibility = dailyState.AverageVisibility;
                        daily.Cloudiness = dailyState.Cloudiness;
                        daily.DateTime = DateTimeOffset.FromUnixTimeSeconds(dailyState.DateTime).LocalDateTime;

                        if (dailyState.FeelsLikeTemperature != null)
                        {
                            daily.FeelsLikeTemperature = new WeatherForecast.DailyTemp();
                            daily.FeelsLikeTemperature.DayTemperature = dailyState.FeelsLikeTemperature.DayTemperature;
                            daily.FeelsLikeTemperature.EveningTemperature = dailyState.FeelsLikeTemperature.EveningTemperature;
                            daily.FeelsLikeTemperature.MaxDailyTemperature = dailyState.FeelsLikeTemperature.MaxDailyTemperature;
                            daily.FeelsLikeTemperature.MinDailyTemperature = dailyState.FeelsLikeTemperature.MinDailyTemperature;
                            daily.FeelsLikeTemperature.MorningTemperature = dailyState.FeelsLikeTemperature.MorningTemperature;
                            daily.FeelsLikeTemperature.NightTemperature = dailyState.FeelsLikeTemperature.NightTemperature;
                        }

                        daily.Humidity = dailyState.Humidity;
                        daily.Pressure = dailyState.Pressure;
                        daily.Rain = dailyState.Rain;
                        daily.Snow = dailyState.Snow;
                        daily.SunriseTime = DateTimeOffset.FromUnixTimeSeconds(dailyState.SunriseTime).LocalDateTime;
                        daily.SunsetTime = DateTimeOffset.FromUnixTimeSeconds(dailyState.SunsetTime).LocalDateTime;
                        if (dailyState.Temperature != null)
                        {
                            daily.Temperature = new WeatherForecast.DailyTemp();
                            daily.Temperature.DayTemperature = dailyState.Temperature.DayTemperature;
                            daily.Temperature.EveningTemperature = dailyState.Temperature.EveningTemperature;
                            daily.Temperature.MaxDailyTemperature = dailyState.Temperature.MaxDailyTemperature;
                            daily.Temperature.MinDailyTemperature = dailyState.Temperature.MinDailyTemperature;
                            daily.Temperature.MorningTemperature = dailyState.Temperature.MorningTemperature;
                            daily.Temperature.NightTemperature = dailyState.Temperature.NightTemperature;
                        }

                        daily.UVIndex = dailyState.UVIndex;
                        if (dailyState.WeatherList != null)
                        {
                            daily.WeatherList = new List<Weather>();

                            foreach (var weather in dailyState.WeatherList)
                            {
                                var weatherInformation = new Weather();

                                weatherInformation.WeatherConditionId = Convert.ToInt32(weather.Id);
                                weatherInformation.Main = weather.Main;
                                weatherInformation.Description = weather.Description;
                                weatherInformation.Icon = weather.Icon;

                                daily.WeatherList.Add(weatherInformation);
                            }
                        }
                        daily.WindDirection = dailyState.WindDirection;
                        daily.WindSpeed = dailyState.WindSpeed;

                        weatherForecast.Daily.Add(daily);
                    }


                    _context.WeatherForecast.Add(weatherForecast);
                    _context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("OpenWeatherJob - Error: {0} ", ex.Message));
            }

            _logger.LogInformation("OpenWeatherJob - Finished");
        }
    }
}







