using CMLSmartHomeCollector.Factories;
using CMLSmartHomeCollector.Interfaces;
using CMLSmartHomeController.Model;
using log4net;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace CMLSmartHomeCollector
{
    public class CollectorBase
    {
        private ILog _logger;
        private IConfiguration _configuration;
        private IRestClient _communicator;

        public long id { get; set; }
        public string name { get; set; }
        public string macAddress { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public List<SensorCollector>  sensors { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="communicator">Object for communication with Controller</param>
        public CollectorBase(ILog logger, IConfiguration configuration, IRestClient communicator)
        {
            _logger = logger;
            _configuration = configuration;
            _communicator = communicator;

            sensors = new List<SensorCollector>();
        }

        /// <summary>
        /// Collector initialize
        /// </summary>
        public void Initialize()
        {
            macAddress = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();

            name = Dns.GetHostName();
            location = _configuration["Location"];
            description = _configuration["Description"];

            // Read sensors definition from config file
            var configSensors = _configuration.GetSection("Sensors");
            foreach (IConfigurationSection section in configSensors.GetChildren())
            {
                sensors.Add(SensorFactory.Get(section));
            }
        }

        /// <summary>
        /// // Registration to controller and synchronize ID
        /// </summary>
        public void RegisterCollectorInController()
        {
            var registredCollector = _communicator.GetCollectorByMACAddress(this);

            if (registredCollector == null)
            {
                //Save collector
                registredCollector = _communicator.SetCollector(this);

                if (registredCollector == null)
                {
                    throw new System.NullReferenceException("RegisterCollectorInController: registredCollector null object after SetCollector");
                }
           
            }

            id = registredCollector.Id;

            foreach (var item in registredCollector.Sensors)
            {
                var sensor = sensors.Find(t => t.Type == item.Type);
                if (sensor != null)
                {
                    sensor.Id = item.Id;
                }
            }
        }

        /// <summary>
        /// Start process of Collector
        /// </summary>
        public void Run()
        {
            _logger.Info("Run Collector");

            //Collector Initialize
            Initialize();

            // Registration to controller and synchronize ID
            RegisterCollectorInController();

            // Read values from sensors
            Parallel.ForEach(sensors, (sensor) =>
            {
                while (true)
                {
                    var measureValue = sensor.Measure();

                    var sensorRecord = new SensorRecord();

                    sensorRecord.CollectorId = id;
                    sensorRecord.SensorId = sensor.Id;
                    sensorRecord.Unit = sensor.Unit;
                    sensorRecord.Value = measureValue;
                    sensorRecord.DateTime = System.DateTime.Now;

                    _communicator.SetMeasure(sensorRecord);

                    Thread.Sleep(sensor.ReadingFrequency * 100); // :TODO: musí být 1000
                }

            });

        }

    }
}
