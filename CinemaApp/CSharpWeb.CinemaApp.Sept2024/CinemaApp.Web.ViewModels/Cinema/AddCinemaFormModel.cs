namespace CinemaApp.Web.ViewModels.Cinema
{
    using System.ComponentModel.DataAnnotations;
    using Services.Mapping;
    using static CinemaApp.Common.EntityValidationConstants.Cinema;
    using Data.Models;

    public class AddCinemaFormModel : IMapTo<Cinema>
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(LocationMinLength)]
        [MaxLength(LocationMaxLength)]
        public string Location { get; set; } = null!;
    }
}
