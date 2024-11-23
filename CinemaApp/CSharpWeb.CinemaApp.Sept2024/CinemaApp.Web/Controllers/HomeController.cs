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

        public IActionResult Error(int? statusCode = null)
        {
            // TODO: Add other pages
            if (!statusCode.HasValue)
            {
                return this.View();
            }

            if (statusCode == 404)
            {
                return this.View("Error404");
            }
            else if (statusCode == 401 || statusCode == 403)
            {
                return this.View("Error403");
            }

            return this.View("Error500");
        }
    }
}
