﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMLSmartHomeCommon.Model;
using CMLSmartHomeWeb.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CMLSmartHomeWeb.Controllers
{
    public class CollectorController : Controller
    {
        private IConfiguration _configuration;

        public CollectorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        ControllerAPI _collectorAPI = new ControllerAPI();
        List<Collector> _collectors = new List<Collector>();

        public async Task<IActionResult> Index()
        {
            List<Collector> collectors = new List<Collector>();
            var client = _collectorAPI.Initialize(_configuration);
            var response = await client.GetAsync("api/Collectors");
            if (response.IsSuccessStatusCode)
            {
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                collectors = JsonConvert.DeserializeObject<List<Collector>>(jsonResult);
            }
            return View(collectors); 
        }
    }
}