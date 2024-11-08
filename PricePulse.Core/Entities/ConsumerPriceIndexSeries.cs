using System.ComponentModel.DataAnnotations;

namespace PricePulse.Core.Entities;

public class ConsumerPriceIndexSeries
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(255)]
    [Required]
    public string SeriesId { get; set; }
    
    [MaxLength(255)]
    [Required]
    public string SeriesTitle { get; set; }
    
    [MaxLength(255)]
    [Required]
    public string Area { get; set; }
    
    [MaxLength(255)]
    [Required]
    public string Item { get; set; }
    
    [Required]
    public int Year { get; set; }
    
    [Required]
    public int Month { get; set; }
    
    [Required]
    public decimal Value { get; set; }
    
    [Required]
    public bool IsSeasonallyAdjusted { get; set; }
    
    [Required]
    public string Frequency { get; set; }
    
    [Required]
    public string Source { get; set; }
    
    [Required]
    public string UnitOfMeasure { get; set; }
    
    [Required]
    public string DataType { get; set; }
    
    [Required]
    public string Currency { get; set; } = "USD";
    
    [Required]
    public DateTime DateRetrieved { get; set; }
    
    [Required]
    public string? Description { get; set; }
    
    [Required]
    public bool IsEnergyItem { get; set; }
    
    [Required]
    public bool IsFoodItem { get; set; }
    
    [Required]
    public bool IsHousingItem { get; set; }
    
    [Required]
    public bool IsMedicalItem { get; set; }
    
    [Required]
    public bool IsTransportationItem { get; set; }
    
    [Required]
    public bool IsApparelItem { get; set; }
}