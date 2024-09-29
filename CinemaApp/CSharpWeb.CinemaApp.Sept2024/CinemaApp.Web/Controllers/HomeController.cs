namespace CinemaApp.Web.Controllers
{
    using System.Diagnostics; // System namespaces

    using Microsoft.AspNetCore.Mvc; // Third-party namespaces

    using ViewModels; // Internal project namespaces

    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public async Task<IActionResult> Index()
        {
            // Two ways of transmitting data from Controller to View

            // 1. Using ViewData/ViewBag
            // 2. Pass ViewModel to the View

            ViewData["Title"] = "Home Page"; // this is dictionary
            ViewData["Message"] = "Welcome to the Cinema Web App!"; // this is dictionary

            return View();
        }
    }
}
