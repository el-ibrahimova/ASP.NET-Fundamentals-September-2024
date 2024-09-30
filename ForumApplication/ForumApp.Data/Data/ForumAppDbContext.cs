using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ForumApp.Infrastructure.Data.Configuration;
using ForumApp.Infrastructure.Data.Models;

namespace ForumApp.Infrastructure.Data
{
    public class ForumAppDbContext : DbContext
    {
        public ForumAppDbContext()
        {

        }

        public ForumAppDbContext(DbContextOptions<ForumAppDbContext> options)
        : base(options)
        {

        }

        public virtual DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
