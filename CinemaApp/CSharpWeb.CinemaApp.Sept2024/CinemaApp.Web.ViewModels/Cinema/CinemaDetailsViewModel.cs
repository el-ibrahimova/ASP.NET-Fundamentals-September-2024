using CinemaApp.Web.ViewModels.Movie;

namespace CinemaApp.Web.ViewModels.Cinema
{
    public class CinemaDetailsViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;

        public IEnumerable<CinemaMovieViewModel> Movies = new HashSet<CinemaMovieViewModel>();
    }
}
