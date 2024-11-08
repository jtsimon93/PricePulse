namespace PricePulse.Core.Models.BLS;

public class BlsData
{
    public string Year { get; set; } // Year of the data entry
    public string Period { get; set; } // Period of the data entry, e.g., "M01" for January
    public string PeriodName { get; set; } // Name of the period, e.g., "January"
    public string Value { get; set; } // Value of the data entry as a string
    public string Footnotes { get; set; } // Any footnotes associated with the data entry
}