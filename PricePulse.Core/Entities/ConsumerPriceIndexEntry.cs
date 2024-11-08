using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PricePulse.Core.Entities;

public class ConsumerPriceIndexEntry
{
    [Key]
    public int Id { get; set; }
    
    public int ConsumerPriceIndexSeriesId { get; set; }
    
    [ForeignKey("ConsumerPriceIndexSeriesId")]
    public ConsumerPriceIndexSeries ConsumerPriceIndexSeries { get; set; }
    
    [Required]
    public int Year { get; set; }
    
    [Required]
    public int Month { get; set; }
    
    [Required]
    public decimal Value { get; set; } 
    
    [Required]
    public DateTime DateRetrieved { get; set; }
}