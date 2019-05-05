__author__ = 'Radek'

import logging
import logging.handlers

#import timber
#import graypy

class Logger:

    def __init__(self, server, port):
        #Create and configure logger
        #logging.basicConfig(filename="CMLSmartHomeDashboard.log",
        #                format='%(asctime)s %(message)s',
        #                filemode='w')

        #self.logger = logging.getLogger()
        #timber_handler = timber.TimberHandler(api_key='6809_d1d9b1115892d037:368c846532cd493106e235e117e54be56f462defe73e41422f3e6e6eab964885')
        #self.logger.addHandler(timber_handler)

        self.logger = logging.getLogger('CMLSmartHomeLogger')
        self.logger.setLevel(logging.INFO)

        #add handler to the logger
        handler = logging.handlers.SysLogHandler('/dev/log')

        #add syslog format to the handler
        formatter = logging.Formatter('Python: { "loggerName":"%(name)s", "timestamp":"%(asctime)s", "pathName":"%(pathname)s", "logRecordCreationTime":"%(created)f", "functionName":"%(funcName)s", "levelNo":"%(levelno)s", "lineNo":"%(lineno)d", "time":"%(msecs)d", "levelName":"%(levelname)s", "message":"%(message)s"}')

        handler.formatter = formatter
        self.logger.addHandler(handler)

        self.logger.info('Test Log')

        #graylog_handler = graypy.GELFHandler(server, port)
        #graylog_handler.setLevel(logging.INFO)
        #logger.addHandler(graylog_handler)

    def exception(self, exp):
        self.logger.exception(exp)
