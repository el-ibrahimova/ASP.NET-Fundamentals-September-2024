using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Models;
using static SoftUniBazar.Common.EntityConstants;

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
            var model = await data.Ads
                .Select(a => new AllAdViewModel()
                {
                    Category = a.Category.Name,
                    CreatedOn = a.CreatedOn.ToString(EntityDateFormat),
                    Description = a.Description,
                    Id = a.Id,
                    ImageUrl = a.ImageUrl,
                    Name = a.Name,
                    Owner = a.Owner.UserName,
                    Price = a.Price
                })
                .ToListAsync();

            return View(model);
        }

        private async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return await data.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
