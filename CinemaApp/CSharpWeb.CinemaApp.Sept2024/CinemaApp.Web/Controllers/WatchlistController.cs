using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class WatchlistController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
