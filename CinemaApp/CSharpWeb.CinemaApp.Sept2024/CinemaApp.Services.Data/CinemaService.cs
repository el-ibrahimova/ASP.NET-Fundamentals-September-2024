using CinemaApp.Services.Mapping;
using CinemaApp.Web.ViewModels.Movie;
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

        public async Task AddCinemaAsync(AddCinemaFormModel model)
        {
            Cinema cinema = new Cinema();

            AutoMapperConfig.MapperInstance.Map(model, cinema);

            await this.cinemaRepository.AddAsync(cinema);
        }

        public async Task<CinemaDetailsViewModel?> GetCinemaDetailsByIdAsync(Guid id)
        {
            Cinema? cinema = await this.cinemaRepository
                .GetAllAttached()
                .Include(c => c.MovieCinemas)
                .ThenInclude(cm => cm.Movie)
                .FirstOrDefaultAsync(c => c.Id == id);

            CinemaDetailsViewModel? viewModel = null;

            // invalid (non-existing) GUID in the URL
            if (cinema != null)
            {
                viewModel = new CinemaDetailsViewModel()
                {
                    Name = cinema.Name,
                    Location = cinema.Location,
                    Movies = cinema
                        .MovieCinemas
                        .Where(cm => cm.IsDeleted == false)
                        .Select(cm => new CinemaMovieViewModel()
                        {
                            Title = cm.Movie.Title,
                            Duration = cm.Movie.Duration
                        })
                        .ToArray()
                };
            }

            return viewModel;
        }
    }
}


