using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DeskMarket.Data.Models
{
    [Comment("Categories of the products")]
    public class Category
    {
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        [Required]
        [MaxLength(Common.EntityConstants.CategoryNameMaxLength)]
        [Comment("Name of the category")]
        public string Name { get; set; } = null!;

        [Comment("Collection with products")]
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
