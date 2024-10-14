using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Models
{
    [PrimaryKey(nameof(CollectorId), nameof(BookId))]
    [Comment("User Books")]
    public class IdentityUserBook
    {
        [Comment("Book collector")] 
        public string CollectorId { get; set; } = null!;

        [ForeignKey(nameof(CollectorId))]
        [Comment("Collector")]
        public IdentityUserBook Collector { get; set; } = null!;

        [Comment("Book Id")]
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        [Comment("Book")]
        public Book Book { get; set; } = null!;
    }
}
