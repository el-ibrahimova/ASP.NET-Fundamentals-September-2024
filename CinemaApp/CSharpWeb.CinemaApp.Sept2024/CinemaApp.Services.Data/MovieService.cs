
namespace CinemaApp.Services.Data
{
    using System.Globalization;
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using Mapping;
    using Web.ViewModels.Movie;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using static Common.EntityValidationConstants.Movie;

    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie, Guid> movieRepository;

        public MovieService(IRepository<Movie, Guid> movieRepository)
        {
            this.movieRepository = movieRepository;
        }
        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync()
        {
            //IEnumerable<Movie> allMovies = await this.dbContext
            //    .Movies
            //    .ToArrayAsync();

            return await this.movieRepository
                .GetAllAttached()
                .To<AllMoviesIndexViewModel>()
                .ToArrayAsync();
        }

        public async Task<bool> AddMovieAsync(AddMovieInputModel inputModel)
        {
            bool isReleaseDateValid = DateTime.TryParseExact(inputModel.ReleaseDate, ReleaseDateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);

            if (!isReleaseDateValid)
            {
                return false;
            }
            
            //  Movie movie = new Movie()
            // {
            //    Title = inputModel.Title,
            //    Genre = inputModel.Genre,
            //    ReleaseDate = releaseDate,
            //    Director = inputModel.Director,
            //    Duration = inputModel.Duration,
            //    Description = inputModel.Description,
            //    ImageUrl = inputModel.ImageUrl
            //};

            //await this.dbContext.Movies.AddAsync(movie);
            //await this.dbContext.SaveChangesAsync();


            Movie movie = new Movie();

            AutoMapperConfig.MapperInstance.Map(inputModel, movie);
            movie.ReleaseDate = releaseDate;

            await this.movieRepository.AddAsync(movie);

            return true;
        }
    }
}
