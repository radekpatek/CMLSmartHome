__author__ = 'Radek Pátek'

import urllib.request
import json

class Dashboard:
    def __init__(self):
        self.internalTemperature = 'N/A'
        self.internalHumidity = 'N/A'
        self.outdoorTemperature = 'N/A'
        self.internalHumidity = 'N/A'

class CMLSmartHomeAPI:
    def __init__(self, config, logger):
        self.url = config.controllerUrl
        self.logger = logger
        self.restApi = config.controllerRestApiMainDashboard
        self.urlRequestTimeoutSec = config.urlRequestTimeoutSec

    def getDashboardData(self):
        self.logger.info('getDashboardData - URL open')
        urlDashBoardData = self.url + self.restApi
        url_handle = urllib.request.urlopen(urlDashBoardData, None, int(self.urlRequestTimeoutSec))
        data = url_handle.read()
        encoding = url_handle.info().get_content_charset('utf-8')

        dashboard = Dashboard()
        parsedJson = json.loads(data.decode(encoding))
        dashboard.internalTemperature = parsedJson['internalTemperature']
        dashboard.internalHumidity = parsedJson['internalHumidity']
        dashboard.outdoorTemperature = parsedJson['outdoorTemperature']
        dashboard.outdoorHumidity = parsedJson['outdoorHumidity']
        url_handle.close()
        self.logger.info('getDashboardData - URL close')
        self.logger.info('getDashboardData - internalTemperature: ' + str(dashboard.internalTemperature))

        return( dashboard )





