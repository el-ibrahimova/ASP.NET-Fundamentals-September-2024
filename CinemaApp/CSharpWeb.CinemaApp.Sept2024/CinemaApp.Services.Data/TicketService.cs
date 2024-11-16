using CinemaApp.Common;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Interfaces;
using CinemaApp.Services.Data.Interfaces;
using CinemaApp.Services.Mapping;
using CinemaApp.Web.ViewModels.CinemaMovie;

namespace CinemaApp.Services.Data
{
    
    public class TicketService:BaseService, ITicketService
    {
        private readonly IRepository<CinemaMovie, object> cinemaMovieRepository;

        public TicketService(IRepository<CinemaMovie, object> cinemaMovieRepository)
        {
            this.cinemaMovieRepository = this.cinemaMovieRepository;
        }
        public Task<bool> SetAvailableTicketsAsync(SetAvailableTicketsViewModel model)
        {
            CinemaMovie cinemaMovie = AutoMapperConfig.MapperInstance.Map<CinemaMovie>(model);

         
           return this.cinemaMovieRepository.UpdateAsync(cinemaMovie);
        }
    }
}
