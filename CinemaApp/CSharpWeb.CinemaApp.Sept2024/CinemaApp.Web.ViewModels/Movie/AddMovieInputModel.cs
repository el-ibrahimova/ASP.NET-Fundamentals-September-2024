﻿namespace CinemaApp.Web.ViewModels.Movie
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Movie;
    using static Common.EntityValidationMessages.Movie;
    public class AddMovieInputModel
    {
        public AddMovieInputModel()
        {
            // in this way we set default value for ReleaseDate
            this.ReleaseDate = DateTime.UtcNow.ToString(ReleaseDateFormat);
        }

        [Required(ErrorMessage = TitleRequiredMessage) ]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = GenreRequiredMessage)]
        [MinLength(GenreMinLength)]
        [MaxLength(GenreMaxLength)]
        public string Genre { get; set; } = null!;

        [Required(ErrorMessage = ReleaseDateMessage)]
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
    }
}
