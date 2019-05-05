__author__ = 'Radek'

import configparser

class Config:
    def __init__(self):
        config = configparser.ConfigParser()
        config.read('CMLSmartHomeDashboard.ini')

        self.loggerAddress =  config['Logger']['loggerAddress']
        self.loggerPort =  config['Logger']['loggerPort']
        self.loggerLevel =  config['Logger']['loggerLevel']

        self.controllerUrl =  config['CMLSmartHomeController']['url']
        self.controllerRestApiMainDashboard = config['CMLSmartHomeController']['restApiMainDashboard']
        self.urlRequestTimeoutSec = config['CMLSmartHomeController']['urlRequestTimeoutSec']
