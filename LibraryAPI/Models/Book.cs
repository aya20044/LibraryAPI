using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string? Description { get; set; }
        [Required]
        public string Language { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public DateOnly PublishedDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; } = default!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
