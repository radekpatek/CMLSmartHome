using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMLSmartHomeController.Model;
using CMLSmartHomeWeb.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CMLSmartHomeWeb.Controllers
{
    public class SensorRecordController : Controller
    {
        private IConfiguration _configuration;

        public SensorRecordController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        ControllerAPI _collectorAPI = new ControllerAPI();

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "Record ID" ? "Record ID Desc" : "Record ID";
            ViewBag.CollectorIDSortParm = sortOrder == "Collector ID" ? "Collector ID Desc" : "Collector ID";
            ViewBag.SensorIDSortParm = sortOrder == "Sensor ID" ? "Sensor ID Desc" : "Sensor ID";
            ViewBag.ValueSortParm = sortOrder == "Value" ? "Value Desc" : "Value";
            ViewBag.UnitSortParm = sortOrder == "Unit" ? "Unit Desc" : "Unit";
            ViewBag.DateSortParm = sortOrder == "DateTime" ? "DateTime Desc" : "DateTime";

            var sensorRecords = new List<SensorRecord>();
            var client = _collectorAPI.Initialize(_configuration);
            var response = await client.GetAsync("api/SensorRecords");
            if (response.IsSuccessStatusCode)
            {
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                sensorRecords = JsonConvert.DeserializeObject<List<SensorRecord>>(jsonResult);
                sortOrder = "DateTime Desc";
            }

            switch (sortOrder)
            {
                case "Record ID":
                    sensorRecords = sensorRecords.OrderBy(s => s.Id).ToList();
                    break;
                case "Record ID Desc":
                    sensorRecords = sensorRecords.OrderByDescending(s => s.Id).ToList();
                    break;
                case "Sensor ID":
                    sensorRecords = sensorRecords.OrderBy(s => s.SensorId).ToList();
                    break;
                case "Sensor ID Desc":
                    sensorRecords = sensorRecords.OrderByDescending(s => s.SensorId).ToList();
                    break;
                case "Value":
                    sensorRecords = sensorRecords.OrderBy(s => s.Value).ToList();
                    break;
                case "Value Desc":
                    sensorRecords = sensorRecords.OrderByDescending(s => s.Value).ToList();
                    break;
                case "Unit ID":
                    sensorRecords = sensorRecords.OrderBy(s => s.Unit).ToList();
                    break;
                case "Unit Desc":
                    sensorRecords = sensorRecords.OrderByDescending(s => s.Unit).ToList();
                    break;
                case "DateTime":
                    sensorRecords = sensorRecords.OrderBy(s => s.DateTime).ToList();
                    break;
                default:
                    sensorRecords = sensorRecords.OrderByDescending(s => s.DateTime).ToList();
                    break;
            }

            return View(sensorRecords);
        }

    }
}