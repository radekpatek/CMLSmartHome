using System.Collections.Generic;
using System.Threading.Tasks;
using CMLSmartHomeCommon.Models;
using CMLSmartHomeWeb.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CMLSmartHomeWeb.Controllers
{
    public class CollectorController : Controller
    {
        CollectorAPI _collectorAPI = new CollectorAPI();

        public async Task<IActionResult> Index()
        {
            var students = new List<Collector>();
            var client = _collectorAPI.Initialize();
            var response = await client.GetAsync("api/Collectors");
            if (response.IsSuccessStatusCode)
            {
                string jsonResult = response.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<List<Collector>>(jsonResult);
            }

            return View(students); 
        }
    }
}