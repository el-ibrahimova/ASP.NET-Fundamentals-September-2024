using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SeminarHub.Data.Models
{
    [Comment("Categories")]
    public class Category
    {
        [Key]
        [Comment("Primary key")]
       public int Id { get; set; }

        [Required]
        [MaxLength(Common.ApplicationConstants.CategoryNameMaxLength)]
        [Comment("Name of the category")]
        public string Name { get; set; } = null!;

        public IEnumerable<Seminar> Seminars { get; set; } = new HashSet<Seminar>();
    }
}
