﻿using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CinemaApp.Common.EntityValidationConstants.Cinema;

namespace CinemaApp.Data.Configuration
{
    public class CinemaConfiguration:IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder.Property(c => c.Location)
                .IsRequired()
                .HasMaxLength(LocationMaxLength);

            builder.Property(c => c.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasData(this.GenerateCinemas());
        }

        private IEnumerable<Cinema> GenerateCinemas()
        {
            IEnumerable<Cinema> cinemas = new List<Cinema>()
            {
                new Cinema()
                {
                    Name = "Cinema city",
                    Location = "Sofia"
                },
                new Cinema()
                {
                    Name = "Cinema city",
                    Location = "Plovdiv"
                },
                new Cinema()
                {
                    Name = "Cinemax",
                    Location = "Varna"
                }
            };

            return cinemas;
        }
    }
}
