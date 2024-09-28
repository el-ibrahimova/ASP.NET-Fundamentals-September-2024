using System.Reflection;
using Azure.Identity;
using CinemaApp.Data.Models;

namespace CinemaApp.Data
{
    using Microsoft.EntityFrameworkCore;
    public class CinemaDbContext : DbContext
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // apply all configuration from entity configuration using reflection
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
