#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <ArduinoJson.h>

#define trigger_pin D6
#define Echo_pin D5
#define LED 2

//WiFi
const char* ssid     = "Gondor";
const char* password = "Borovnicka311";
const char* hostname = "DoorOpenIdentifier";
const String wsHost = "cmlsmarthomecontroller";

//CMLSmartCollector
int collectorId;
int openDoorSensorId;
float DoorOpenState;

long duration;
int distance;
String mac;

// VytvoŘENÍ collectoru na controlleru
void createCollector()
{
    HTTPClient http; //Object of class HTTPClient
    http.setTimeout(5000);

    String url = "http://" + wsHost + "/api/Collectors";
    Serial.println("createCollector: " + url);
    http.begin(url);
    http.addHeader("Host", wsHost);
    http.addHeader("Content-Type", "application/json"); 
    
    const int capacity = JSON_OBJECT_SIZE(1);
    StaticJsonDocument<capacity> doc;
    
    doc["macaddress"] = mac.c_str();

    String data;
    serializeJson(doc, data);

    Serial.println(data);
    
    int httpCode = http.POST(data);

    if(httpCode > 0) 
    {      
        Serial.printf("[HTTP] POST... code: %d\n", httpCode);
        if(httpCode == HTTP_CODE_OK) 
        {
          String payload = http.getString();
          Serial.println(payload);
        }
    } else {
        Serial.printf("[HTTP] POST... failed, error: %s\n", http.errorToString(httpCode).c_str());
    }
    http.end(); //Close connection
}


void getCollector()
{ 
    HTTPClient http; //Object of class HTTPClient
    http.setTimeout(5000);
    
    String url = "http://" + wsHost + "/api/Collectors/search?macAddress=" + mac;
    Serial.println(url);
    http.begin(url);
    http.addHeader("Host", wsHost);
    http.addHeader("Content-Type", "application/json"); 
    
    int httpCode = http.GET();

    if(httpCode > 0) 
    {
        Serial.printf("[HTTP] GET... code: %d\n", httpCode);

        if(httpCode ==  HTTP_CODE_NOT_FOUND) 
        {
            createCollector();
        }
        
        if(httpCode == HTTP_CODE_OK) 
        {
            String data = http.getString();
            Serial.println(data);
  
            DynamicJsonDocument doc(1024);
            deserializeJson(doc, http.getString());
  
            collectorId = doc["id"];
            Serial.print("collectorId: "); 
            Serial.println(collectorId);
            JsonArray sensors = doc["sensors"];
  
            for (JsonObject sensor : sensors)
            {
              int sensorType = sensor["type"].as<int>();
              int sensorId = sensor["id"].as<int>();
  
              if (sensorType == 4)
              {
                    openDoorSensorId = sensorId;               
              }
            }    
            
            Serial.print("openDoorSensorId: ");
            Serial.println(openDoorSensorId);  
        }
    } else {
        Serial.printf("[HTTP] GET... failed, error: %s\n", http.errorToString(httpCode).c_str());
    }
    http.end(); //Close connection
}

void sendRecord(const int sensorId, const float value, const int unit)
{ 
    HTTPClient http; //Object of class HTTPClient
    http.setTimeout(5000);

    String url = "http://" + wsHost + "/api/SensorRecords";
    Serial.println(url);
    http.begin(url);
    http.addHeader("Host", wsHost);
    http.addHeader("Content-Type", "application/json"); 
    
    const int capacity = JSON_OBJECT_SIZE(4);
    StaticJsonDocument<capacity> doc;
    
    doc["sensorId"] = sensorId;
    doc["collectorId"] = collectorId;
    doc["value"] = String(value, 1);
    doc["unit"] = unit;

    String data;
    serializeJson(doc, data);

    int httpCode = http.POST(data);

    if(httpCode > 0) 
    {      
        Serial.printf("[HTTP] POST... code: %d\n", httpCode);
        if(httpCode == HTTP_CODE_OK) 
        {
          String payload = http.getString();
          Serial.println(payload);
        }
    } else {
        Serial.printf("[HTTP] POST... failed, error: %s\n", http.errorToString(httpCode).c_str());
    }
    http.end(); //Close connection
}

void setup() {
  pinMode(trigger_pin, OUTPUT); // configure the trigger_pin(D6) as an Output
  pinMode(Echo_pin, INPUT); // configure the Echo_pin(D5) as an Input
  Serial.begin(115200); // Enable the serial with 9600 baud rate

  WiFi.hostname(hostname);
  WiFi.mode(WIFI_OFF);        //Prevents reconnection issue (taking too long to connect)
  delay(1000);
  WiFi.mode(WIFI_STA);        //This line hides the viewing of ESP as wifi hotspot
  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) 
  {
    delay(1000);
    Serial.println("Connecting...");
  }

  mac = WiFi.macAddress();
  Serial.println(mac);
  mac.replace(":","");

  getCollector();

}

void loop() {
    
  digitalWrite(trigger_pin, LOW); //set trigger signal low for 2us
  delayMicroseconds(2);

  /*send 10 microsecond pulse to trigger pin of HC-SR04 */
  digitalWrite(trigger_pin, HIGH);  // make trigger pin active high
  delayMicroseconds(10);            // wait for 10 microseconds
  digitalWrite(trigger_pin, LOW);   // make trigger pin active low

  /*Measure the Echo output signal duration or pulss width */
  duration = pulseIn(Echo_pin, HIGH); // save time duration value in "duration variable
  distance= duration*0.034/2; //Convert pulse duration into distance

  /* if distance greater than 10cm, turn on LED */
  if ( distance < 200)
  digitalWrite(LED, HIGH);
  else 
  digitalWrite(LED, LOW);

  // print measured distance value on Arduino serial monitor
  Serial.print("Distance: ");
  Serial.print(distance);
  Serial.println(" cm");

  // door is closed when distance is less than 3500 cm (-1-Open, 1-Close)
  DoorOpenState = (distance > 3500) ? -1 : 1;

  sendRecord(openDoorSensorId, DoorOpenState, 4);

  delay(20000);
}
