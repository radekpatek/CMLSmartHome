using CMLSmartHomeController.Model;
using CMLSmartHomeCommon.Model;
using Microsoft.EntityFrameworkCore;

namespace CMLSmartHomeController.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Collector> Collectors { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SmartHomeController> Controllers { get; set; }
        public DbSet<SensorRecord> SensorRecords { get; set; }
        public DbSet<SensorRecordArchive> SensorRecordsArchive { get; set; }
        public DbSet<VSensorRecord> VSensorRecords { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<WeatherForecast> WeatherForecast { get; set; }
        public DbSet<WeatherForecast.CurrentState> WeatherForecastCurrentState { get; set; }
        public DbSet<WeatherForecast.HourlyState> WeatherForecastHourlyState { get; set; }
        public DbSet<WeatherForecast.DailyState> WeatherForecastDailyState { get; set; }
        public DbSet<WeatherForecast.Weather> WeatherForecastWeather { get; set; }
        public DbSet<WeatherForecast.DailyTemp> WeatherForecastDailyTemperature { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SmartHomeController>().ToTable("Controller");
            
            modelBuilder.Entity<Collector>().ToTable("Collectors");
            
            modelBuilder.Entity<Sensor>().ToTable("Sensors");

            modelBuilder.Entity<SensorRecord>()
                .ToTable("SensorRecords")
                .HasIndex(p => new { p.SensorId, p.DateTime }); ;

            modelBuilder.Entity<SensorRecordArchive>()
                .ToTable("SensorRecordsArchive")
                .HasIndex(p => new { p.SensorId, p.DateTime }); ;

            modelBuilder.Entity<VSensorRecord>()
                .ToTable("VSensorRecords")
                .HasIndex(p => new { p.SensorId, p.DateTime }); ;

            modelBuilder.Entity<Dashboard>().ToTable("Dashboard");

            modelBuilder.Entity<WeatherForecast.DailyTemp>(entity =>
            {
                entity.ToTable("WeatherForecastDailyTemperature");
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<WeatherForecast.CurrentState>(entity =>
            {
                entity.ToTable("WeatherForecastCurrentState");
                entity.HasKey(p => p.Id);
                entity.HasMany(m => m.WeatherList)
                      .WithOne()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WeatherForecast.HourlyState>(entity =>
            {
                entity.ToTable("WeatherForecastHourlyState");
                entity.HasKey(p => p.Id);
                entity.HasMany(m => m.WeatherList)
                      .WithOne()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WeatherForecast.DailyState>(entity =>
            {
                entity.ToTable("WeatherForecastDailyState");
                entity.HasKey(p => p.Id);
                entity.HasMany(m => m.WeatherList)
                      .WithOne()
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(m => m.Temperature)
                      .WithOne(m => m.DailyStateTemperature)
                      .HasForeignKey<WeatherForecast.DailyTemp>(b => b.DailyStateTemperatureId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(false);
                entity.HasOne(m => m.FeelsLikeTemperature)
                      .WithOne(m => m.DailyStateFeelsLikeTemperature)
                      .HasForeignKey<WeatherForecast.DailyTemp>(b => b.DailyStateFeelsLikeTemperatureId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WeatherForecast>(entity =>
            {
                entity.ToTable("WeatherForecast");
                entity.HasKey(p => p.Id);
                entity.HasOne(m => m.Current)
                      .WithOne(m => m.WeatherForecast)
                      .HasForeignKey<WeatherForecast.CurrentState>(b => b.WeatherForecastId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(m => m.Hourly)
                      .WithOne(m => m.WeatherForecast)                      
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(true);
                entity.HasMany(m => m.Daily)
                      .WithOne(m => m.WeatherForecast)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(true);
            });

        }

    }
}
