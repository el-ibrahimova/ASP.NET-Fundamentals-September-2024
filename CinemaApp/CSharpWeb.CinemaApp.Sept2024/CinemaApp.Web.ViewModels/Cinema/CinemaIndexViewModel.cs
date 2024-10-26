namespace CinemaApp.Web.ViewModels.Cinema
{
    using Services.Mapping;
    using Data.Models;

    public class CinemaIndexViewModel:IMapFrom<Cinema>
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
    }
}
