#define ENABLE_GxEPD2_GFX 0

#include <GxEPD2_BW.h>
#include "Bitmap.h"
#include "Dashboard.h"
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
HTTPClient http; 
WiFiClient client;  
const char* ssid     = "Gondor";
const char* password = "Borovnicka311";

int status = WL_IDLE_STATUS;  

Dashboard mainDashboard;

int getDashboardPart(String method, DynamicJsonDocument &dashboard)
{
  Serial.printf("getDashboardPart %s - Start, FreeHeap(%i) \n", method.c_str(), ESP.getFreeHeap()); 
  dashboard.clear();

  http.begin(client, "http://" + host + "/api/MainDashboard/" + method);

  int x = 0;
  int httpCode;

  do {
      x++;
      http.addHeader("Host", host);
      http.addHeader("Content-Type", "application/json");     
      http.setTimeout(40000);
      httpCode = http.GET();
      Serial.printf("getDashboardPart %s - Get httpCode: %i \n", method.c_str(), httpCode); 
      delay(100); 
    } while ((httpCode !=  HTTP_CODE_OK ) and x < 10 );

    if(httpCode ==  HTTP_CODE_OK) 
    {
        int len = http.getSize();
        Serial.printf("getDashboardPart %s - Size: %i \n", method.c_str(), len); 

        DeserializationError err = deserializeJson(dashboard, http.getStream());    

        if (err) {
          Serial.printf("getDashboardPart %s - DeserializeJson() returned: %s \n", method.c_str(), err.c_str());
        }
    }
  http.end(); 

  Serial.printf("getDashboardPart %s - End, FreeHeap(%i) \n", method.c_str(), ESP.getFreeHeap()); 

  return httpCode;
}

void getDashboardBase()
{
  DynamicJsonDocument dashboardBase(512);

  if (getDashboardPart("Base", dashboardBase)== HTTP_CODE_OK)
  {
    mainDashboard.SetBase(dashboardBase);  
  }
  dashboardBase.clear(); 
};

void getDashboardOutdoorCollectors()
{
  DynamicJsonDocument dashboard(1024);
  if (getDashboardPart("OutdoorCollectors", dashboard) == HTTP_CODE_OK)
  {
    mainDashboard.SetOutdoorCollectors(dashboard);  
  }
  dashboard.clear(); 
}

void getDashboardIndoorCollectors()
{
  DynamicJsonDocument dashboard(2048);
  if (getDashboardPart("IndoorCollectors", dashboard) == HTTP_CODE_OK)
  {
    mainDashboard.SetIndoorCollectors(dashboard);  
  }
  dashboard.clear(); 
};

void getDashboardSunState()
{
  DynamicJsonDocument dashboardSunState(512);

  if (getDashboardPart("SunState", dashboardSunState) == HTTP_CODE_OK)
  {
    mainDashboard.SetSunState(dashboardSunState);  
  }
  dashboardSunState.clear(); 
}

void getDashboardDewPoints()
{
  DynamicJsonDocument dashboardDewPoints(256);

  if (getDashboardPart("DewPoints", dashboardDewPoints)== HTTP_CODE_OK)
  {
    mainDashboard.SetDewPoints(dashboardDewPoints);  
  }
  dashboardDewPoints.clear(); 
};

void getDashboardGraphs()
{
    Serial.printf("getDashboardGraphs - Start, FreeHeap(%i) \n", ESP.getFreeHeap()); 

    const size_t capacityDashboardGraphs = JSON_OBJECT_SIZE(1) + 14850;
    DynamicJsonDocument dashboardGraphs(capacityDashboardGraphs);
  
    Serial.printf("getDashboardGraphs - after alocate DynamicJsonDocument, FreeHeap(%i) \n", ESP.getFreeHeap()); 

    http.useHTTP10(true);
    String url = "http://" + host + "/api/MainDashboard/Graphs";
    http.begin(client, url);
    http.addHeader("Host", host);
    http.addHeader("Content-Type", "application/json"); 
    
    int httpCode = http.GET();
    
    Serial.printf("getDashboardGraphs - Get httpCode: %d\n", httpCode); 
    
    if(httpCode ==  HTTP_CODE_OK) 
    {
       int len = http.getSize();
       Serial.printf("getDashboardGraphs - Hppt size: %d, dashboardGraphs.capacity: %d \n", len, dashboardGraphs.capacity());
 
        DeserializationError err = deserializeJson(dashboardGraphs, http.getStream());
        if (err) {
          Serial.printf("getDashboardGraphs - deserializeJson() returned Error %s \n", err.c_str());
        }

        //Grafy
        serializeMsgPack(dashboardGraphs["outdoorTemperatureGraphByte"], message);
        Serial.printf("getDashboardGraphs - sizeof(message): %d\n", sizeof(message));

        //Od­říznout první­ 3 byty
        int messageLength = sizeof(message)-3;
        for (int index = 0; index < messageLength; index++)
        {
          message[index] = message[index+3];
        }
    }
    http.end(); 
    
    dashboardGraphs.clear();
    dashboardGraphs.garbageCollect();

    Serial.printf("getDashboardGraphs - End, FreeHeap(%i) \n", ESP.getFreeHeap()); 
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
  Serial.printf("drawDashboard - START FreeHeap(%i) \n", ESP.getFreeHeap());

  byte *graph_Image_Bitmap = (byte*)malloc(10000);
  if (graph_Image_Bitmap == NULL)
  {
    Serial.printf("drawDashboard - malloc failed \n");
  }
  else
  {
    Serial.printf("drawDashboard - after malloc FreeHeap(%i) \n", ESP.getFreeHeap());
    display.setRotation(2);
    display.setFullWindow();
    display.firstPage(); 
        
    unsigned int binary_length = decode_base64(message, graph_Image_Bitmap);
    Serial.printf("drawDashboard - graph_Image_Bitmap length: %d FreeHeap(%i)\n", binary_length, ESP.getFreeHeap());

    do
      {
        display.fillScreen(GxEPD_WHITE);
      
        //Draw underlayer picture
        display.drawBitmap(0, 0, dashboardImage, display.epd2.WIDTH, display.epd2.HEIGHT, GxEPD_BLACK);
        display.drawBitmap(215, 145, graph_Image_Bitmap, 400, 200, GxEPD_BLACK);

        //Outdoor       
        for(int i = 0; i < 2; i++) {
          switch ( mainDashboard.OutdoorCollectors[0].Sensors[i].Sensor.Type ) { 
              case 1 : 
                // Temperature
                displayValue(84,92, right, FreeSans18pt7b, String(mainDashboard.OutdoorCollectors[0].Sensors[i].Value,1));
                break;
              case 2 : 
                // Humidity
                displayValue(214,92, right, FreeSans18pt7b, String(mainDashboard.OutdoorCollectors[0].Sensors[i].Value,0));
                break;            
          }
        }

        //Indoor
        int y = 65;
        for(int i = 0; i < 3; i++) {
          //Location
          displayValue(510, y, left, FreeSans12pt7b, mainDashboard.IndoorCollectors[i].Location);
            
          //Sensors
          for(int j = 0; j < 3; j++) {
            switch ( mainDashboard.IndoorCollectors[i].Sensors[j].Sensor.Type ) { 
                case 1 : 
                  // Temperature
                  displayValue(324, y, right, FreeSans12pt7b, String(mainDashboard.IndoorCollectors[i].Sensors[j].Value,1));
                  break;
                case 2 : 
                  // Humidity
                  displayValue(414, y, right, FreeSans12pt7b, String(mainDashboard.IndoorCollectors[i].Sensors[j].Value,0));
                  break;
                case 3 : 
                  // CO2
                  displayValue(498, y, right, FreeSans12pt7b, String(mainDashboard.IndoorCollectors[i].Sensors[j].Value,0));
                  break;
              }
          }
          y += 25;
        }      
        
      //Východ slunce
      displayValue(160, 170, right, FreeSans12pt7b, mainDashboard.Sunrise); 

      //Západ slunce    
      displayValue(160, 210, right, FreeSans12pt7b, mainDashboard.Sunset); 

      //Teplota rosnéhoho bodu - vnitřní­ (Jí­delna)    
      displayValue(160, 272, right, FreeSans12pt7b, String(mainDashboard.IndoorDewpointTemperature,0)); 

      //Teplota rosného bodu - venkovní
      displayValue(95, 272, right, FreeSans12pt7b, String(mainDashboard.OutdoorDewpointTemperature,0));   
       
      //Datum generování      
      displayValue(627, 380, right, FreeSans9pt7b, mainDashboard.GenerationDateTime);   
      //displayValue(530, 375, left, Cousine_Regular_8, mainDashboard.GenerationDateTime);   
    }
    while (display.nextPage());   
  }
  free(graph_Image_Bitmap);
  Serial.printf("drawDashboard - End FreeHeap(%i)\n", ESP.getFreeHeap());
  
}

void setup() {
  Serial.begin(115200);
  Serial.println("Start");
  
  //E-Paper
  display.init(115200);
  
  //WiFi
  WiFi.mode(WIFI_OFF);        
  delay(500);
  Serial.println();
  WiFi.hostname("Dashboard-ESP");
  WiFi.setAutoConnect(true);
  WiFi.setAutoReconnect(true);

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
  Serial.printf("IP address: %s \n", WiFi.localIP().toString().c_str());

  getDashboardBase();
  getDashboardSunState();
  getDashboardDewPoints();  
  getDashboardOutdoorCollectors();
  getDashboardIndoorCollectors();
  getDashboardGraphs();
  drawDashboard();
 
  Serial.println("End");

  ESP.deepSleep(5*60e6, WAKE_RF_DEFAULT);
}

void loop() {
}
