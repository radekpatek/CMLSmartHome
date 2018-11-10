using CMLSmartHomeCommon.Model;
using CMLSmartHomeController.Model;
using CMLSmartHomeWeb.Helper;
using CMLSmartHomeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMLSmartHomeWeb.Controllers
{
    public class SettingsController : Controller
    {
        private IConfiguration _configuration;

        public SettingsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        ControllerAPI _controllerAPI = new ControllerAPI();

        public async Task<IActionResult> Index()
        {
            SettingsModel settings = new SettingsModel();

            var client = _controllerAPI.Initialize(_configuration);

            //Controllers
            var controllersResponse = await client.GetAsync("api/Controllers");
            if (controllersResponse.IsSuccessStatusCode)
            {
                string jsonResult = controllersResponse.Content.ReadAsStringAsync().Result;
                settings.Controllers = JsonConvert.DeserializeObject<List<SmartHomeController>>(jsonResult);
            }

            //Collectors
            var collectorsResponse = await client.GetAsync("api/Collectors");
            if (collectorsResponse.IsSuccessStatusCode)
            {
                string jsonResult = collectorsResponse.Content.ReadAsStringAsync().Result;
                settings.Collectors = JsonConvert.DeserializeObject<List<Collector>>(jsonResult);
            }

            //Sensorss
            var sensorsResponse = await client.GetAsync("api/Sensors");
            if (sensorsResponse.IsSuccessStatusCode)
            {
                string jsonResult = sensorsResponse.Content.ReadAsStringAsync().Result;
                settings.Sensors = JsonConvert.DeserializeObject<List<Sensor>>(jsonResult);
            }

            return View(settings);
        }

    }
}