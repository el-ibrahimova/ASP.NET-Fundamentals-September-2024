using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CinemaApp.Data.Models;
using CinemaApp.Services.Mapping;

namespace CinemaApp.Web.ViewModels.Ticket
{
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