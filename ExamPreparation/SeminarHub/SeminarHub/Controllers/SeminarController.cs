using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models;

namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext data;

        public SeminarController(SeminarHubDbContext context)
        {
            data = context;
        }


        [HttpGet]
        public async Task< IActionResult> Add()
        {
            var model = new AddSeminarViewModel();

            model.Categories = await GetCategories();
            
            return View(model);
        }

        private async Task<List<Category>> GetCategories()
        {
            return await data.Categories.ToListAsync();
        }
    }
}
