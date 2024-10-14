using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static Library.Common.DataConstants;  

namespace Library.Data.Models
{
    [Comment("Books for the library")]
    public class Book
    {
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        [Required]
        [MaxLength(BookTitleMaxLength)]
        [Comment("Book title")]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(BookAuthorMaxLength)]
        [Comment("Book author")]
        public string Author { get; set; } = null!;

        [Required]
        [MaxLength(BookDescriptionMaxLength)]
        [Comment("Book description")]
        public string Description { get; set; } = null!;

        [Required]
        [Comment("Url of the book image")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Comment("Book rating")]
        public decimal Rating { get; set; }

        [Required]
        [Comment("Book category identifier")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [Comment("Category of the book")]
        public Category Category { get; set; } = null!;

        public ICollection<IdentityUserBook> UsersBooks { get; set; } = new HashSet<IdentityUserBook>();
    }
}
