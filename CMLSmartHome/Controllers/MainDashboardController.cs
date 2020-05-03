using System.Linq;
using CMLSmartHomeController.Models;
using CMLSmartHomeCommon.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using static CMLSmartHomeCommon.Model.WeatherForecast;

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

                        mainDashboard.TemperatureForecast = new MainDashboard.HourlyForecast();
                        mainDashboard.TemperatureForecast.Hour = hourlyForecast.Select(t => t.DateTime.ToString("HH")).ToArray();
                        mainDashboard.TemperatureForecast.Values = hourlyForecast.Select(t => t.Temperature).ToArray();

                        mainDashboard.PrecipitationForecast = new MainDashboard.HourlyForecast();
                        mainDashboard.PrecipitationForecast.Hour = hourlyForecast.Select(t => t.DateTime.ToString("HH")).ToArray();
                        mainDashboard.PrecipitationForecast.Values = hourlyForecast.Select(t => t.Rain + t.Snow).ToArray();
                    }

                    // Datum a čas sestavení boardu
                    mainDashboard.GenerationDateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

                }
            }
      
            return mainDashboard;
        }

        // GET Graphs
        [Route("{Graphs}")]
        public DashboardGraphs GetGraps()
        {
            var graphs = new DashboardGraphs();

            // Graf venkovní teploty
            graphs.OutdoorTemperatureGraph = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff,0xff, 0xff, 0xff, 0xff, 0xff };

            //return System.Text.Encoding.Default.GetString(graphs.OutdoorTemperatureGraph); -- bude-li potřeba posílat jako string
            return graphs;
        }

    }
}
