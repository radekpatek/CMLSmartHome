using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMLSmartHomeCommon.Classes;  
using CMLSmartHomeCommon.Model;
using CMLSmartHomeWeb.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CMLSmartHomeWeb.Controllers
{
    public class DashboardController : Controller
    {
        private IConfiguration _configuration;

        public DashboardController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        ControllerAPI _controllerAPI = new ControllerAPI();

        public async Task<IActionResult> Index()
        {
            var dashboard = new MainDashboard();
            var client = _controllerAPI.Initialize(_configuration);
            var response = await client.GetAsync("api/MainDashboard");
            if (response.IsSuccessStatusCode)
            {
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                dashboard = JsonConvert.DeserializeObject<MainDashboard>(jsonResult);
            }

            return View(dashboard);
        }


    }
}