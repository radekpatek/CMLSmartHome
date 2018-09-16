using CMLSmartHomeCollector.Classes;
using CMLSmartHomeCollector.Interfaces;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;

namespace CMLSmartHomeCollector
{
    class Program
    {
        public static void Main(string[] args)
        {
            // create service collection
            var services = new ServiceCollection();
            ConfigureServices(services);

            // create service provider
            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILog>();

            try
            {
                logger.Info("Start CMLSmartHomeController");

                var configuration = serviceProvider.GetService<IConfiguration>();
      
                // Run collector
                serviceProvider.GetService<CollectorBase>().Run();

                logger.Info("Stop CMLSmartHomeController");
            }
            catch (System.Exception e)
            {
                logger.Error(string.Format("MessageText: {0}", e.Message), e);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {

            // Log config
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            var logger = LogManager.GetLogger(typeof(Program));

            services.AddSingleton(logger);
            services.AddLogging();

            // build config
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            services.AddOptions();

            services.AddSingleton<IConfiguration>(configuration.GetSection("App"));
            services.AddSingleton<IRestClient>(new RestClient(logger, configuration.GetSection("App")));
            services.AddTransient<CollectorBase>();
        }
    }
}
