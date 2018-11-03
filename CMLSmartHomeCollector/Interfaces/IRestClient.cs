using CMLSmartHomeCommon.Model;
using CMLSmartHomeController.Model;

namespace CMLSmartHomeCollector.Interfaces
{
    public interface IRestClient
    {
        Collector GetCollectorByMACAddress(CollectorBase collector);
        Collector SetCollector(CollectorBase collector);

        long SetMeasure(SensorRecord sensorRecord);
    }
}
