using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskBoard.Data;
using static TaskBoard.Data.DataConstants;

namespace TaskBoard.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(TaskTitleMinLength)]
        [MaxLength(TaskTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(TaskDescriptionMinLength)]
        [MaxLength(TaskDescriptionMaxLength)]

        public string Description { get; set; } = null!;

        public int? BoardId { get; set; }

        [Required] public string Owner { get; set; } = null!;
    }
}