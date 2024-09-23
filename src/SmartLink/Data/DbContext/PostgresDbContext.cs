using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.DbContext;

public class PostgresDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options) { }

    // Define your database tables as DbSet properties
    public DbSet<SensorData> SensorData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define your entity relationships and configurations here
        modelBuilder.Entity<SensorData>().ToTable("sensor_data");
    }
}

