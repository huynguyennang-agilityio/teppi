namespace Teppi.Share.Entities;

public class Category: BaseEntity
{
    [Required] // Data annotations needed to configure as required
    public required string Name { get; set; }
    
    [Column(TypeName = "varchar(200)")]
    public required string ImageUrl { get; set; }

    public ICollection<Course> Courses { get; set; } = new List<Course>();

}