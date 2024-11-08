using PricePulse.Core.Entities;

namespace PricePulse.Core.Interfaces;

public interface IBlsApiService
{
    Task<IEnumerable<ConsumerPriceIndexEntry>> FetchHistoricalDataAsync(string seriesId, int startYear, int endYear);
    Task<IEnumerable<ConsumerPriceIndexEntry>> FetchLatestDataAsync(string seriesId);
}