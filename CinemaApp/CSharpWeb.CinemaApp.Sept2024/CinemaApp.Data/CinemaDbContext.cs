using System.Reflection;
using Azure.Identity;
using CinemaApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CinemaApp.Data
{
    using Microsoft.EntityFrameworkCore;
    public class CinemaDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public CinemaDbContext()
        {
        }

        public CinemaDbContext(DbContextOptions options)
        : base(options)
        {
            
        }

        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<Cinema> Cinemas { get; set; } = null!;

        public virtual DbSet<CinemaMovie> CinemaMovies { get; set; } = null!;

        public virtual DbSet<ApplicationUserMovie> UsersMovies { get; set; } = null!;   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // apply all configuration from entity configuration using reflection
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
