using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Models
{
    [Comment("User Books")]
    public class IdentityUserBook
    {
        [Comment("Book collector")] 
        public string CollectorId { get; set; } = null!;

        [ForeignKey(nameof(CollectorId))]
        [Comment("Collector")]
        public IdentityUser Collector { get; set; } = null!;

        [Comment("Book Id")]
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        [Comment("Book")]
        public Book Book { get; set; } = null!;
    }
}
