using CMLSmartHomeCollector.Config;
using log4net;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CMLSmartHomeCollector
{
    public class Collector
    {
        private ILog _logger;
        private IConfiguration _configuration;

        public List<Sensor> sensors;

        public Collector(ILog logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            sensors = new List<Sensor>();
        }

        public void Run()
        {
            _logger.Info("Run Collector");
            // Registration to controller
            // Načíst si z configu seznam senzorů a poslat na controller, který vrátí ID, pod kterými se bude komunikovat

            // Read sensors definition from config file
            var configSensors = _configuration.GetSection("Sensors");
            foreach (IConfigurationSection section in configSensors.GetChildren())
            {
                this.sensors.Add(new Sensor(section));
            }


        //Listen sensors
        string url = string.Format("https://{0}:{1}/{2}"
                    , _configuration["ControllerServer"]
                    , _configuration["Port"]
                    , _configuration["RestAPI"]);

            _logger.Info(string.Format("Call Rest API - {0}", url));

            // string details = CallRestMethod(url);
        }

        private static string CallRestMethod(string url)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.Headers.Add("Username", "xyz");
            webrequest.Headers.Add("Password", "abc");
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            webresponse.Close();
            return result;
        }

    }
}
