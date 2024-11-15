using CinemaApp.Services.Mapping;

namespace CinemaApp.Web.ViewModels.Ticket
{
    using AutoMapper;
    using Data.Models;

    public class UserTicketViewModel : IMapFrom<Ticket>, IHaveCustomMappings
    {
        public string Id { get; set; } = null!;

        public string MovieTitle { get; set; } = null!;

        public string CinemaName { get; set; } = null!;
        public string CinemaLocation { get; set; } = null!;

        public string Price { get; set; } = null!;

        public DateTime PurchaseDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Ticket, UserTicketViewModel>()
                .ForMember(d => d.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(d => d.CinemaName, opt => opt.MapFrom(src => src.Cinema.Name))
                .ForMember(d => d.CinemaLocation, opt => opt.MapFrom(src => src.Cinema.Location))
                .ForMember(d => d.Price, opt => opt.MapFrom(src => src.Price.ToString("f2")));
        }
    }
}