using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models;
using static SoftUniBazar.Common.EntityConstants;

namespace SoftUniBazar.Controllers
{
    [Authorize]
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

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AdViewModel();
            model.Categories= await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdViewModel model)
        {
          if (ModelState.IsValid == false)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            Ad newAd = new Ad()
            {
                Id = model.Id,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                CategoryId = model.CategoryId,
                Name = model.Name,
                OwnerId = GetUserId(),
                CreatedOn = DateTime.Now
            };

            await data.Ads.AddAsync(newAd);
            await data.SaveChangesAsync();

            return RedirectToAction("All", "Ad");
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

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? String.Empty;
        }
    }
}
