using Homies.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Homies.Data.DataConstants;

namespace Homies.Models
{
    public class EventFormViewModel
    {
        [Required(ErrorMessage = RequireMessage)]
        [StringLength(EventNameMaxLength, 
            MinimumLength = EventNameMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = RequireMessage)]
        [StringLength(EventDescriptionMaxLength,
            MinimumLength = EventDescriptionMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; } = null!;

      
        [Required]
        public string Start { get; set; }

        [Required]
        public string End { get; set; }

        [Required]
        public int TypeId { get; set; }


        [ForeignKey(nameof(TypeId))]
        public Type Type { get; set; } = null!;

        public IList<EventParticipant> EventsParticipants { get; set; } = new List<EventParticipant>();
    }
}
