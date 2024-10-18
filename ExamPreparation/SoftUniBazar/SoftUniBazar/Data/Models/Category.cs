using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
       
        [Required]
        [MaxLength(Common.EntityConstants.CategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Ad> Ads { get; set; } = new HashSet<Ad>();
    }
}
