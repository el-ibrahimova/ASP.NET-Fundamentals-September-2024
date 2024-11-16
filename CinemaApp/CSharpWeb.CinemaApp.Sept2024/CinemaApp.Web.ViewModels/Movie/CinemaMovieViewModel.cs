namespace CinemaApp.Web.ViewModels.Movie
{
    using Services.Mapping;
    using Data.Models;
    public class CinemaMovieViewModel : IMapFrom<Movie>
    {
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int Duration { get; set; }

        public string Description { get; set; } = null!;

    }
}
