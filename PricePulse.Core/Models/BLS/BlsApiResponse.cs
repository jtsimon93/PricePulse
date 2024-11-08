namespace PricePulse.Core.Models.BLS;

public class BlsApiResponse
{
    public string Status { get; set; } // Status of the request, e.g., "REQUEST_SUCCEEDED"
    public BlsResults Results { get; set; } // Contains the main data result set
}