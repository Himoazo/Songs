using System.ComponentModel.DataAnnotations;

namespace Songs.Models;

public class Song
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Songtitel måste anges")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Songtiteln måste vara mellan 1 och 100 bokstäver")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "Songlängd måste anges")]

    [Range(1, int.MaxValue, ErrorMessage = "Songlängd måste vara större än 0")]
    public int Length { get; set; }

    
    public List<Artist> Artist { get; set; } = null!;

    public List<Category> Category { get; set; } = null!;
}
