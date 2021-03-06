﻿using CMLSmartHomeCollector.Interfaces;
using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using CMLSmartHomeController.Model;
using CMLSmartHomeCommon.Model;

namespace CMLSmartHomeCollector.Classes
{

    public class RestClient : IRestClient
    {
        private ILog _logger;
        private IConfiguration _configuration;
        private string _controllerServer;
        private string _restAPI;
        private readonly string _contentType;

        private HttpClient _httpClient;

        public string ContentType { get; set; }

        const string user = "CMLSmartHomeUser";
        const string password = "CMLSmartHome$45";

        public RestClient(ILog logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _controllerServer = _configuration["ControllerServer"];
            _restAPI = _configuration["RestAPI"];
            _contentType = "application/json";

            _httpClient = new HttpClient();

        }

        /// <summary>
        /// Get Collector by MAC Address
        /// </summary>
        public Collector GetCollectorByMACAddress(CollectorBase collector)
        {
            var controller = "Collectors/Search";

            UriBuilder builder = new UriBuilder(string.Format("http://{0}/{1}/{2}", _controllerServer, _restAPI, controller))
            {
                Query = string.Format("macAddress={0}", collector.macAddress)
            };

            _logger.Info(string.Format("Call Rest API - {0}", builder.Uri));

            var response = _httpClient.GetAsync(builder.Uri);

            string jsonResult = response.Result.Content.ReadAsStringAsync().Result;


            return JsonConvert.DeserializeObject<Collector>(jsonResult);
        }


        /// <summary>
        /// Set Collector
        /// </summary>
        public Collector SetCollector(CollectorBase collector)
        {
            var controller = "Collectors";

            string url = string.Format("http://{0}/{1}/{2}", _controllerServer, _restAPI, controller);
            var uri = new Uri(url);

            _logger.Info(string.Format("Call Rest API - {0}", url));

            var PostData = JsonConvert.SerializeObject(collector);
            
            var response = _httpClient.PostAsync(uri, new StringContent(PostData, Encoding.UTF8, _contentType));

            string jsonResult = response.Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Collector>(jsonResult);
        }

        /// <summary>
        /// Call writing measurement
        /// </summary>
        public long SetMeasure(SensorRecord sensorRecord)
        {
            var controller = "SensorRecords";

            string url = string.Format("http://{0}/{1}/{2}", _controllerServer, _restAPI, controller);
            var uri = new Uri(url);

            _logger.Info(string.Format("Call Rest API - {0}/Post", url));

            var PostData = JsonConvert.SerializeObject(sensorRecord);

            var response = _httpClient.PostAsync(uri, new StringContent(PostData, Encoding.UTF8, _contentType));

            string jsonResult = response.Result.Content.ReadAsStringAsync().Result;
            var sensorRecordResult = JsonConvert.DeserializeObject<SensorRecord>(jsonResult);

            return sensorRecordResult.Id;
        }
    }
}
