using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Songs.Models;

[Index(nameof(ArtistName), IsUnique = true)]
public class Artist
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Artistnamn måste anges")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Artistnamn måste vara mellan 1 och 100 bokstäver")]
    public required string ArtistName { get; set; }

    public int? Birth {  get; set; }
    public string? Nationality { get; set; }

    [JsonIgnore]
    public List<Song>? Songs { get; set; }
}
