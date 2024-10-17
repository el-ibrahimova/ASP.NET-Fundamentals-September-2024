using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Common.ApplicationConstants;

namespace SeminarHub.Models
{
    public class AddSeminarViewModel
    {
        [Required]
        [MaxLength(TopicMaxLength)]
        [MinLength(TopicMinLength)]
        public string Topic { get; set; } = null!;

        [Required]
        [MaxLength(LecturerMaxLength)]
        [MinLength(LecturerMinLength)]
        public string Lecturer { get; set; } = null!;

        [Required]
        [MaxLength(DetailsMaxLength)]
        [MinLength(DetailsMinLength)]
        public string Details { get; set; } = null!;

       [Required] 
        public string DateAndTime { get; set; } = null!;


        public int? Duration { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
