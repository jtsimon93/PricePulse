using PricePulse.Core.Entities;

namespace PricePulse.Core.Interfaces;

public interface IConsumerPriceIndexEntryRepository
{
    Task<ConsumerPriceIndexEntry> AddAsync(ConsumerPriceIndexEntry entry);
    Task<IEnumerable<ConsumerPriceIndexEntry>> GetAllAsync();
    Task<IEnumerable<ConsumerPriceIndexEntry>> GetBySeriesIdAsync(string seriesId);
    Task<IEnumerable<ConsumerPriceIndexEntry>> GetBySeriesIdAndYearAsync(string seriesId, int year);
    Task<IEnumerable<ConsumerPriceIndexEntry>> GetBySeriesIdAndMonthAsync(string seriesId, int month);
    Task<IEnumerable<ConsumerPriceIndexEntry>> GetBySeriesIdAndYearAndMonthAsync(string seriesId, int year, int month);
}