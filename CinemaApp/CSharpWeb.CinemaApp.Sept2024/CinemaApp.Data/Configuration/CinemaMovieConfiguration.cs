using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
    public class CinemaMovieConfiguration:IEntityTypeConfiguration<CinemaMovie>
    {
        public void Configure(EntityTypeBuilder<CinemaMovie> builder)
        {
            builder.HasKey(cm => new { cm.CinemaId, cm.MovieId });

            builder.HasOne(cm => cm.Movie)
                .WithMany(m => m.MovieCinemas)
                .HasForeignKey(cm => cm.MovieId);

            builder.HasOne(cm => cm.Cinema)
                .WithMany(cm => cm.MovieCinemas)
                .HasForeignKey(cm => cm.CinemaId);
        }
    }
}
