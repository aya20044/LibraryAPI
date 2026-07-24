using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTOs
{
    public class AuthorDto
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public ICollection<BookDto> Books { get; set; } = [];
    }
}
