using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using ForumApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Core.Services
{
    public class PostService:IPostService
    {
        private readonly ForumAppDbContext context;

        public PostService(ForumAppDbContext _context
        )
        {
            context = _context;
        }

        public async Task<IEnumerable<PostModel>> GetAllPostsAsync()
        {
            return await context.Posts
                .Select(p=> new PostModel()
                {
                    Id= p.Id,
                    Content = p.Content,
                    Title = p.Title
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
