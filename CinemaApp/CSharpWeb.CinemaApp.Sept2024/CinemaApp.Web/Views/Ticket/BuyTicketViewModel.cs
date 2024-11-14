using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Views.Ticket
{
    using static Common.EntityValidationConstants.Ticket;
    using static Common.EntityValidationMessages.Ticket;
    public class BuyTicketViewModel
    {
        [Required]
        public string CinemaId { get; set; } = null!;

        [Required]
        public string MovieId { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Range(typeof(decimal), PriceMinValue, PriceMaxValue, ErrorMessage = IncorrectPriceMessage)]
        public decimal Price { get; set; }

        [Range(CountMinValue, CountMaxValue, ErrorMessage = IncorrectCountMessage)]
        public int Count { get; set; }

    }
}
