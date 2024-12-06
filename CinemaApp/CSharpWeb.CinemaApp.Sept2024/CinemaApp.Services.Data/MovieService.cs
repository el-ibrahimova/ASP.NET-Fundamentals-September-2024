﻿
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using CinemaApp.Web.ViewModels.Cinema;
using CinemaApp.Web.ViewModels.CinemaMovie;
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
    using static Common.ApplicationConstants;

    public class MovieService : BaseService, IMovieService
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
        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync(AllMoviesSearchFilterViewModel inputModel)
        {
            IQueryable<Movie> allMoviesQuery = this.movieRepository.GetAllAttached();

            if (!String.IsNullOrWhiteSpace(inputModel.SearchQuery))
            {
                allMoviesQuery = allMoviesQuery.Where(m =>
                    m.Title.ToLower().Contains(inputModel.SearchQuery.ToLower()));
            }

            if (!String.IsNullOrWhiteSpace(inputModel.GenreFilter))
            {
                allMoviesQuery = allMoviesQuery.Where(m =>
                    m.Genre.ToLower() == inputModel.GenreFilter.ToLower());
            }

            if (!String.IsNullOrWhiteSpace(inputModel.YearFilter))
            {
                Match rangeMatch = Regex.Match(inputModel.YearFilter, YearFilterRangeRegex);

                if (rangeMatch.Success)
                {
                    int startYear = int.Parse(rangeMatch.Groups[1].Value);
                    int endYear = int.Parse(rangeMatch.Groups[2].Value);

                    allMoviesQuery = allMoviesQuery
                        .Where(m => m.ReleaseDate.Year >= startYear && m.ReleaseDate.Year <= endYear);
                }
                else
                {
                    bool isValidNumber = int.TryParse(inputModel.YearFilter, out int year);

                    if(isValidNumber)
                    {
                        allMoviesQuery = allMoviesQuery.Where(m => m.ReleaseDate.Year == year);
                    }
                }
            }

            if (inputModel.CurrentPage.HasValue && inputModel.EntitiesPerPage.HasValue)
            {
                allMoviesQuery = allMoviesQuery
                    .Skip(inputModel.EntitiesPerPage.Value * (inputModel.CurrentPage.Value - 1))
                    .Take(inputModel.EntitiesPerPage.Value);
            }

            return await allMoviesQuery
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

           Movie movie = new Movie();

            AutoMapperConfig.MapperInstance.Map(inputModel, movie);
            movie.ReleaseDate = releaseDate;

            await this.movieRepository.AddAsync(movie);

            return true;
        }

        public async Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(Guid id)
        {
            Movie? movie = await this.movieRepository
                .GetByIdAsync(id);

            MovieDetailsViewModel? viewModel = new();

            if (movie != null)
            {
                AutoMapperConfig.MapperInstance.Map(movie, viewModel);
            }

            return viewModel;
        }

        public async Task<AddMovieToCinemaInputModel?> GetAddMovieToCinemaInputModelByIdAsync(Guid id)
        {
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
                         .Where(c => c.IsDeleted == false)
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
                    .FirstOrDefaultAsync(cm => cm.MovieId == movieId
                && cm.CinemaId == cinemaGuid);


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

        public async Task<EditMovieViewModel?> GetEditMovieViewModelByIdAsync(Guid id)
        {
            // TODO: Check soft Delete
            EditMovieViewModel? editMovieFormModel = await this.movieRepository
                .GetAllAttached()
                .To<EditMovieViewModel>()
                .FirstOrDefaultAsync(m => m.Id.ToLower() == id.ToString().ToLower());

            if (editMovieFormModel != null && editMovieFormModel.ImageUrl.Equals(NoImageUrl))
            {
                editMovieFormModel.ImageUrl = "No image";
            }

            return editMovieFormModel;
        }

        public async Task<bool> EditMovieAsync(EditMovieViewModel formModel)
        {
            Guid movieGuid = Guid.Empty;
            if (!this.IsGuidValid(formModel.Id, ref movieGuid))
            {
                return false;
            }

            bool isReleaseDateValid = DateTime.TryParseExact(formModel.ReleaseDate, ReleaseDateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);

            if (!isReleaseDateValid)
            {
                return false;
            }


            Movie editedMovie = AutoMapperConfig.MapperInstance.Map<Movie>(formModel);
            editedMovie.Id = movieGuid;
            editedMovie.ReleaseDate = releaseDate;

            if (formModel.ImageUrl ==null || formModel.ImageUrl.Equals("No image"))
            {
                editedMovie.ImageUrl = NoImageUrl;
            }


            return await this.movieRepository.UpdateAsync(editedMovie);
        }

        public async Task<AvailableTicketsViewModel?> GetAvailableTicketsByIdAsync(Guid cinemaId, Guid movieId)
        {
            CinemaMovie? cinemaMovie = await this.cinemaMovieRepository
                .FirstOrDefaultAsync(cm => cm.MovieId == movieId &&
                                           cm.CinemaId == cinemaId);

            AvailableTicketsViewModel availableTicketsViewModel = null;
            if (cinemaMovie != null)
            {
                availableTicketsViewModel = new AvailableTicketsViewModel()
                {
                    CinemaId = cinemaId.ToString(),
                    MovieId = movieId.ToString(),
                    Quantity = 0,
                    AvailableTickets = cinemaMovie.AvailableTickets
                };
            }

            return availableTicketsViewModel;
        }

        public async Task<IEnumerable<string>> GetAllGenresAsync()
        {
            IEnumerable<string> allGenres =await this.movieRepository
                .GetAllAttached()
                .Select(m => m.Genre)
                .Distinct()
                .ToArrayAsync();

            return allGenres;
        }

        public async Task<int> GetMoviesCountAsync()
        {
            return await this.movieRepository
                .GetAllAttached()
                .CountAsync();
        }
    }
}
