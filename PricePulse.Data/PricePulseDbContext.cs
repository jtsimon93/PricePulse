using Microsoft.EntityFrameworkCore;
using PricePulse.Core.Entities;

namespace PricePulse.Data;

public class PricePulseDbContext : DbContext
{
    public PricePulseDbContext(DbContextOptions<PricePulseDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<ConsumerPriceIndexSeries> ConsumerPriceIndexSeries { get; set; }
    public DbSet<ConsumerPriceIndexEntry> ConsumerPriceIndexEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ConsumerPriceIndexSeries>()
            .HasIndex(c => new { c.SeriesId })
            .IsUnique();

        // Composite unique constraint on Year, Month, and SeriesId to avoid duplicates
        modelBuilder.Entity<ConsumerPriceIndexEntry>()
            .HasIndex(e => new { e.ConsumerPriceIndexSeriesId, e.Year, e.Month })
            .IsUnique();
    }
    
}