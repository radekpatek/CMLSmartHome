__author__ = 'Radek Pátek'

global logger, config

#import epd7in5
import CMLSmartHomeAPI as api
import Logger
import Config
import time

sleepTimeSec = 60

def main():
    config = Config.Config()

    logger = Logger.Logger(config.loggerAddress, config.loggerPort).logger
    homeAPI = api.CMLSmartHomeAPI( config, logger)

    logger.info('CMLSmartHomeDashboard - start')

    while True:
        try:
            data = homeAPI.getDashboardData()
        except Exception as e:
            logger.exception(e)

        time.sleep(sleepTimeSec)

# ----------------------------------------------------------
if __name__ == '__main__':
    main()