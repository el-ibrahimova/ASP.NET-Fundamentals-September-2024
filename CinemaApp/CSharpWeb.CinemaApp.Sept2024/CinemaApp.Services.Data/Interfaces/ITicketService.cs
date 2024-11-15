using CinemaApp.Web.ViewModels.CinemaMovie;

namespace CinemaApp.Services.Data.Interfaces
{
    public interface ITicketService
    {
        public Task<bool> SetAvailableTicketsAsync(SetAvailableTicketsViewModel model);
    }
}
