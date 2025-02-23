using Songs.Models;
using System.ComponentModel.DataAnnotations;

namespace Songs.DTOs
{
    public class CategoryDTO
    {
        [Required(ErrorMessage = "Genre måste anges")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Genre måste vara mellan 1 och 100 bokstäver")]
        public required string Genre { get; set; }
    }
}


