using System.ComponentModel.DataAnnotations;

namespace YourApp.Models
{
    public class Author
    {
        public int authorId { get; set; }

        [Required]
        [StringLength(100)]
        public string firstName { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string lastName { get; set; } = "";

        [StringLength(300)]
        public string bio { get; set; } = "";

        // navigation property - one author has many books
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
