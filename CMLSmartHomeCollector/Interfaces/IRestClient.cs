using CMLSmartHome.Models;
using CMLSmartHomeCommon.Models;

namespace CMLSmartHomeCollector.Interfaces
{
    public interface IRestClient
    {
        Collector GetCollectorByMACAddress(CollectorBase collector);
        Collector SetCollector(CollectorBase collector);

        long SetMeasure(SensorRecord sensorRecord);
    }
}
