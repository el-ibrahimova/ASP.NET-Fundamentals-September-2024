using System.Security.Claims;
using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext data;

        public BookService(LibraryDbContext dbContext)
        {
            data = dbContext;
        }

        public async Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync()
        {
            return await data.Books
                  .AsNoTracking()
                  .Select(b => new AllBookViewModel()
                  {
                      Id = b.Id,
                      Title = b.Title,
                      Author = b.Author,
                      ImageUrl = b.ImageUrl,
                      Rating = b.Rating,
                      Category = b.Category.Name
                  })
                  .ToListAsync();
        }

        public async Task<IEnumerable<MineBookViewModel>> GetMineBooksAsync(string userId)
        {
            return await data.Books
                    .Where(u => u.UsersBooks.Any(b => b.CollectorId == userId))
                    .AsNoTracking()
                    .Select(b => new MineBookViewModel()
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = b.Author,
                        ImageUrl = b.ImageUrl,
                        Description = b.Description,
                        Category = b.Category.Name
                    })
                    .ToListAsync();
        }

        public async Task<BookViewModel?> GetBookByIdAsync(int id)
        {
            return await data.Books
                .Where(b => b.Id == id)
                .AsNoTracking()
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Description = b.Description,
                    Rating = b.Rating,
                    CategoryId = b.CategoryId
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddBookToCollectionAsync(string userId, BookViewModel bookModel)
        {
            bool alreadyAdded = await data.UserBooks
                .AnyAsync(ub => ub.CollectorId == userId
                                && ub.BookId == bookModel.Id);

            if (alreadyAdded == false)
            {
                var userBook = new IdentityUserBook()
                {
                    CollectorId = userId,
                    BookId = bookModel.Id
                };

                await data.UserBooks.AddAsync(userBook);
                await data.SaveChangesAsync();
            }
        }

        public async Task RemoveBookFromCollectionAsync(string userId, BookViewModel bookModel)
        {
            var userBook = await data.UserBooks
                .FirstOrDefaultAsync(ub => ub.CollectorId == userId
                                           && ub.BookId == bookModel.Id);

            if (userBook != null)
            {
                data.UserBooks.Remove(userBook);
                await data.SaveChangesAsync();
            }
        }
    }
}


