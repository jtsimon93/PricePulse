using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PricePulse.Core.Entities;
using PricePulse.Core.Interfaces;

namespace PricePulse.Data.Repositories;

public class ConsumerPriceIndexSeriesRepository : IConsumerPriceIndexSeriesRepository
{
    private readonly PricePulseDbContext _dbContext;
    private readonly ILogger<ConsumerPriceIndexSeriesRepository> _logger;
    
    public ConsumerPriceIndexSeriesRepository(PricePulseDbContext dbContext, ILogger<ConsumerPriceIndexSeriesRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<ConsumerPriceIndexSeries> AddAsync(ConsumerPriceIndexSeries series)
    {
        _dbContext.ConsumerPriceIndexSeries.Add(series);
        await _dbContext.SaveChangesAsync();
        return series;
    }
    
    public async Task<ConsumerPriceIndexSeries?> GetBySeriesIdAsync(string seriesId)
    {
        return await _dbContext.ConsumerPriceIndexSeries
            .FirstOrDefaultAsync(series => series.SeriesId == seriesId);
    }
    
    public async Task<IEnumerable<ConsumerPriceIndexSeries>> GetAllAsync()
    {
        return await _dbContext.ConsumerPriceIndexSeries
            .AsNoTracking()
            .ToListAsync();
    }
    
    
    
}