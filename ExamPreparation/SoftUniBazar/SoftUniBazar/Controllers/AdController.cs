using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Data;

namespace SoftUniBazar.Controllers
{
    public class AdController : Controller
    {
        private readonly BazarDbContext data;

        public AdController(BazarDbContext context)
        {
            data = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return View();
        }
    }
}
