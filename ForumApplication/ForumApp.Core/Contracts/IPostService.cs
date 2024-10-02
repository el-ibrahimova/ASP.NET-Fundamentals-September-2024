using ForumApp.Core.Models;

namespace ForumApp.Core.Contracts
{
    public interface IPostService
    {
        Task<IEnumerable<PostModel>> GetAllPostsAsync();
        Task AddAsync(PostModel model);
        Task<PostModel?> getByIdAsync(int id);
        Task EditAsync(PostModel model);
        Task DeleteAsync(int id);
    }
}
