using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CinemaApp.Common.EntityValidationConstants.Movie;
using static CinemaApp.Common.ApplicationConstants;

namespace CinemaApp.Data.Configuration
{
    public class MovieConfiguration:IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            // Fluent API for Movie
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            builder.Property(m => m.Genre)
                .IsRequired()
                .HasMaxLength(GenreMaxLength);

            builder.Property(m => m.Director)
                .IsRequired()
                .HasMaxLength(DirectorNameMaxLength);

            builder.Property(m=>m.ImageUrl)
                .IsRequired(false)
                .HasMaxLength(ImageUrlMaxLength)
                .HasDefaultValue(NoImageUrl);


            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder.HasData(this.SeedMovies());
        }

        private List<Movie> SeedMovies()
        {
            List<Movie> movies = new List<Movie>()
            {
                new Movie()
                {
                    Title = "Harry Potter and the Goblet of Fire",
                    Genre = "Fantasy",
                    ReleaseDate = new DateTime(2005, 11, 01),
                    Director = "Mike Newel",
                    Duration = 157,
                    Description = "It follows Harry Potter, a wizard in his fourth year at Hogwarts School of Witchcraft and Wizardry, and the mystery surrounding the entry of Harry's name into the Triwizard Tournament, in which he is forced to compete."
                },

                new Movie()
                {
                    Title = "Lord of the Rings",
                    Genre = "Fantasy",
                    ReleaseDate = new DateTime(2001, 05,01),
                    Director = "Peter Jackson",
                    Duration = 178,
                    Description = "The plot of The Lord of the Rings is about the war of the peoples of the fantasy world Middle-earth against a dark lord known as Sauron."
                }

            };

            return movies;
        }
    }
}
