#include "ArduinoJson.h"
#include "Dashboard.h"

CollectorValues::CollectorValues(void)
{
    Sensors = new SensorValue[ SIZE_Sensor ]; 
};


HourlyForecast::HourlyForecast(void)
{
    Hours = new String[ SIZE_HourlyForecast ]; 
    Values = new double[ SIZE_HourlyForecast ]; 
};


Dashboard::Dashboard(void)
{
    OutdoorCollectors = new CollectorValues[ SIZE_OutdoorCollectors ]; 
    IndoorCollectors = new CollectorValues[ SIZE_IndoorCollectors ]; 
};

void Dashboard::SetBase(DynamicJsonDocument Base)
{
    GenerationDateTime = Base["generationDateTime"].as<char*>();
}

void Dashboard::SetOutdoorCollectors(DynamicJsonDocument outdoorCollectors)
{
    int i = 0;
    for (JsonObject outdoorCollector : outdoorCollectors["collectors"].as<JsonArray>()){
        //Location
        OutdoorCollectors[i].Location = outdoorCollector["location"].as<char*>();
        
        int j = 0;    
        //Sensors
        for (JsonObject outdoorSensor : outdoorCollector["sensors"].as<JsonArray>()){
            OutdoorCollectors[i].Sensors[j].Sensor.Type = outdoorSensor["sensor"]["type"].as<int>();
            OutdoorCollectors[i].Sensors[j].Sensor.Id = outdoorSensor["sensor"]["id"].as<int>();
            OutdoorCollectors[i].Sensors[j].Sensor.Name = outdoorSensor["sensor"]["name"].as<char*>();
            OutdoorCollectors[i].Sensors[j].Sensor.Unit = outdoorSensor["sensor"]["unit"].as<int>();;
            OutdoorCollectors[i].Sensors[j].Value = outdoorSensor["value"].as<double>();
            j++;
        }
        i++;
    }
}


void Dashboard::SetIndoorCollectors(DynamicJsonDocument indoorCollectors)
{
    int i = 0;
    for (JsonObject indoorCollector : indoorCollectors["collectors"].as<JsonArray>()){
        //Location
        IndoorCollectors[i].Location = indoorCollector["location"].as<char*>();
       
        int j = 0;    
        //Sensors
        for (JsonObject indoorSensor : indoorCollector["sensors"].as<JsonArray>()){
            IndoorCollectors[i].Sensors[j].Sensor.Type = indoorSensor["sensor"]["type"].as<int>();
            IndoorCollectors[i].Sensors[j].Sensor.Id = indoorSensor["sensor"]["id"].as<int>();
            IndoorCollectors[i].Sensors[j].Sensor.Name = indoorSensor["sensor"]["name"].as<char*>();
            IndoorCollectors[i].Sensors[j].Sensor.Unit = indoorSensor["sensor"]["unit"].as<int>();;
            IndoorCollectors[i].Sensors[j].Value = indoorSensor["value"].as<double>();
            j++;
        }
        i++;
    }
}

void Dashboard::SetDewPoints(DynamicJsonDocument DewPoints)
{
    OutdoorDewpointTemperature = DewPoints["outdoorDewpointTemperature"].as<double>();
    IndoorDewpointTemperature = DewPoints["indoorDewpointTemperature"].as<double>();
}

void Dashboard::SetSunState(DynamicJsonDocument SunState)
{
    Sunrise = SunState["sunrise"].as<char*>();
    Sunset = SunState["sunset"].as<char*>();
}


