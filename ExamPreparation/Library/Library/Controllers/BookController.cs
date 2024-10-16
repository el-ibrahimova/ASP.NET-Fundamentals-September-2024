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
        public async Task<IActionResult> Mine(int id)
        {
            var mineBooks = await bookService.GetMineBooksAsync();

            return View(mineBooks);
        }


    }
}
