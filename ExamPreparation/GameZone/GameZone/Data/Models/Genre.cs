using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static GameZone.Common.ModelConstants;

namespace GameZone.Data.Models
{
    public class Genre
    {
        [Key]
        [Comment("Genre Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(GenreNameMaxLength)]
        [Comment("Ganre name")]
        public string Name { get; set; } = null!;

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
