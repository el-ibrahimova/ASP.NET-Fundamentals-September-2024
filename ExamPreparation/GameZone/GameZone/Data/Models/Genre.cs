using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Data.Models
{
    public class Genre
    {
        [Key]
        [Comment("Genre Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [Comment("Ganre name")]
        public string Name { get; set; } = null!;

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
