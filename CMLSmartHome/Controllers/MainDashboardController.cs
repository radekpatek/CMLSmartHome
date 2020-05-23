using System.Linq;
using CMLSmartHomeController.Models;
using CMLSmartHomeCommon.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using CMLSmartHomeController.Chart;
using System.Drawing;

namespace CMLSmartHomeController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainDashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MainDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MainDashboard
        [HttpGet]
        public MainDashboard Get()
        {
            var mainDashboard = new MainDashboard();

            if (_context.Dashboards.Count() > 0)
            {
                var dashboard = _context.Dashboards.Include(t => t.OutdoorCollector.Sensors)
                                   .First();

                if (dashboard != null)
                {
                    // Venkovní senzory
                    var outdoorSensors = new List<SensorValue>();
                    foreach (var sensor in dashboard.OutdoorCollector.Sensors)
                    {
                        var sv = new SensorValue();
                        sv.Sensor = sensor;
                        sv.Value = _context.SensorRecords.Where(t => t.SensorId == sensor.Id).LastOrDefault().Value;

                        outdoorSensors.Add(sv);
                    }
                    mainDashboard.OutdoorSensorsValue = outdoorSensors.ToArray();

                    // Vnitřní senzory
                    var IndoorCollectors = new List<CollectorValues>();
                    foreach (var collector in _context.Collectors.Where(t => t.Id != dashboard.OutdoorCollector.Id).Include(t => t.Sensors))
                    {
                        var collectorValues = new CollectorValues();
                        var IndoorSensorValues = new List<SensorValue>();
                        foreach (var sensor in collector.Sensors)
                        {
                            var sv = new SensorValue();
                            sv.Sensor = sensor;
                            sv.Value = _context.SensorRecords.Where(t => t.SensorId == sensor.Id).LastOrDefault().Value;

                            IndoorSensorValues.Add(sv);
                        }
                        collectorValues.Sensors = IndoorSensorValues;
                        collectorValues.Location = collector.Name;
                        IndoorCollectors.Add(collectorValues);
                    }
                    mainDashboard.IndoorCollectors = IndoorCollectors.ToArray();

                    // Přepověď teploty a srážek
                    if (_context.WeatherForecast != null)
                    {
                        var weatherForecastId = _context.WeatherForecast.LastOrDefault().Id;

                        var hourlyForecast = _context.WeatherForecastHourlyState.Where(t => t.WeatherForecastId == weatherForecastId)
                            .OrderBy(m => m.DateTime);

                        if (hourlyForecast != null)
                        { 
                            mainDashboard.TemperatureForecast = new MainDashboard.HourlyForecast();
                            mainDashboard.TemperatureForecast.Hour = hourlyForecast.Select(t => t.DateTime.Hour.ToString()).ToArray();
                            mainDashboard.TemperatureForecast.Values = hourlyForecast.Select(t => t.Temperature).ToArray();

                            mainDashboard.PrecipitationForecast = new MainDashboard.HourlyForecast();
                            mainDashboard.PrecipitationForecast.Hour = hourlyForecast.Select(t => t.DateTime.Hour.ToString()).ToArray();
                            mainDashboard.PrecipitationForecast.Values = hourlyForecast.Select(t => t.Rain + t.Snow).ToArray();
                        }

                        var currentState = _context.WeatherForecastCurrentState.LastOrDefault();

                        if (currentState != null)
                        {
                            mainDashboard.Sunrise = currentState.SunriseTime.ToString("HH:mm");
                            mainDashboard.Sunset = currentState.SunsetTime.ToString("HH:mm");
                        }
                    }

                    // Teplota rosného bodu - v jídelně
                    var indoorDewPointCollector = _context.Collectors.Where(t => t.Id == 1).Include(t => t.Sensors).FirstOrDefault();

                    if (indoorDewPointCollector != null)
                    {
                        var indoorTemperatureSensor = indoorDewPointCollector.Sensors.Where(t => t.Type == CMLSmartHomeCommon.Enums.SensorType.Temperature).FirstOrDefault();
                        var indoorHumaditySensor = indoorDewPointCollector.Sensors.Where(t => t.Type == CMLSmartHomeCommon.Enums.SensorType.Humidity).FirstOrDefault();

                        var indoorTemperature = _context.SensorRecords.Where(t => t.SensorId == indoorTemperatureSensor.Id).LastOrDefault().Value;
                        var indoorHumadity = _context.SensorRecords.Where(t => t.SensorId == indoorHumaditySensor.Id).LastOrDefault().Value;

                        mainDashboard.IndoorDewpointTemperature = Weather.DewpointTemperatureCalculate(indoorHumadity, indoorTemperature);
                    }

                    // Teplota rosného bodu - venkovní
                    var outdoorTemperatureSensor = dashboard.OutdoorCollector.Sensors.Where(t => t.Type == CMLSmartHomeCommon.Enums.SensorType.Temperature).FirstOrDefault();
                    var outdoorHumaditySensor = dashboard.OutdoorCollector.Sensors.Where(t => t.Type == CMLSmartHomeCommon.Enums.SensorType.Humidity).FirstOrDefault();

                    var outdoorTemperature = _context.SensorRecords.Where(t => t.SensorId == outdoorTemperatureSensor.Id).LastOrDefault().Value;
                    var outdoorHumadity = _context.SensorRecords.Where(t => t.SensorId == outdoorHumaditySensor.Id).LastOrDefault().Value;

                        mainDashboard.OutdoorDewpointTemperature = Weather.DewpointTemperatureCalculate(outdoorHumadity, outdoorTemperature);

                    // Datum a čas sestavení boardu
                    mainDashboard.GenerationDateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

                }
            }

            return mainDashboard;
        }

        // GET Graphs
        [HttpGet]
        [Route("Graphs")]
        public DashboardGraphs GetGraphs()
        {
            var graphs = new DashboardGraphs();

            if (_context.Dashboards.Count() > 0)
            {
                // Přepověď teploty a srážek
                if (_context.WeatherForecast != null)
                {
                    var weatherForecastId = _context.WeatherForecast.LastOrDefault().Id;

                    var hourlyForecast = _context.WeatherForecastHourlyState.Where(t => t.WeatherForecastId == weatherForecastId)
                        .OrderBy(m => m.DateTime);

                    // Graf venkovní teploty a srážek
                    var cmlChart = new CMLChart();

                    //cmlChart.Label = "Předpověď počasí";

                    var horly = hourlyForecast.Select(t => t.DateTime.Hour.ToString()).ToArray();
                    cmlChart.XAsix = new CMLChartXAsix(horly, CMLValuesType.Hourly);

                    var temperatureForecastValues = hourlyForecast.Select(t => t.Temperature).ToArray();
                    var precipitationForecastValues = hourlyForecast.Select(t => t.Rain + t.Snow).ToArray();

                    cmlChart.YAsixs = new List<CMLChartYAsix>();
                    cmlChart.YAsixs.Add(new CMLChartYAsix("Teplota (°C)", true, Color.Black, Color.Black, temperatureForecastValues, 1, PresentationType.Line, Location.Left));
                    cmlChart.YAsixs.Add(new CMLChartYAsix("Srážky (mm)", true, Color.Black, Color.Black, precipitationForecastValues, 2, PresentationType.Bar, Location.Right));

                    // Nastavení maximalní hodnoty na ose Y pro výše srážek. Ostaní min/max jsou defaultně min/max hodnoty
                    cmlChart.YAsixs.Where(t => t.Id == 1).FirstOrDefault().MinValue = 0;
                    cmlChart.YAsixs.Where(t => t.Id == 2).FirstOrDefault().MaxValue = 4;

                    // :TODO: nastavit hodnoty velikosti obrázku grafu 
                    int width = 400;
                    int height = 200;
                    var chartImageBitmap = cmlChart.GetBitmap(width, height, true);

                    var bitmapConvert = new BitmapConvert(chartImageBitmap);
                    graphs.OutdoorTemperatureGraphByte = bitmapConvert.GetBitmapByByteArray();
                }
            }
            return graphs;

        }
        [HttpGet]
        [Route("GraphsString")]
        public DashboardGraphString GetGraphsString()
        {
            var graphs = new DashboardGraphString();

            if (_context.Dashboards.Count() > 0)
            {
                // Přepověď teploty a srážek
                if (_context.WeatherForecast != null)
                {
                    var weatherForecastId = _context.WeatherForecast.LastOrDefault().Id;

                    var hourlyForecast = _context.WeatherForecastHourlyState.Where(t => t.WeatherForecastId == weatherForecastId)
                        .OrderBy(m => m.DateTime);

                    // Graf venkovní teploty a srážek
                    var cmlChart = new CMLChart();

                    //cmlChart.Label = "Předpověď počasí";

                    var horly = hourlyForecast.Select(t => t.DateTime.Hour.ToString()).ToArray();
                    cmlChart.XAsix = new CMLChartXAsix(horly, CMLValuesType.Hourly);

                    var temperatureForecastValues = hourlyForecast.Select(t => t.Temperature).ToArray();
                    var precipitationForecastValues = hourlyForecast.Select(t => t.Rain + t.Snow).ToArray();

                    cmlChart.YAsixs = new List<CMLChartYAsix>();
                    cmlChart.YAsixs.Add(new CMLChartYAsix("Teplota (°C)", true, Color.Black, Color.Black, temperatureForecastValues, 1, PresentationType.Line, Location.Left));
                    cmlChart.YAsixs.Add(new CMLChartYAsix("Srážky (mm)", true, Color.Black, Color.Black, precipitationForecastValues, 2, PresentationType.Bar, Location.Right));

                    // Nastavení maximalní hodnoty na ose Y pro výše srážek. Ostaní min/max jsou defaultně min/max hodnoty
                    cmlChart.YAsixs.Where(t => t.Id == 1).FirstOrDefault().MinValue = 0;
                    cmlChart.YAsixs.Where(t => t.Id == 2).FirstOrDefault().MaxValue = 4;

                    // :TODO: nastavit hodnoty velikosti obrázku grafu 
                    int width = 400;
                    int height = 200;
                    var chartImageBitmap = cmlChart.GetBitmap(width, height, true);

                    var bitmapConvert = new BitmapConvert(chartImageBitmap);
                    graphs.OutdoorTemperatureGraphString = bitmapConvert.GetBitmapByString();
                }
            }
            return graphs;
        }

    }
}
