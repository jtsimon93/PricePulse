namespace PricePulse.Core.Models.BLS;

public class BlsSeries
{
    public string SeriesID { get; set; } // ID of the series, e.g., "CUUR0000SAF116"
    public List<BlsData> Data { get; set; } // List of data points for this series
}