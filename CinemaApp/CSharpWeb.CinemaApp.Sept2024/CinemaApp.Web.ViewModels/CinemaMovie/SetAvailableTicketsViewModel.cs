using CinemaApp.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Web.ViewModels.CinemaMovie
{
    using Data.Models;
    using static Common.EntityValidationConstants.CinemaMovie;
    using static Common.EntityValidationMessages.CinemaMovie;
    public class SetAvailableTicketsViewModel : IMapTo<CinemaMovie>
    {
        [Required]
        public string CinemaId { get; set; } = null!;

        [Required] public string MovieId { get; set; } = null!;

        [Required(ErrorMessage = AvailableTicketsRequiredMessage)]
        [Range(AvailableTicketsMinValue, AvailableTicketsMaxValue, ErrorMessage = AvailableTicketsRangeMessage)]
        public int AvailableTickets { get; set; }
    }
}