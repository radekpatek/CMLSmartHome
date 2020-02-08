using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMLSmartHomeCommon.Model;
using CMLSmartHomeController.Model;
using CMLSmartHomeWeb.Helper;
using CMLSmartHomeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        List<Collector> _collectors = new List<Collector>();


        public async Task<IActionResult> Index(SensorRecordModel Model, int CollectorID, int SensorID, DateTime RecordDatetimeFrom, DateTime? RecordDatetimeTo, string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            SensorRecordModel sensorRecordsModel;

            //Initialize
            sensorRecordsModel = Model;

            sensorRecordsModel.SensorRecords = new List<SensorRecord>();
            var actDateTime = DateTime.Now;
            if (RecordDatetimeFrom == DateTime.MinValue)
                sensorRecordsModel.RecordDatetimeFrom = new DateTime(actDateTime.Year, actDateTime.Month, actDateTime.Day, actDateTime.Hour, actDateTime.Minute, 0).AddDays(-1);
            sensorRecordsModel.SensorID = SensorID;

            if (ViewBag.CollectorList == null) ViewBag.CollectorList = new SelectList(GetCollectorList(), "Id", "Name");
            if (ViewBag.SensorList == null ) ViewBag.SensorList = new SelectList(GetSensorList(sensorRecordsModel.CollectorID), "Id", "Name");

            if (!ModelState.IsValid) return View(Model);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "Record ID" ? "Record ID Desc" : "Record ID";
            ViewBag.CollectorIDSortParm = sortOrder == "Collector ID" ? "Collector ID Desc" : "Collector ID";
            ViewBag.SensorIDSortParm = sortOrder == "Sensor ID" ? "Sensor ID Desc" : "Sensor ID";
            ViewBag.ValueSortParm = sortOrder == "Value" ? "Value Desc" : "Value";
            ViewBag.UnitSortParm = sortOrder == "Unit" ? "Unit Desc" : "Unit";
            ViewBag.DateSortParm = sortOrder == "DateTime" ? "DateTime Desc" : "DateTime";

            currentFilter += CollectorID != 0 ? String.Format("collectorId={0}&", CollectorID) : "" ;
            currentFilter += SensorID != 0 ? String.Format("sensorId={0}&", SensorID) : "";
            currentFilter += RecordDatetimeFrom != DateTime.MinValue ? String.Format("recordDatetimeFrom={0}&", RecordDatetimeFrom.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK")) : "";
            currentFilter += (RecordDatetimeTo != DateTime.MinValue && RecordDatetimeTo != null) ? String.Format("recordDatetimeTo={0}&", RecordDatetimeTo?.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK")) : "";
            currentFilter= currentFilter.TrimEnd('&');

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                var client = _collectorAPI.Initialize(_configuration);
                var response = await client.GetAsync("api/SensorRecords/search?" + searchString);
                if (response.IsSuccessStatusCode)
                {
                    string jsonSensorRecordsResult = response.Content.ReadAsStringAsync().Result;

                    sensorRecordsModel.SensorRecords = JsonConvert.DeserializeObject<List<SensorRecord>>(jsonSensorRecordsResult);
                    sortOrder = "DateTime ASC";
                }

                switch (sortOrder)
                {
                    case "Record ID":
                        sensorRecordsModel.SensorRecords = sensorRecordsModel.SensorRecords.OrderBy(s => s.Id).ToList();
                        break;
                    case "Record ID Desc":
                        sensorRecordsModel.SensorRecords = sensorRecordsModel.SensorRecords.OrderByDescending(s => s.Id).ToList();
                        break;
                    case "Sensor ID":
                        sensorRecordsModel.SensorRecords = sensorRecordsModel.SensorRecords.OrderBy(s => s.SensorId).ToList();
                        break;
                    case "Sensor ID Desc":
                        sensorRecordsModel.SensorRecords = sensorRecordsModel.SensorRecords.OrderByDescending(s => s.SensorId).ToList();
                        break;
                    case "Value":
                        sensorRecordsModel.SensorRecords = sensorRecordsModel.SensorRecords.OrderBy(s => s.Value).ToList();
                        break;
                    case "Value Desc":
                        sensorRecordsModel.SensorRecords = sensorRecordsModel.SensorRecords.OrderByDescending(s => s.Value).ToList();
                        break;
                    case "Unit ID":
                        sensorRecordsModel.SensorRecords = sensorRecordsModel.SensorRecords.OrderBy(s => s.Unit).ToList();
                        break;
                    case "Unit Desc":
                        sensorRecordsModel.SensorRecords = sensorRecordsModel.SensorRecords.OrderByDescending(s => s.Unit).ToList();
                        break;
                    case "DateTime":
                        sensorRecordsModel.SensorRecords = sensorRecordsModel.SensorRecords.OrderBy(s => s.DateTime).ToList();
                        break;
                    default:
                        sensorRecordsModel.SensorRecords = sensorRecordsModel.SensorRecords.OrderByDescending(s => s.DateTime).ToList();
                        break;
                }
            }
            return View(sensorRecordsModel);
        }


        /// <summary>
        /// Získání seznamu kolektorů
        /// </summary>
        /// <returns></returns>
        public List<Collector> GetCollectorList()
        {
            List<Collector> collectors = new List<Collector>();

            var client = _collectorAPI.Initialize(_configuration);
            var task = Task.Run(() => client.GetAsync("api/Collectors"));
            task.Wait();

            if (task.Status != TaskStatus.Faulted)
            {
                var collectorsResponse = task.Result.Content.ReadAsStringAsync().Result;
                collectors = JsonConvert.DeserializeObject<List<Collector>>(collectorsResponse);
            }

            return collectors;
        }

        /// <summary>
        /// Získání seznamu senzorů daného kolektoru
        /// </summary>
        /// <returns></returns>
        public List<Sensor> GetSensorList(int CollectorId)
        {
            List<Sensor> sensors = new List<Sensor>();

            if (CollectorId != 0)
            {
                var client = _collectorAPI.Initialize(_configuration);
                var task = Task.Run(() => client.GetAsync(String.Format("api/Collectors/{0}", CollectorId)));
                task.Wait();

                if (task.Status != TaskStatus.Faulted)
                {
                    var collectorResponse = task.Result.Content.ReadAsStringAsync().Result;
                    sensors = JsonConvert.DeserializeObject<Collector>(collectorResponse).Sensors.ToList();
                }
                ViewBag.SensorList = new SelectList(sensors, "Id", "Name");
            }
            return sensors;
        }

        /// <summary>
        /// Získání seznamu senzorů daného kolektoru
        /// </summary>
        /// <param name="CollectorId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSensorListAsync(int CollectorId)
        {
            ViewBag.SensorList = new SelectList(GetSensorList(CollectorId), "Id", "Name");
            
            return PartialView("DisplaySensors");
        }


    }
}