using System.Security.Claims;
using Library.Contracts;
using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var books = await bookService.GetAllBooksAsync();

            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var myBooks = await bookService.GetMineBooksAsync(GetUserId());

            return View(myBooks);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            var userId = GetUserId();

            await bookService.AddBookToCollectionAsync(userId, book);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return RedirectToAction(nameof(Mine));
            }

            var userId = GetUserId();

            await bookService.RemoveBookFromCollectionAsync(userId, book);

            return RedirectToAction(nameof(Mine));
        }
    }
}
