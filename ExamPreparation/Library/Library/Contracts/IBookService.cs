using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();
        Task<IEnumerable<MineBookViewModel>> GetMineBooksAsync(string userId);

        Task AddBookToCollectionAsync(string userId, BookViewModel bookModel);
        Task<BookViewModel?> GetBookByIdAsync(int id);
    }
}
