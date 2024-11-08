using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using PricePulse.Core.Entities;
using PricePulse.Core.Interfaces;
using PricePulse.Core.Models.BLS;

namespace PricePulse.Services;

public class BlsApiService : IBlsApiService
{
    private readonly string _blsBaseUrl = "https://api.bls.gov/publicAPI/v2/timeseries/data/";
    private readonly IConsumerPriceIndexEntryRepository _entryRepository;
    private readonly HttpClient _httpClient;
    private readonly ILogger<BlsApiService> _logger;

    public BlsApiService(HttpClient httpClient, ILogger<BlsApiService> logger,
        IConsumerPriceIndexEntryRepository entryRepository)
    {
        _httpClient = httpClient;
        _logger = logger;
        _entryRepository = entryRepository;
    }

    public async Task<IEnumerable<ConsumerPriceIndexEntry>> FetchHistoricalDataAsync(string seriesId, int startYear,
        int endYear)
    {
        var payload = new
        {
            seriesid = new[] { seriesId },
            startyear = startYear,
            endyear = endYear,
            registrationkey = ""
        };

        var jsonPayload = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(_blsBaseUrl, jsonPayload);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"BLS API request failed with status code {response.StatusCode}");

            var responseData = await response.Content.ReadAsStringAsync();

            var blsResponse = JsonSerializer.Deserialize<BlsApiResponse>(responseData);

            if (blsResponse == null || blsResponse.Status != "REQUEST_SUCCEEDED")
                throw new Exception("Failed to retrieve data from BLS API");

            // Convert BLS data to ConsumerPriceIndexEntry objects
            var entries = blsResponse.Results.Series
                .SelectMany(series => series.Data.Select(data => new ConsumerPriceIndexEntry
                {
                    SeriesId = seriesId,
                    Year = int.Parse(data.Year),
                    Month = ParseMonth(data.Period),
                    Value = decimal.Parse(data.Value),
                    DateRetrieved = DateTime.UtcNow
                }))
                .ToList();

            return entries;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch historical data from BLS API");
            return [];
        }
    }

    public async Task<IEnumerable<ConsumerPriceIndexEntry>> FetchLatestDataAsync(string seriesId)
    {
        try
        {
            var latestEntry = await _entryRepository.GetLatestEntryBySeriesIdAsync(seriesId);

            // Determine the starting year and month for fetching new data
            var startYear = latestEntry?.Year ?? DateTime.UtcNow.Year;
            var startMonth = latestEntry != null ? latestEntry.Month + 1 : 1;

            // Adjust the month and year if the month exceeds 12
            if (startMonth > 12)
            {
                startMonth = 1;
                startYear++;
            }

            var payload = new
            {
                seriesid = new[] { seriesId },
                startyear = startYear,
                endyear = DateTime.UtcNow.Year,
                registrationkey = ""
            };

            var jsonPayload = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_blsBaseUrl, jsonPayload);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"BLS API request failed with status code {response.StatusCode}");

            var responseData = await response.Content.ReadAsStringAsync();
            var blsResponse = JsonSerializer.Deserialize<BlsApiResponse>(responseData);

            if (blsResponse == null || blsResponse.Status != "REQUEST_SUCCEEDED")
                throw new Exception("Failed to retrieve data from BLS API");

            var newEntries = blsResponse.Results.Series
                .SelectMany(series => series.Data.Select(data => new ConsumerPriceIndexEntry
                {
                    SeriesId = seriesId,
                    Year = int.Parse(data.Year),
                    Month = ParseMonth(data.Period),
                    Value = decimal.Parse(data.Value),
                    DateRetrieved = DateTime.UtcNow
                }))
                .Where(entry => entry.Year > startYear || (entry.Year == startYear && entry.Month >= startMonth))
                .ToList();

            return newEntries;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch latest data from BLS API");
            return [];
        }
    }


    private int ParseMonth(string period)
    {
        return int.Parse(period.Replace("M", ""));
    }
}