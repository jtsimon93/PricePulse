using PricePulse.Core.Entities;

namespace PricePulse.Core.Interfaces;

public interface IConsumerPriceIndexSeriesRepository
{
    Task<ConsumerPriceIndexSeries> AddAsync(ConsumerPriceIndexSeries series);
    Task<ConsumerPriceIndexSeries?> GetBySeriesIdAsync(string seriesId);
    Task<IEnumerable<ConsumerPriceIndexSeries>> GetAllAsync();
}