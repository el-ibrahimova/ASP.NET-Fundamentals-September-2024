using AutoMapper;
using CinemaApp.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Web.ViewModels.Movie
{
    using Data.Models;
    using static Common.EntityValidationConstants.Movie;
    using static Common.EntityValidationMessages.Movie;
    public class EditMovieViewModel :IMapFrom<Movie>, IMapTo<Movie>, IHaveCustomMappings
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = TitleRequiredMessage)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = GenreRequiredMessage)]
        [MinLength(GenreMinLength)]
        [MaxLength(GenreMaxLength)]
        public string Genre { get; set; } = null!;

        [Required(ErrorMessage = ReleaseDateRequiredMessage)]
        public string ReleaseDate { get; set; } = null!;

        [Required(ErrorMessage = DurationRequiredMessage)]
        [Range(DurationMinValue, DurationMaxValue)]
        public int Duration { get; set; }

        [Required(ErrorMessage = DirectorRequiredMessage)]
        [MinLength(DirectorNameMinLength)]
        [MaxLength(DirectorNameMaxLength)]
        public string Director { get; set; } = null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [MaxLength(ImageUrlMaxLength)]
        public string? ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, EditMovieViewModel>()
                .ForMember(d => d.ReleaseDate, opt => opt.MapFrom(s => s.ReleaseDate.ToString(ReleaseDateFormat)));

            configuration.CreateMap<EditMovieViewModel, Movie>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.ReleaseDate, opt => opt.Ignore());
        }
    }
}

