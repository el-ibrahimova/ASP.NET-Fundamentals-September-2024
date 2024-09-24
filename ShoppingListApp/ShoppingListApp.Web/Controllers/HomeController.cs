using Microsoft.AspNetCore.Mvc;
using ShoppingListApp.Web.Models;
using System.Diagnostics;

namespace ShoppingListApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Message = "Hello World!";
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "This is an ASP.NET Core MVC app!";
            return View();
        }

        public IActionResult Numbers()
        {
          return View();
        }

        public IActionResult NumbersToN(int count =3)
        {
            ViewBag.Count = count;
            return View();
        }

    }
}
