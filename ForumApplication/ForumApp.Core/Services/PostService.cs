using ForumApp.Core.Contracts;
using ForumApp.Infrastructure.Data;

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
    }
}
