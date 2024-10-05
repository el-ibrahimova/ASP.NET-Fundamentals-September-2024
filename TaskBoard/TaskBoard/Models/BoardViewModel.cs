using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using static TaskBoard.Data.DataConstants;

namespace TaskBoard.Models
{
    public class BoardViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TaskTitleMaxLength, MinimumLength = TaskTitleMinLength)]
        public string Name { get; set; } = null!;
        public IEnumerable<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}
