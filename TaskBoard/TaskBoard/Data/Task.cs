using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static TaskBoard.Data.DataConstants;

namespace TaskBoard.Data
{
    public class Task
    {
        [Key]
        [Comment("Task identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TaskTitleMaxLength)]
        [Comment("Task Title")]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(TaskDescriptionMaxLength)]
        [Comment("Task Description")]
        public string Description { get; set; } = null!;

        [Comment("Date of creation")]
        public DateTime? CreatedOn { get; set; }

        [Comment("Board identifier")]
        public int? BoardId { get; set; }

        [Required]
        [Comment("Application user identifier")]
        public string OwnerId { get; set; } = null!;


        [ForeignKey(nameof(BoardId))]
        public Board? Board { get; set; } = null!;

        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;
    }
}
