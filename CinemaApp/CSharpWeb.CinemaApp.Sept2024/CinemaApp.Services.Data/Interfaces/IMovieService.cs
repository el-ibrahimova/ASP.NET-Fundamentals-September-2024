using CinemaApp.Web.ViewModels.CinemaMovie;
using CinemaApp.Web.ViewModels.Movie;

namespace CinemaApp.Services.Data.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync();

        Task <bool> AddMovieAsync(AddMovieInputModel inputModel);

        Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(Guid id);

        Task<AddMovieToCinemaInputModel?> GetAddMovieToCinemaInputModelByIdAsync(Guid id);

        Task <bool>AddMovieToCinemasAsync(Guid movieId, AddMovieToCinemaInputModel model);

        Task<EditMovieViewModel?> GetEditMovieViewModelByIdAsync(Guid id);

        Task<bool> EditMovieAsync(EditMovieViewModel formModel);
        Task<AvailableTicketsViewModel?> GetAvailableTicketsByIdAsync(Guid cinemaId, Guid movieId);

        Task<IEnumerable<string>> GetAllGenresAsync();
    }
}
