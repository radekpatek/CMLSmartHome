using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CMLSmartHomeWeb.Helper
{
    public class CollectorAPI
    {
        public HttpClient Initialize()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57534");
            return client;
        }
    }
}
