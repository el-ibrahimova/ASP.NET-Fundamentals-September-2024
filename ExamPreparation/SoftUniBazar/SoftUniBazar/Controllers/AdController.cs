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
            model.Categories = await GetCategories();

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

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string userId = GetUserId();

            var model = await data.AdsBuyers
                .AsNoTracking()
                .Where(b => b.BuyerId == userId)
                .Select(a => new MyCartViewModel()
                {
                    Id = a.Ad.Id,
                    Name = a.Ad.Name,
                    CreatedOn = a.Ad.CreatedOn.ToString(EntityDateFormat),
                    Category = a.Ad.Category.Name,
                    Description = a.Ad.Description,
                    Price = a.Ad.Price,
                    Owner = userId,
                    ImageUrl = a.Ad.ImageUrl
                })
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var ad = await data.Ads
                .Where(a => a.Id == id)
                .Include(ba => ba.AdsBuyers)
                .FirstOrDefaultAsync();

            if (ad == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            bool isAlreadyAdded = data.AdsBuyers.Any(b => b.BuyerId == userId && b.AdId == id);

            if (isAlreadyAdded)
            {
                return RedirectToAction(nameof(All));
            }
            
            ad.AdsBuyers.Add(new AdBuyer()
            {
                BuyerId = userId,
                AdId = id
            });

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ad = await data.Ads.FindAsync(id);

            if (ad == null)
            {
                return BadRequest();
            }

            if (ad.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new AdViewModel()
            {
                CategoryId = ad.CategoryId,
                Description = ad.Description,
                Name = ad.Name,
                Id = ad.Id,
                ImageUrl = ad.ImageUrl,
                Price = ad.Price
            };

            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdViewModel model, int id)
        {
            var ad = await data.Ads.FindAsync(id);

            if (ad == null)
            {
                return BadRequest();
            }

            if (ad.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            ad.CategoryId = model.Id;
            ad.Description = model.Description;
            ad.Name = model.Name;
            ad.ImageUrl = model.ImageUrl;
            ad.Price = model.Price;
            ad.Id = model.Id;

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var ad = await data.Ads
                .Where(a => a.Id == id)
                .Include(b => b.AdsBuyers)
                .FirstOrDefaultAsync();

            var buyer = await data.AdsBuyers
                .Where(b => b.BuyerId == GetUserId())
                .FirstOrDefaultAsync();

            if (ad == null || buyer == null)
            {
                return BadRequest();
            }

            data.AdsBuyers.Remove(buyer);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
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
