namespace CinemaApp.Web.ViewModels.Movie
{
    public class AllMoviesSearchFilterViewModel
    {
        public IEnumerable<AllMoviesIndexViewModel>? Movies { get; set; }

        public string? SearchQuery { get; set; }
        public string? GenreFilter { get; set; }

        public IEnumerable<string>? AllGenres { get; set; }
        public string? YearFilter { get; set; }
    }
}
