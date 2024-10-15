using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookController : BaseController
    {
        [HttpGet]
        public IActionResult All()
        {
            return View();
        }
    }
}
