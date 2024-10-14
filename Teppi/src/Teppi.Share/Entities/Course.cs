namespace Teppi.Share.Entities;

public class Course: BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    
    [MaxLength(500)]
    public required string ImageUrl { get; set; }
    public string? TagTitle { get; set; }
    
    // Add these lines
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}