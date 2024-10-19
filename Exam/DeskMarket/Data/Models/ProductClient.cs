using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Data.Models
{
    [Comment("Mapping table with products and clients")]
    public class ProductClient
    {
        [Required]
        [Comment("ID of the product")]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [Comment("Product")]
        public Product Product { get; set; } = null!;

        [Required]
        [Comment("ID of the client")]
        public string ClientId { get; set; } = null!;

        [ForeignKey(nameof(ClientId))]
        [Comment("Client")]
        public IdentityUser Client { get; set; } = null!;
    }
}
