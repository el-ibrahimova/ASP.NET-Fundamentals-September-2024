using Homies.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Homies.Data
{
    public class HomiesDbContext : IdentityDbContext
    {
        public HomiesDbContext(DbContextOptions<HomiesDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventParticipant>()
                .HasKey(ep => new { ep.EventId, ep.HelperId });

            modelBuilder
                .Entity<Homies.Data.Models.Type>()
                .HasData(new Homies.Data.Models.Type()
                {
                    Id = 1,
                    Name = "Animals"
                },
                new Homies.Data.Models.Type()
                {
                    Id = 2,
                    Name = "Fun"
                },
                new Homies.Data.Models.Type()
                {
                    Id = 3,
                    Name = "Discussion"
                },
                new Homies.Data.Models.Type()
                {
                    Id = 4,
                    Name = "Work"
                });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Homies.Data.Models.Type> Types { get; set; }

        public DbSet<EventParticipant> EventsParticipants { get; set; } 

    }
}