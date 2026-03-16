using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourApp.Models
{
    public class Book
    {
        public int bookId { get; set; }

        [Required]
        [StringLength(200)]
        public string title { get; set; } = "";

        [Required]
        [StringLength(20)]
        public string genre { get; set; } = "";

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal price { get; set; }

        [Required]
        [Display(Name = "Publication Year")]
        public int publicationYear { get; set; }

        // foreign key to Author
        [Required]
        [Display(Name = "Author")]
        public int authorId { get; set; }

        // navigation property
        public Author? Author { get; set; }
    }
}
