using CinemaApp.Web.ViewModels.Watchlist;

namespace CinemaApp.Services.Data.Interfaces
{
    public interface IWatchlistService
    {
        Task<IEnumerable<ApplicationUserWatchlistViewModel>> GetUserWatchlistByUserIdAsync(string userId);

        Task<bool> AddMovieToUserWatchlistAsync(string? movieId, string userId);

        Task<bool> RemoveMovieFromUserWatchlistAsync(string? movieId, string userId);
    }
}
