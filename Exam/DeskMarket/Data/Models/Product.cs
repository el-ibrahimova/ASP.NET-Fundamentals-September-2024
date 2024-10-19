using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static DeskMarket.Common.EntityConstants;

namespace DeskMarket.Data.Models
{
    [Comment("Table with products")]
    public class Product
    {
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        [Comment("Name of the product")]
        public string ProductName { get; set; } = null!;

        [Required]
        [MaxLength(ProductDescriptionMaxLength)]
        [Comment("Description of the product")]
        public string Description { get; set; } = null!;

        [Required]
        [Comment("Product price")]
        public decimal Price { get; set; }

        [Comment("Image of the product")]
        public string? ImageUrl { get; set; }

        [Required]
        [Comment("ID of the seller")]
        public string SellerId { get; set; } = null!;

        [Required]
        [Comment("Seller of the product")]
        public IdentityUser Seller { get; set; } = null!;

        [Required]
        [Comment("Date the product was added")]
        public DateTime AddedOn { get; set; }

        [Required]
        [Comment("ID of the product category")]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        [Comment("Product category")]
        public Category Category { get; set; } = null!;

        [Comment("Shows that product is deleted or not")]
        public bool IsDeleted { get; set; }

        [Comment("Collection of products with clients")]
        public ICollection<ProductClient> ProductsClients { get; set; } = new HashSet<ProductClient>();
    }
}
