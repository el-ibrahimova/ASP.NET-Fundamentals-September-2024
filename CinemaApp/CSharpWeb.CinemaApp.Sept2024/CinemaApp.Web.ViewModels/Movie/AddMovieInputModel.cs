using AutoMapper;
using CinemaApp.Services.Mapping;

namespace CinemaApp.Web.ViewModels.Movie
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Movie;
    using static Common.EntityValidationMessages.Movie;
    using Data.Models;
    public class AddMovieInputModel:IMapTo<Movie>, IHaveCustomMappings
    {
        public AddMovieInputModel()
        {
            // in this way we set default value for ReleaseDate
            this.ReleaseDate = DateTime.UtcNow.ToString(ReleaseDateFormat);
        }

        [Required(ErrorMessage = TitleRequiredMessage)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = GenreRequiredMessage)]
        [MinLength(GenreMinLength)]
        [MaxLength(GenreMaxLength)]
        public string Genre { get; set; } = null!;

        [Required(ErrorMessage = ReleaseDateRequiredMessage)]
        public string ReleaseDate { get; set; }

        [Range(DurationMinValue, DurationMaxValue)]
        [Required(ErrorMessage = DurationRequiredMessage)]
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
            configuration.CreateMap<AddMovieInputModel, Movie>()
                .ForMember(d => d.ReleaseDate, x => x.Ignore());
        }
    }
}
