using System.ComponentModel.DataAnnotations;

namespace Songs.DTOs
{
    public class ArtistDTO
    {
        [Required(ErrorMessage = "Artistnamn måste anges")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Artistnamn måste vara mellan 1 och 100 bokstäver")]
        public required string ArtistName { get; set; }

        public int? Birth { get; set; }
        public string? Nationality { get; set; }
    }
}
