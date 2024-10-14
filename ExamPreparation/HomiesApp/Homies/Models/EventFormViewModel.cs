using Homies.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Homies.Data.DataConstants;
using Type = Homies.Data.Models.Type;

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


        [Required(ErrorMessage = RequireMessage)]
        public string Start { get; set; } = null!;

        [Required(ErrorMessage = RequireMessage)]
        public string End { get; set; } = null!;

        [Required(ErrorMessage = RequireMessage)]
        public int TypeId { get; set; }

        public IEnumerable<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();
    }
}
