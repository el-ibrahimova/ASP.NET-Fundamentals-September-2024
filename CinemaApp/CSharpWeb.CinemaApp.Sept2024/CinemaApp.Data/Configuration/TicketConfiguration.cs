﻿using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
    public class TicketConfiguration:IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);
          
            builder.Property(t => t.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
           
            builder.Property(t => t.CinemaId)
                .IsRequired();
            
            builder.Property(t => t.MovieId)
                .IsRequired();
          
            builder.Property(t => t.UserId)
                .IsRequired();

            builder
                .HasOne(t => t.Cinema)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CinemaId);

            builder
                .HasOne(t => t.Movie)
                .WithMany(m => m.Tickets)
                .HasForeignKey(t => t.MovieId);

            builder
                .HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId);

        }
    }
}
