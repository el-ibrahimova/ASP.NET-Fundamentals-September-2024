using System.Security.Claims;
using Library.Contracts;
using Library.Data;
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

    }
}


