#define ENABLE_GxEPD2_GFX 0

#include <GxEPD2_BW.h>
#include "Bitmap.h"
#include "base64.hpp"
#include <base64.h>

#include "ArduinoJson.h"
#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <Adafruit_I2CDevice.h>

#include "Fonts\Cousine8pt7b.h"
#include "Fonts\FreeSans9pt7b.h"
#include "Fonts\FreeSans12pt7b.h"
#include "Fonts\FreeSans18pt7b.h"


GxEPD2_BW < GxEPD2_750, GxEPD2_750::HEIGHT / 2 > display(GxEPD2_750(D8,  D3, D4, D2));

enum align { left, right };
unsigned char message[13370];

//Controller
String host = "cmlsmarthomecontroller";

//WiFi
const char* ssid     = "Gondor";
const char* password = "Borovnicka311";

int status = WL_IDLE_STATUS;  

DynamicJsonDocument dashboard(2048);


void getDashboard()
{
  
    Serial.println("getDashboard - Start");
    HTTPClient http; 
    
    http.begin("http://" + host + "/api/MainDashboard");
    
    int x = 0;
    int httpCode;

    do {
      x++;
      delay(1000);     
      Serial.println("getDashboard - HTTP Get"); 
      
      http.addHeader("Host", host);
      http.addHeader("Content-Type", "application/json");     
      http.setTimeout(20000);
      httpCode = http.GET();
      Serial.print("getDashboard - Get httpCode: "); 
      Serial.println(httpCode);
    } while ((httpCode !=  HTTP_CODE_OK ) and x < 10 );
    
    if(httpCode ==  HTTP_CODE_OK) 
    {
        int len = http.getSize();
        Serial.print("dashboard Size - ");
        Serial.println(len);
         
        DeserializationError err = deserializeJson(dashboard, http.getStream());       
        if (err) {
          Serial.print("dashboard deserializeJson() returned ");
          Serial.println(err.c_str());
        }
    }

    http.end(); 
}

void getDashboardGraphs()
{
    const size_t capacityDashboardGraphs = JSON_OBJECT_SIZE(1) + 14850;
    DynamicJsonDocument dashboardGraphs(capacityDashboardGraphs);
  
    Serial.println("getDashboardGraphs - Start");
    HTTPClient http; //Object of class HTTPClient

    http.useHTTP10(true);
    String url = "http://" + host + "/api/MainDashboard/Graphs";
    http.begin(url);
    http.addHeader("Host", host);
    http.addHeader("Content-Type", "application/json"); 
    
    int httpCode = http.GET();
    
    Serial.printf("dashboardGraphs - Get httpCode: %d\n", httpCode); 
    
    if(httpCode ==  HTTP_CODE_OK) 
    {
       int len = http.getSize();
       Serial.printf("dashboardGraphs Size: %d\n", len);
        Serial.print("kapacita: ");
        Serial.println(dashboardGraphs.capacity());
 
        DeserializationError err = deserializeJson(dashboardGraphs, http.getStream());
        if (err) {
          Serial.print("dashboardGraphs deserializeJson() returned ");
          Serial.println(err.c_str());
        }

        //Grafy
        serializeMsgPack(dashboardGraphs["outdoorTemperatureGraphByte"], message);
        printf("sizeof(message): %d\n", sizeof(message));

        //Od­říznout první­ 3 byty
        int messageLength = sizeof(message)-3;
        for (int index = 0; index < messageLength; index++)
        {
          message[index] = message[index+3];
        }
        
        dashboardGraphs.clear();
    }
    http.end(); 
    Serial.println("getDashboardGraphs - End");
}

void displayValue(int x, int y, align al, GFXfont font, String value)
{
    display.setFont(&font);
    display.setTextColor(GxEPD_BLACK);

    if (al == right)
    {
      int16_t tbx, tby; uint16_t tbw, tbh;
      display.getTextBounds(value, 0, 0, &tbx, &tby, &tbw, &tbh);
      x = x - tbw;
    }
    
    display.setCursor(x, y);
    display.print(value);
}

void drawDashboard()
{
  //unsigned char graph_Image_Bitmap[10000];

  byte *graph_Image_Bitmap = (byte*)malloc(10000);
  if (graph_Image_Bitmap == NULL)
  {
    Serial.println("malloc failed");
  }
  else
  {
  Serial.println("drawDashboard - Start");
  display.setRotation(2);
  display.setFullWindow();
  display.firstPage(); 

  unsigned int binary_length = decode_base64(message, graph_Image_Bitmap);
  Serial.printf("graph_Image_Bitmap_deco binary_length %d\n", binary_length);
        
  do
    {
      display.fillScreen(GxEPD_WHITE);
     
      //Draw underlayer picture
      Serial.println("DrawBackground");
      display.drawBitmap(0, 0, dashboardImage, display.epd2.WIDTH, display.epd2.HEIGHT, GxEPD_BLACK);
      display.drawBitmap(215, 145, graph_Image_Bitmap, 400, 200, GxEPD_BLACK);
       
      free(graph_Image_Bitmap);
      
      //Outdoor
      for (JsonObject outdoorSensor : dashboard["outdoorSensorsValue"].as<JsonArray>()){
        switch ( outdoorSensor["sensor"]["type"].as<int>() ) { 
            case 1 : 
              // Temperature
              displayValue(84,92, right, FreeSans18pt7b, outdoorSensor["value"]);
              break;
            case 2 : 
              // Humidity
              displayValue(214,92, right, FreeSans18pt7b, outdoorSensor["value"]);
              break;
          }
      }
      
      //Indoor
      int y = 65;
      for (JsonObject indoorCollector : dashboard["indoorCollectors"].as<JsonArray>()){
        //Location
        displayValue(510, y, left, FreeSans12pt7b, indoorCollector["location"].as<char*>());
  
        Serial.print("indoorSensor - Collector: ");
        Serial.println(indoorCollector["location"].as<String>());
          
        //Sensors
        for (JsonObject indoorSensor : indoorCollector["sensors"].as<JsonArray>()){
          switch ( indoorSensor["sensor"]["type"].as<int>() ) { 
              case 1 : 
                // Temperature
                displayValue(324, y, right, FreeSans12pt7b, indoorSensor["value"]);
                break;
              case 2 : 
                // Humidity
                displayValue(414, y, right, FreeSans12pt7b, indoorSensor["value"].as<String>());
                break;
              case 3 : 
                // CO2
                displayValue(498, y, right, FreeSans12pt7b, indoorSensor["value"].as<String>());
                break;
            }
        }
        y += 25;
      }

      //Východ slunce
      String sunrise = dashboard["sunrise"].as<String>();
      displayValue(160, 170, right, FreeSans12pt7b, sunrise); 

      //Západ slunce
      String sunset = dashboard["sunset"].as<String>();      
      displayValue(160, 210, right, FreeSans12pt7b, sunset); 

      //Teplota rosnéhoho bodu - vnitřní­ (Jí­delna)    
      int indoorDewpointTemperature = round(dashboard["indoorDewpointTemperature"].as<double>());   
      displayValue(160, 272, right, FreeSans12pt7b, String(indoorDewpointTemperature)); 

      //Teplota rosného bodu - venkovní
      int outdoorDewpointTemperature = round(dashboard["outdoorDewpointTemperature"].as<double>());      
      displayValue(95, 272, right, FreeSans12pt7b, String(outdoorDewpointTemperature)); 
      
      //Datum generování
      display.setFont(&Cousine_Regular_8);      
      display.setTextColor(GxEPD_BLACK);
      display.setCursor(540, 375);
      
      String generated = dashboard["generationDateTime"].as<String>();
      display.print(generated);
  
    }
    while (display.nextPage());
  }
  dashboard.clear();  
  Serial.println("drawDashboard - End");
  
}


void setup() {
  Serial.begin(115200);
  Serial.println("Start");
  
  //E-Paper
  display.init(115200);
  
  //WiFi
  WiFi.mode(WIFI_OFF);        //Prevents reconnection issue (taking too long to connect)
  delay(500);
  Serial.println();
  WiFi.hostname("Dashboard-ESP");
  Serial.print("Connecting to ");
  Serial.println(ssid);

  WiFi.mode(WIFI_STA); 
  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());

  getDashboard();
  getDashboardGraphs();
  drawDashboard();
 
  ESP.deepSleep(5*60e6, WAKE_RF_DEFAULT);
}

void loop() {
}
