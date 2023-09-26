namespace floral_fusion.Models;
public class Product
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Image { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
    // public string? Tags { get; set; }
    // [DataType(DataType.Date)]
    // public DateTime? CreatedAt { get; set; }
    // public string? CreatedAt { get; set; }
}