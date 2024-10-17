using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static SeminarHub.Common.ApplicationConstants;

namespace SeminarHub.Data.Models
{
    [Comment("Seminar info")]
    public class Seminar
    {
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TopicMaxLength)]
        [Comment("Seminar's topic")]
        public string Topic { get; set; } = null!;

        [Required]
        [MaxLength(LecturerMaxLength)]
        [Comment("Lecturer name")]
        public string Lecturer { get; set; } = null!;

        [Required]
        [MaxLength(DetailsMaxLength)]
        [Comment("Details about the seminar")]
        public string Details { get; set; } = null!;

        [Required]
        [Comment("Organizer Identifier")]
        public string OrganizerId { get; set; } = null!;

        [Required]
        public IdentityUser Organizer { get; set; } = null!;

        [Required]
        [Comment("Date and time")]
        public DateTime DateAndTime { get; set; }

        [Comment("Duration of the seminar")]
        public int? Duration { get; set; }

        [Required]
        [Comment("Category identifier")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [Comment("Category of the seminar")]
        public Category Category { get; set; } = null!;

        public ICollection<SeminarParticipant> SeminarsParticipants { get; set; } = new HashSet<SeminarParticipant>();
    }
}
