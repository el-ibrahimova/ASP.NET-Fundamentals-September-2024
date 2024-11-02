using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Interfaces;
using CinemaApp.Services.Data.Interfaces;
using CinemaApp.Web.ViewModels.Watchlist;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Data
{
    using static Common.EntityValidationConstants.Movie;

    public class WatchlistService:BaseService, IWatchlistService
    {
        private readonly IRepository<ApplicationUserMovie, object> userMovieRepository;
        private readonly IRepository<Movie, Guid> movieRepository;

        public WatchlistService(IRepository<ApplicationUserMovie, object> userMovieRepository, IRepository<Movie, Guid> movieRepository)
        {
            this.userMovieRepository = this.userMovieRepository;
            this.movieRepository = movieRepository;
        }

        public async Task<IEnumerable<ApplicationUserWatchlistViewModel>> GetUserWatchlistByUserIdAsync(string userId)
        {
           // var watchlist = await this.dbContext.UsersMovies
            //    .Include(um => um.Movie)
            //    .Where(um => um.ApplicationUserId.ToString().ToLower() == userId.ToLower())
            //    .Select(um => new ApplicationUserWatchlistViewModel()
            //    {
            //        MovieId = um.MovieId.ToString(),
            //        Title = um.Movie.Title,
            //        Genre = um.Movie.Genre,
            //        ReleaseDate = um.Movie.ReleaseDate.ToString(ReleaseDateFormat),
            //        ImageUrl = um.Movie.ImageUrl
            //    })
            //    .ToListAsync();

            var watchlist = await this.userMovieRepository
                .GetAllAttached()
                .Include(um => um.Movie)
                .Where(um => um.ApplicationUserId.ToString().ToLower() == userId.ToLower())
                .Select(um => new ApplicationUserWatchlistViewModel()
                {
                    MovieId = um.MovieId.ToString(),
                    Title = um.Movie.Title,
                    Genre = um.Movie.Genre,
                    ReleaseDate = um.Movie.ReleaseDate.ToString(ReleaseDateFormat),
                    ImageUrl = um.Movie.ImageUrl
                })
                .ToListAsync();

            return watchlist;
        }

        public async Task<bool> AddMovieToUserWatchlistAsync(string? movieId, string userId)
        {
            //Guid movieGuid = Guid.Empty;
            //if (!this.IsGuidValid(movieId, ref movieGuid))
            //{
            //    return this.RedirectToAction("Index", "Movie");
            //}

            //Movie? movie = await this.dbContext.Movies
            //    .FirstOrDefaultAsync(m => m.Id == movieGuid);

            //if (movie == null)
            //{
            //    return this.RedirectToAction("Index", "Movie");
            //}

            //Guid userGuid = Guid.Parse(this.userManager.GetUserId(this.User)!);

            //bool addedToWatchlistAlready = await this.dbContext
            //    .UsersMovies.AnyAsync(um => um.ApplicationUserId == userGuid
            //                                && um.MovieId == movieGuid);

            //if (!addedToWatchlistAlready)
            //{
            //    ApplicationUserMovie newUserMovie = new ApplicationUserMovie()
            //    {
            //        ApplicationUserId = userGuid,
            //        MovieId = movieGuid
            //    };

            //    this.dbContext.UsersMovies.AddAsync(newUserMovie);
            //    this.dbContext.SaveChangesAsync();
            //}


            Guid movieGuid = Guid.Empty;
            if (!this.IsGuidValid(movieId, ref movieGuid))
            {
                return false;
            }

            Movie? movie = await this.movieRepository
                .GetByIdAsync(movieGuid);

            if (movie == null)
            {
                return false;
            }
            
            Guid userGuid = Guid.Parse(userId);

            ApplicationUserMovie? addedToWatchlistAlready =
                await this.userMovieRepository.FirstOrDefaultAsync(um =>
                    um.MovieId == movieGuid && um.ApplicationUserId == userGuid);

            if (addedToWatchlistAlready==null)
            {
                ApplicationUserMovie newUserMovie = new ApplicationUserMovie()
                {
                    ApplicationUserId = userGuid,
                    MovieId = movieGuid
                };

                await this.userMovieRepository.AddAsync(newUserMovie);
            }

            return true;
        }

        public async Task<bool> RemoveMovieFromUserWatchlistAsync(string? movieId, string userId)
        {
            //Guid movieGuid = Guid.Empty;
            //if (!this.IsGuidValid(movieId, ref movieGuid))
            //{
            //    return this.RedirectToAction("Index", "Movie");
            //}

            //Movie? movie = await this.dbContext.Movies
            //    .FirstOrDefaultAsync(m => m.Id == movieGuid);

            //if (movie == null)
            //{
            //    return this.RedirectToAction("Index", "Movie");
            //}

            //Guid userGuid = Guid.Parse(this.userManager.GetUserId(this.User)!);

            //ApplicationUserMovie? applicationUserMovie = await this.dbContext
            //    .UsersMovies.FirstOrDefaultAsync(um => um.ApplicationUserId == userGuid
            //                                           && um.MovieId == movieGuid);

            //if (applicationUserMovie != null)
            //{
            //    this.dbContext.UsersMovies.Remove(applicationUserMovie);
            //    await this.dbContext.SaveChangesAsync();
            //}

            Guid movieGuid = Guid.Empty;
            if (!this.IsGuidValid(movieId, ref movieGuid))
            {
                return false;
            }

            Movie? movie = await this.movieRepository.GetByIdAsync(movieGuid);
            
            if (movie == null)
            {
                return false;
            }

            Guid userGuid = Guid.Parse(userId);

            //TODO: Implement soft deletion


            ApplicationUserMovie? applicationUserMovie =
                await this.userMovieRepository.FirstOrDefaultAsync(um =>
                    um.MovieId == movieGuid && um.ApplicationUserId == userGuid);

            if (applicationUserMovie != null)
            {
                await this.userMovieRepository.DeleteAsync(applicationUserMovie);
            }

            return true;
        }
    }
}
