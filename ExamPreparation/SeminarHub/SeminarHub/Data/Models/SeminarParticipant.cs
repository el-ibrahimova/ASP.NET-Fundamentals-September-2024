using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace SeminarHub.Data.Models
{
    [Comment("Seminar Participant")]
    public class SeminarParticipant
    {
        [Required]
        [Comment("Primary key of seminar")]
        public int SeminarId { get; set; }

        [ForeignKey(nameof(SeminarId))]
        public Seminar Seminar { get; set; } = null!;


        [Required]
        [Comment("Primary key of participant")]
        public string ParticipantId { get; set; } = null!;

        [ForeignKey(nameof(ParticipantId))]
        public IdentityUser Participant { get; set; } = null!;
    }
}
