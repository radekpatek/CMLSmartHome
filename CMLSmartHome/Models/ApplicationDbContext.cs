using CMLSmartHomeCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CMLSmartHome.Models
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
        public DbSet<SensorRecord> SensorRecord { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SmartHomeController>().ToTable("Controller");
            modelBuilder.Entity<Collector>().ToTable("Collectors");
            modelBuilder.Entity<Sensor>().ToTable("Sensors");
            modelBuilder.Entity<SensorRecord>().ToTable("SensorRecords");
        }

    }
}
