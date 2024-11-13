
using CinemaApp.Web.ViewModels.Cinema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

    public class MovieService :BaseService, IMovieService
    {
        private readonly IRepository<Movie, Guid> movieRepository;
        private readonly IRepository<Cinema, Guid> cinemaRepository;
        private readonly IRepository<CinemaMovie, object> cinemaMovieRepository;

        public MovieService(IRepository<Movie, Guid> movieRepository, IRepository<Cinema, Guid> cinemaRepository,
            IRepository<CinemaMovie, object> cinemaMovieRepository)
        {
            this.movieRepository = movieRepository;
            this.cinemaRepository = cinemaRepository;
            this.cinemaMovieRepository = cinemaMovieRepository;
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

        public async Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(Guid id)
        {
            //Movie? movie = await this.dbContext
            //    .Movies
            //    .FirstOrDefaultAsync(m => m.Id == movieGuid);

            Movie? movie = await this.movieRepository
                .GetByIdAsync(id);

            MovieDetailsViewModel? viewModel = new ();

            if (movie != null)
            {
                AutoMapperConfig.MapperInstance.Map(movie, viewModel);
            }

            return viewModel;
        }

        public async Task<AddMovieToCinemaInputModel?> GetAddMovieToCinemaInputModelByIdAsync(Guid id)
        {
            //Movie? movie = await this.dbContext
            //    .Movies
            //    .FirstOrDefaultAsync(m => m.Id == movieGuid);

            //if (movie == null)
            //{
            //    return this.RedirectToAction(nameof(Index));
            //}

            //AddMovieToCinemaInputModel viewModel = new AddMovieToCinemaInputModel()
            //{
            //    Id = id!,
            //    MovieTitle = movie.Title,
            //    Cinemas = await this.dbContext
            //        .Cinemas
            //        .Include(c => c.MovieCinemas)
            //        .ThenInclude(cm => cm.Movie)
            //        .Select(c => new CinemaCheckBoxItemInputModel()
            //        {
            //            Id = c.Id.ToString(),
            //            Name = c.Name,
            //            Location = c.Location,
            //            IsSelected = c.MovieCinemas
            //                .Any(cm => cm.Movie.Id == movieGuid
            //                           && cm.IsDeleted == false),
            //        })
            //        .ToArrayAsync()
            //};

            Movie? movie = await this.movieRepository
                .GetByIdAsync(id);

            AddMovieToCinemaInputModel? viewModel = null!;

            if (movie != null)
            {
               viewModel = new AddMovieToCinemaInputModel()
                {
                    Id = id.ToString(),
                    MovieTitle = movie.Title,
                    Cinemas = await this.cinemaRepository
                        .GetAllAttached()
                        .Include(c => c.MovieCinemas)
                        .ThenInclude(cm => cm.Movie)
                        .Where(c=>c.IsDeleted==false)
                        .Select(c => new CinemaCheckBoxItemInputModel()
                        {
                            Id = c.Id.ToString(),
                            Name = c.Name,
                            Location = c.Location,
                            IsSelected = c.MovieCinemas
                                .Any(cm => cm.Movie.Id == id
                                           && cm.IsDeleted == false),
                        })
                        .ToArrayAsync()
                };
            }

            return viewModel;
        }

        public async Task<bool> AddMovieToCinemasAsync(Guid movieId, AddMovieToCinemaInputModel model)
        {
            //Movie? movie = await this.dbContext
            //    .Movies
            //    .FirstOrDefaultAsync(m => m.Id == movieGuid);

            //if (movie == null)
            //{
            //    return this.RedirectToAction(nameof(Index));
            //}

            //ICollection<CinemaMovie> entitiesToAdd = new List<CinemaMovie>();

            //foreach (CinemaCheckBoxItemInputModel cinemaInputModel in model.Cinemas)
            //{
            //    Guid cinemaGuid = Guid.Empty;
            //    bool isCinemaGuidValid = this.IsGuidValid(cinemaInputModel.Id, ref cinemaGuid);

            //    if (!isCinemaGuidValid)
            //    {
            //        this.ModelState.AddModelError(String.Empty, "Invalid cinema selected!");
            //        return this.View(model);
            //    }

            //    Cinema? cinema = await this.dbContext
            //        .Cinemas
            //        .FirstOrDefaultAsync(c => c.Id == cinemaGuid);

            //    if (cinema == null)
            //    {
            //        this.ModelState.AddModelError(String.Empty, "Invalid cinema selected!");
            //        return this.View(model);
            //    }

            //    CinemaMovie? cinemaMovie = await this.dbContext
            //        .CinemaMovies
            //        .FirstOrDefaultAsync(cm => cm.MovieId == movieGuid && cm.CinemaId == cinemaGuid);

            //    if (cinemaInputModel.IsSelected)
            //    {
            //        if (cinemaMovie == null)
            //        {
            //            entitiesToAdd.Add(new CinemaMovie()
            //            {
            //                Cinema = cinema,
            //                Movie = movie
            //            });
            //        }
            //        else
            //        {
            //            cinemaMovie.IsDeleted = false;
            //        }
            //    }
            //    else
            //    {
            //        if (cinemaMovie != null)
            //        {
            //            cinemaMovie.IsDeleted = true;
            //        }
            //    }

            //    await this.dbContext.SaveChangesAsync();
            //}

            //await this.dbContext.CinemaMovies.AddRangeAsync(entitiesToAdd);
            //await this.dbContext.SaveChangesAsync();

            Movie? movie = await this.movieRepository
                .GetByIdAsync(movieId);

            if (movie == null)
            {
                return false;
            }

            ICollection<CinemaMovie> entitiesToAdd = new List<CinemaMovie>();

            foreach (CinemaCheckBoxItemInputModel cinemaInputModel in model.Cinemas)
            {
                Guid cinemaGuid = Guid.Empty;
                bool isCinemaGuidValid = this.IsGuidValid(cinemaInputModel.Id, ref cinemaGuid);

                if (!isCinemaGuidValid)
                {
                    return false;
                }

                Cinema? cinema = await this.cinemaRepository
                    .GetByIdAsync(cinemaGuid);

                if (cinema == null || cinema.IsDeleted)
                {
                    return false;
                }

                CinemaMovie? cinemaMovie = await this.cinemaMovieRepository
                    .FirstOrDefaultAsync(cm=>cm.MovieId== movieId
                && cm.CinemaId==cinemaGuid );


                if (cinemaInputModel.IsSelected)
                {
                    if (cinemaMovie == null)
                    {
                        entitiesToAdd.Add(new CinemaMovie()
                        {
                            Cinema = cinema,
                            Movie = movie
                        });
                    }
                    else
                    {
                        cinemaMovie.IsDeleted = false;
                    }
                }
                else
                {
                    if (cinemaMovie != null)
                    {
                        cinemaMovie.IsDeleted = true;
                    }
                }
            }

        await this.cinemaMovieRepository.AddRangeAsync(entitiesToAdd.ToArray());

            return true;
        }
    }
}
