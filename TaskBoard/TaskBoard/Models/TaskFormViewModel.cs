using System.ComponentModel.DataAnnotations;
using static TaskBoard.Data.DataConstants;

namespace TaskBoard.Models
{
    public class TaskFormViewModel
    { public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequireError)]
        [StringLength(TaskTitleMaxLength, 
            MinimumLength = TaskTitleMinLength, 
            ErrorMessage = ErrorMessages.StringLengthError)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = ErrorMessages.RequireError)]
        [StringLength(TaskDescriptionMaxLength,
            MinimumLength = TaskDescriptionMinLength,
            ErrorMessage = ErrorMessages.StringLengthError)]
        public string Description { get; set; } = null!;
        
        public int? BoardId { get; set; }

        public IEnumerable<TaskBoardModel> Boards { get; set; } = new List<TaskBoardModel>();

    }
}
