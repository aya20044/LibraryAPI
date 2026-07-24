using LibraryAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTOs
{
    public class CategoryDto
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public ICollection<BookDto> Books { get; set; } = [];

    }
}
