using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static TaskBoard.Data.DataConstants;

namespace TaskBoard.Data
{
    public class Board
    {
        [Key]
        [Comment("Board identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(BoardNameMaxLength)]
        [Comment("Board name")]
        public string Name { get; set; } = null!;

        public IEnumerable<Task> Tasks { get; set; } = new List<Task>();

    }
}
