using ForumApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Infrastructure.Data.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(initialPosts);
        }

        private Post[] initialPosts = new Post[]
        {
            new Post()
            {
                Id = 1,
                Title = "My first post",
                Content = "My first post will be about performing CRUD operations in MVC"
            },

            new Post
            {
                Id = 2,
                Title = "My second post",
                Content = "My secon post will be about performing CRUD operations in MVC"
            },
            new Post
            {
                Id = 3,
                Title = "My third post",
                Content = "My third post will be about performing CRUD operations in MVC"
            }
        };
    }
}
