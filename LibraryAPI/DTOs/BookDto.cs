using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.DTOs
{
    public class BookDto
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public string Language { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateOnly PublishedDate { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Author is required.")]
        public int AuthorId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
    }
}
