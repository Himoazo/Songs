using System.ComponentModel.DataAnnotations;

namespace Songs.DTOs;

public class SongDTO
{
    [Required(ErrorMessage = "Songtitel måste anges")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Songtiteln måste vara mellan 1 och 100 bokstäver")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "Songlängd måste anges")]

    [Range(1, int.MaxValue, ErrorMessage = "Songlängd måste vara större än 0")]
    public int Length { get; set; }

    [Required(ErrorMessage = "ArtistId måste anges")]
    public List<int> ArtistId { get; set; } = [];

    [Required(ErrorMessage = "CategoryId måste anges")]
    public List<int> CategoryId { get; set; } = [];
}
