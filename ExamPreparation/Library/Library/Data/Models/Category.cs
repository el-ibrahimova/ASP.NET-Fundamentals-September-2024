using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace Library.Data.Models
{
    [Comment("Categories for the books")]
    public class Category
    {
        [Key]
        [Comment("Category primary key")]
        public int Id { get; set; }

        [Required]
        [MaxLength(Common.DataConstants.CategoryNameMaxLength)]
        [Comment("Name of the category")]
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
