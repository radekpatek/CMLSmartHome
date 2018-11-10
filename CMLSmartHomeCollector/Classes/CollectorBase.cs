using CMLSmartHomeCollector.Factories;
using CMLSmartHomeCollector.Interfaces;
using CMLSmartHomeCollector.Sensors;
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
        public int readingFrequency { get; set; }
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
            readingFrequency = int.Parse(_configuration["ReadingFrequency"]);

            // Read sensors definition from config file
            var configSensors = _configuration.GetSection("Sensors");
            var sensorFactory = new SensorFactory(_logger);
            foreach (IConfigurationSection section in configSensors.GetChildren())
            {
                sensors.Add(sensorFactory.Get(section));
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
            _logger.Info("Run Collector - start");

            //Collector Initialize
            Initialize();

            // Registration to controller and synchronize ID
            RegisterCollectorInController();

            while (true)
            {
                // Read values from sensors
                foreach (var sensor in sensors)
                {
                    try
                    {
                        _logger.Info(string.Format("Sensor {0} before measure", sensor.ToString()));
                        var measureValue = sensor.Measure(sensor.Type);
                        _logger.Info(string.Format("Sensor {0} , Value {1}", sensor.ToString(), measureValue));

                        var sensorRecord = new SensorRecord();

                        sensorRecord.CollectorId = id;
                        sensorRecord.SensorId = sensor.Id;
                        sensorRecord.Unit = sensor.Unit;
                        sensorRecord.Value = measureValue;
                        sensorRecord.DateTime = System.DateTime.Now;

                        _communicator.SetMeasure(sensorRecord);
                    }
                    catch (System.Exception e)
                    {
                        _logger.Error(string.Format("Measure Error - Sensor ID {0}, Error: {1}", sensor.Id, e.Message), e);
                    }
                    Thread.Sleep(readingFrequency * 1000);
                }
            };
        }

    }
}
