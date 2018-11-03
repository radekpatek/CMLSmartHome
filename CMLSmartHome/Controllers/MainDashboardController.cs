using System.Linq;
using CMLSmartHomeController.Models;
using CMLSmartHomeCommon.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using CMLSmartHomeCommon.Enums;

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

            var dashboard = _context.Dashboards.Include(t => t.InternalCollector.Sensors).First();

            if (dashboard != null)
            {
                var InternalTemperaturSensor = dashboard.InternalCollector.Sensors.Where(l => l.Type == SensorType.Temperature).LastOrDefault();
                if (InternalTemperaturSensor != null)
                {
                    mainDashboard.InternalTemperature = _context.SensorRecords.Where(t => t.CollectorId == dashboard.InternalCollector.Id
                                                 && t.SensorId == InternalTemperaturSensor.Id).LastOrDefault()?.Value;
                }

                var InternalHumiditySensor = dashboard.InternalCollector.Sensors.Where(l => l.Type == SensorType.Humidity).LastOrDefault();
                if (InternalHumiditySensor != null)
                { 
                    mainDashboard.InternalHumidity = _context.SensorRecords.Where(t => t.CollectorId == dashboard.InternalCollector.Id
                                             && t.SensorId == InternalHumiditySensor.Id).LastOrDefault()?.Value;
                }

                var OutdoorTemperaturSensor = dashboard.InternalCollector.Sensors.Where(l => l.Type == SensorType.Temperature).LastOrDefault();
                if (OutdoorTemperaturSensor != null)
                {
                    mainDashboard.OutdoorTemperature = _context.SensorRecords.Where(t => t.CollectorId == dashboard.InternalCollector.Id
                                             && t.SensorId == OutdoorTemperaturSensor.Id).LastOrDefault()?.Value;
                }

                var OutdoorHumiditySensor = dashboard.InternalCollector.Sensors.Where(l => l.Type == SensorType.Humidity).LastOrDefault();
                if (OutdoorHumiditySensor != null)
                {
                    mainDashboard.OutdoorHumidity = _context.SensorRecords.Where(t => t.CollectorId == dashboard.InternalCollector.Id
                                             && t.SensorId == OutdoorHumiditySensor.Id).LastOrDefault()?.Value;
                }

            }

      
            return mainDashboard;
        }
    }
}
