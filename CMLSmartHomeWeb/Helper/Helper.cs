using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace CMLSmartHomeWeb.Helper
{
    public class ControllerAPI
    {

        public HttpClient Initialize(IConfiguration configuration)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(configuration.GetSection("SmartHomeController").GetSection("URL").Value);
            return client;
        }
    }
}
