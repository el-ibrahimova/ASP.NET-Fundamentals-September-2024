using CinemaApp.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Data
{
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using Interfaces;
    using CinemaApp.Web.ViewModels.Movie;

    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie, Guid> movieRepository;

        public MovieService(IRepository<Movie, Guid> movieRepository)
        {
            this.movieRepository = movieRepository;
        }
        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync()
        {
            return await this.movieRepository
                .GetAllAttached()
                .To<AllMoviesIndexViewModel>()
                .ToArrayAsync();
        }
    }
}
