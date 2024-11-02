namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc; // Third-party namespaces

    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
