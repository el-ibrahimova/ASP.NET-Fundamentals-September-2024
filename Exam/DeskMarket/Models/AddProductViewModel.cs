using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using static DeskMarket.Common.EntityConstants;

namespace DeskMarket.Models
{
    public class AddProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ProductNameMaxLength, MinimumLength = ProductNameMinLength)]
        public string ProductName { get; set; }=null!;

        [Required]
        [Range(ProductPriceMinValue, ProductPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(ProductDescriptionMaxLength, MinimumLength = ProductDescriptionMinLength)]
        public string Description { get; set; } = null!;

        public string? ImageUrl { get; set; }
        public string AddedOn { get; set; } = null!;

        public int CategoryId { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();
    }
}
