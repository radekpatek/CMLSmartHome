using AutoMapper;
using CMLSmartHomeController.Model;
using CMLSmartHomeController.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMLSmartHomeController.JobScheduler.Jobs
{
    /// <summary>
    /// WeatherRecordArchiveJob
    /// </summary>
    [DisallowConcurrentExecution]
    public class WeatherRecordArchiveJob : IJob
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger<WeatherRecordArchiveJob> _logger;
        private readonly IConfiguration _configuration;

        public WeatherRecordArchiveJob(ILogger<WeatherRecordArchiveJob> logger, IServiceProvider provider, IConfiguration configuration)
        {
            _logger = logger;
            _provider = provider;
            _configuration = configuration;
        }

        private class Settings
        {
            public int ArchiveDays { get; set; }
        }


        Task IJob.Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("WeatherRecordArchiveJob - Started");

            try
            {
                // Create a new scope
                var scope = _provider.CreateScope();

                // Resolve the Scoped service
                var _context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                // Configuration
                var settings = _configuration.GetSection("WeatherRecordArchiveJob").Get<Settings>();

                //settings.ArchiveDays
                var archiveDays = settings.ArchiveDays;

                // Get records to archive
                var records = _context.SensorRecords.Where(t => t.DateTime < DateTime.Now.AddDays(-1 * archiveDays));

                if (records != null)
                { 
                    var recordList = records.ToList();

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<SensorRecord, SensorRecordArchive>());
                    var mapper = new Mapper(config);
                    List<SensorRecordArchive> recordsToArchive = mapper.Map<List<SensorRecord>, List<SensorRecordArchive>>(recordList);

                    // Archive
                    if (recordsToArchive != null)
                    {
                        _context.SensorRecordsArchive.AddRange(recordsToArchive);
                        _context.SensorRecords.RemoveRange(records);
                        var rowsAffected = _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("WeatherRecordArchiveJob - Error: {0} ", ex.Message));
            }

            _logger.LogInformation("WeatherRecordArchiveJob - Finished");

            return Task.CompletedTask;
        }
    }
}