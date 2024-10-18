using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using static SoftUniBazar.Common.EntityConstants;

namespace SoftUniBazar.Models
{
    public class AdViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(AdNameMaxLength, MinimumLength = AdNameMinLength)]
        public string Name { get; set; } = null!;

       [Required]
        [StringLength(AdDescriptionMaxLength, MinimumLength = AdDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }


        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();

        }
}
