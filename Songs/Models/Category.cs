using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Songs.Models;

[Index(nameof(Genre), IsUnique = true)]
public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Genre måste anges")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Genre måste vara mellan 1 och 100 bokstäver")]
    public required string Genre { get; set; }

    [JsonIgnore]
    public List<Song>? Songs { get; set; }
}
