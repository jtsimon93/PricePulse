using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PricePulse.Core.Entities;
using PricePulse.Core.Interfaces;

namespace PricePulse.Data.Repositories;

public class ConsumerPriceIndexEntryRepository : IConsumerPriceIndexEntryRepository
{
    private readonly PricePulseDbContext _dbContext;
    private readonly ILogger<ConsumerPriceIndexEntryRepository> _logger;
    
    public ConsumerPriceIndexEntryRepository(PricePulseDbContext dbContext, ILogger<ConsumerPriceIndexEntryRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<ConsumerPriceIndexEntry> AddAsync(ConsumerPriceIndexEntry entry)
    {
        _dbContext.ConsumerPriceIndexEntries.Add(entry);
        await _dbContext.SaveChangesAsync();
        return entry;
    }
    
    public async Task<IEnumerable<ConsumerPriceIndexEntry>> GetAllAsync()
    {
        return await _dbContext.ConsumerPriceIndexEntries
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<IEnumerable<ConsumerPriceIndexEntry>> GetBySeriesIdAsync(string seriesId)
    {
        return await _dbContext.ConsumerPriceIndexEntries
            .AsNoTracking()
            .Where(entry => entry.SeriesId == seriesId)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<ConsumerPriceIndexEntry>> GetBySeriesIdAndYearAsync(string seriesId, int year)
    {
        return await _dbContext.ConsumerPriceIndexEntries
            .AsNoTracking()
            .Where(entry => entry.SeriesId == seriesId && entry.Year == year)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<ConsumerPriceIndexEntry>> GetBySeriesIdAndMonthAsync(string seriesId, int month)
    {
        return await _dbContext.ConsumerPriceIndexEntries
            .AsNoTracking()
            .Where(entry => entry.SeriesId == seriesId && entry.Month == month)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<ConsumerPriceIndexEntry>> GetBySeriesIdAndYearAndMonthAsync(string seriesId, int year, int month)
    {
        return await _dbContext.ConsumerPriceIndexEntries
            .AsNoTracking()
            .Where(entry => entry.SeriesId == seriesId && entry.Year == year && entry.Month == month)
            .ToListAsync();
    }
    
}