using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CinemaApp.Services.Mapping;

namespace CinemaApp.Web.ViewModels.Cinema
{
    using static Common.EntityValidationConstants.Cinema;
    using Data.Models;
    public class EditCinemaFormModel:IHaveCustomMappings

    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(LocationMinLength)]
        [MaxLength(LocationMaxLength)]
        public string Location { get; set; } = null!;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Cinema, EditCinemaFormModel>();

            configuration.CreateMap<EditCinemaFormModel, Cinema>()
                .ForMember(d => d.Id, x => x.MapFrom(s => Guid.Parse(s.Id)));
        }
    }
}
