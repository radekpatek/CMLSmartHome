#include "ArduinoJson.h"

#define SIZE_OutdoorCollectors 1
#define SIZE_IndoorCollectors 3
#define SIZE_HourlyForecast 48
#define SIZE_Sensor 3

class SensorType
{
    public:
        int Id;
        String Name;
        short Type;
        short Unit;
};

class SensorValue
{
    public:
        SensorType Sensor;
        double Value;
};

class CollectorValues
{
    public:
        CollectorValues();

        String Location;
        SensorValue *Sensors;
};

class HourlyForecast
{
    public:
        HourlyForecast(void);

        String *Hours;
        double *Values;
};

class Dashboard {       
  public:      
    Dashboard();
    void SetBase(DynamicJsonDocument Base);
    void SetOutdoorCollectors(DynamicJsonDocument outdoorCollectors);
    void SetIndoorCollectors(DynamicJsonDocument indoorCollectors);
    void SetDewPoints(DynamicJsonDocument DewPoints);
    void SetSunState(DynamicJsonDocument SunState);

    CollectorValues *OutdoorCollectors;
    CollectorValues *IndoorCollectors;
    String GenerationDateTime;
    String Sunrise;
    String Sunset;
    double OutdoorDewpointTemperature;
    double IndoorDewpointTemperature;
    HourlyForecast TemperatureForecast;
    HourlyForecast PrecipitationForecast;
};
