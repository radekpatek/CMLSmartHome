using CMLSmartHomeCommon.Model;
using CMLSmartHomeController.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CMLSmartHomeWeb.Models
{
    public class SettingsModel
    {
        public ICollection<SmartHomeController> Controllers { get; set; } = new List<SmartHomeController>();
        public ICollection<Collector> Collectors { get; set; } = new List<Collector>();
        public ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();
    }
}
