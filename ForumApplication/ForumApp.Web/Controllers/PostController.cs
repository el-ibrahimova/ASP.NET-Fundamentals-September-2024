using ForumApp.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService _postService)
        {
            postService = _postService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
