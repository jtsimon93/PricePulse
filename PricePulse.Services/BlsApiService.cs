using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using PricePulse.Core.Entities;
using PricePulse.Core.Interfaces;
using PricePulse.Core.Models.BLS;

namespace PricePulse.Services;

public class BlsApiService : IBlsApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<BlsApiService> _logger;
    private readonly string _blsBaseUrl = "https://api.bls.gov/publicAPI/v2/timeseries/data/";

    public BlsApiService(HttpClient httpClient, ILogger<BlsApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
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
            {
                throw new HttpRequestException($"BLS API request failed with status code {response.StatusCode}");
            }

            var responseData = await response.Content.ReadAsStringAsync();

            var blsResponse = JsonSerializer.Deserialize<BlsApiResponse>(responseData);

            if (blsResponse == null || blsResponse.Status != "REQUEST_SUCCEEDED")
            {
                throw new Exception("Failed to retrieve data from BLS API");
            }

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
        }

        return [];

    }

    public async Task<IEnumerable<ConsumerPriceIndexEntry>> FetchLatestDataAsync(string seriesId)
    {
        throw new NotImplementedException();
    }
    
    private int ParseMonth(string period)
    {
        return int.Parse(period.Replace("M", ""));
    }
}