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

        public async Task<AddBookViewModel> GetNewAddBookModelAsync()
        {
            var categories = await data.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            var model = new AddBookViewModel()
            {
                Categories = categories
            };

            return model;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            Book book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                ImageUrl = model.Url,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Rating = decimal.Parse(model.Rating)
            };

            await data.Books.AddAsync(book);
            await data.SaveChangesAsync();
        }

        public async Task<AddBookViewModel?> GetBookByIdForEditAsync(int id)
        {
            var categories = await data.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return await data.Books
                .Where(b => b.Id == id)
                .AsNoTracking()
                .Select(b => new AddBookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Url = b.ImageUrl,
                    Description = b.Description,
                    Rating = b.Rating.ToString(),
                    CategoryId = b.CategoryId,
                    Categories = categories
                })
                .FirstOrDefaultAsync();
        }

        public async Task EditBookAsync(AddBookViewModel model, int id)
        {
           var book = await data.Books.FindAsync(id);

           if (book != null)
           {
               book.Title = model.Title;
               book.Author = model.Author;
               book.ImageUrl = model.Url;
               book.Description = model.Description;
               book.CategoryId = model.CategoryId;
               book.Rating = decimal.Parse(model.Rating);

               await data.SaveChangesAsync();
           }
        }
    }
}


