using CinemaApp.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Data
{
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using Interfaces;
    using Web.ViewModels.Cinema;

    public class CinemaService : ICinemaService
    {
        private readonly IRepository<Cinema, Guid> cinemaRepository;

        public CinemaService(IRepository<Cinema, Guid> cinemaRepository)
        {
            this.cinemaRepository = cinemaRepository;
        }

        public async Task<IEnumerable<CinemaIndexViewModel>> IndexGetAllOrderedByLocationAsync()
        {
            var cinemas = await this.cinemaRepository
                .GetAllAttached()
                .OrderBy(c => c.Location)
                .To<CinemaIndexViewModel>()
                .ToArrayAsync();

            return cinemas;
        }

        public Task AddCinemaAsync(AddCinemaFormModel model)
        {
            throw new NotImplementedException();
        }

        public Task<CinemaDetailsViewModel> GetCinemaDetailsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
