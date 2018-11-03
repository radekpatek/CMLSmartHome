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
        public DbSet<Dashboard> Dashboards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SmartHomeController>().ToTable("Controller");
            modelBuilder.Entity<Collector>().ToTable("Collectors");
            modelBuilder.Entity<Sensor>().ToTable("Sensors");
            modelBuilder.Entity<SensorRecord>().ToTable("SensorRecords");
            modelBuilder.Entity<Dashboard>().ToTable("Dashboard");
        }

    }
}
