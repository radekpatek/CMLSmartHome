import os, time, sys, subprocess, json

MYNAME = 'REST'
flag_debugging = True

# ----------------------------------------------------------
# Weather Underground parameters
URL_LEFT = 'http://192.168.1.152/api/'
URL_RIGHT = 'MainDashboard'
WU_API_KEY = ''
FULL_URL = URL_LEFT + WU_API_KEY + URL_RIGHT
URL_REQUEST_TIMEOUT_SEC = 60
FORMAT_DATE = "%d-%m-%Y" 
FORMAT_TIME = "%H:%M:%S" 

# ----------------------------------------------------------
# Time-stamp logger; API is like C-language printf
def logger(arg_format, *arg_list):
	now = time.strftime("%Y-%m-%d %H:%M:%S ", time.localtime())
	fmt = "{nstr} {fstr}".format(nstr=now, fstr=arg_format)
	print(fmt % arg_list)
	sys.stdout.flush()

# ----------------------------------------------------------
# Must be Python 3.x
if sys.version_info[0] < 3:
	logger("%s: *** Requires Python 3", MYNAME)
	exit(86)

# ----------------------------------------------------------
# Import Python 3 libraries
from tkinter import *
import urllib.request

# ----------------------------------------------------------
# Procedure: Get date, time, farenheit-temperature, and celsius-temperature
def get_display_data():
	global count_down, str_temp, str_condition, flag_url
	if flag_debugging:
		logger("%s: DEBUG get_display_data begin", MYNAME)
	try:
                url_handle = urllib.request.urlopen(FULL_URL, None, URL_REQUEST_TIMEOUT_SEC)
                data = url_handle.read()
                encoding = url_handle.info().get_content_charset('utf-8')
                parsed_json = json.loads(data.decode(encoding))
                str_inttemp = parsed_json['internalTemperature']
                str_inthum = parsed_json['internalHumidity']
                str_outtemp = parsed_json['outdoorTemperature']
                str_outhum = parsed_json['outdoorHumidity']                
                url_handle.close()
                flag_url = True
                if flag_debugging:
                    logger("%s: DEBUG weather access success", MYNAME)
	except:
                if flag_debugging:
                        logger("%s: DEBUG Oops, weather access failed", MYNAME)
                flag_url = False
                
	now = time.localtime()
	str_date = time.strftime(FORMAT_DATE, now)
	str_time = time.strftime(FORMAT_TIME, now)
	if flag_debugging:
		logger("%s: DEBUG Display date = %s, time = %s, temp = %s - %s", 
				MYNAME, str_date, str_time, str_temp, str_condition)
	return( str_date, str_time, str_temp, str_condition )
# ----------------------------------------------------------
# Procedure: Main Loop
get_display_data()

