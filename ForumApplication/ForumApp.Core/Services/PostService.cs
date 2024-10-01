using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using ForumApp.Infrastructure.Data;
using ForumApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ForumApp.Core.Services
{
    public class PostService : IPostService
    {
        private readonly ForumAppDbContext context;

        private readonly ILogger logger;

        public PostService(
            ForumAppDbContext _context,
            ILogger<PostService> _logger)
        {
            context = _context;
            logger = _logger;
        }

        public async Task<IEnumerable<PostModel>> GetAllPostsAsync()
        {
            return await context.Posts
                .Select(p => new PostModel()
                {
                    Id = p.Id,
                    Content = p.Content,
                    Title = p.Title
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(PostModel model)
        {
            var entity = new Post()
            {
                Title = model.Title,
                Content = model.Content
            };

            try
            {
                await context.AddAsync(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "PostService.AddAsync");
                throw new ApplicationException("Operation failed. Please, try again.");
            }

        }
    }
}
