using AutoMapper;
using CinemaApp.Services.Mapping;

namespace CinemaApp.Web.ViewModels.Movie
{
    using Data.Models;
    public class AllMoviesIndexViewModel:IMapFrom<Movie>, IHaveCustomMappings
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;

        public string ReleaseDate { get; set; } = null!;

        public string Duration { get; set; } = null!;
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, AllMoviesIndexViewModel>()
                .ForMember(d => d.ReleaseDate,
                    x => x.MapFrom(s => s.ReleaseDate.ToString("MMMM yyyy")));

        }
    }
}
