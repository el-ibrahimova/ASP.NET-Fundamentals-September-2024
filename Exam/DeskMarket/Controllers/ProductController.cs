using DeskMarket.Data;
using DeskMarket.Data.Models;
using DeskMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using static DeskMarket.Common.EntityConstants;

namespace DeskMarket.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext data;

        public ProductController(ApplicationDbContext context)
        {
            data = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = GetUserId();

           var model = await data.Products
                .AsNoTracking()
                .Where(p => p.IsDeleted == false)
                .Select(p => new IndexViewModel()
                {
                    ImageUrl = p.ImageUrl,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Id = p.Id,
                    IsSeller = userId == p.SellerId,
                    HasBought  = data.ProductsClients.Any(pc => pc.ClientId == userId && pc.ProductId == p.Id)
                })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddProductViewModel();
            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            DateTime addedOn;
            if (!DateTime.TryParseExact(model.AddedOn, EntityDateFormat, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out addedOn))
            {
                ModelState.AddModelError(nameof(model.AddedOn), $"Invalid date format! Format must be {EntityDateFormat}.");

                model.Categories = await GetCategories();
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            string userId = GetUserId();

            Product product = new Product()
            {
                Id = model.Id,
                ProductName = model.ProductName,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                SellerId = userId,
                AddedOn = addedOn,
                CategoryId = model.CategoryId,
                IsDeleted = false,
            };

            await data.Products.AddAsync(product);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await data.Products
                .Where(p => p.IsDeleted == false && p.Id == id)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return BadRequest();
            }

            if (product.SellerId != GetUserId())
            {
                return Unauthorized();
            }

            var editedProduct = new EditProductViewModel()
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                AddedOn = product.AddedOn.ToString(EntityDateFormat),
                SellerId = GetUserId()
            };

            editedProduct.Categories = await GetCategories();

            return View(editedProduct);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            var product = await data.Products
                .Where(p => p.IsDeleted == false && p.Id == model.Id)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return BadRequest();
            }

            if (product.SellerId != GetUserId())
            {
                return Unauthorized();
            }

            DateTime addedOn;
            if (!DateTime.TryParseExact(model.AddedOn, EntityDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out addedOn))
            {
                ModelState.AddModelError(nameof(model.AddedOn), $"Invalid date! Format must be: {EntityDateFormat}");

                model.Categories = await GetCategories();
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            product.ProductName = model.ProductName;
            product.Price = model.Price;
            product.Description = model.Description;
            product.ImageUrl = model.ImageUrl;
            product.AddedOn = addedOn;
            product.SellerId = model.SellerId;

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { model.Id });

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await data
                .Products
                .Include(p => p.ProductsClients)
                .Include(product => product.Seller)
                .Include(product => product.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return BadRequest();
            }

            var model = new DetailsViewModel()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                CategoryName = product.Category.Name,
                AddedOn = product.AddedOn
                    .ToString(EntityDateFormat),
                Seller = product.Seller.UserName!,
                HasBought = product.ProductsClients
                    .Any(pc => pc.ClientId == GetUserId())
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var model = await data.ProductsClients
                .AsNoTracking()
                .Where(pc => pc.ClientId == GetUserId())
                .Select(pc => new CartViewModel()
                {
                    Id = pc.Product.Id,
                    ImageUrl = pc.Product.ImageUrl,
                    ProductName = pc.Product.ProductName,
                    Price = pc.Product.Price
                }).ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await data.Products
                .Where(p => p.IsDeleted == false && p.Id == id)
                .Include(pc => pc.ProductsClients)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!data.ProductsClients.Any(pc => pc.ClientId == userId && pc.ProductId == id))
            {
                product.ProductsClients.Add(new ProductClient()
                {
                    ClientId = userId,
                    ProductId = product.Id
                });

                await data.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var product = await data.Products
                .Where(p => p.IsDeleted == false && p.Id == id)
                .Include(pc => pc.ProductsClients)
                .FirstOrDefaultAsync();

            var client = await data.ProductsClients
                .Where(c => c.ClientId == GetUserId())
                .FirstOrDefaultAsync();

            if (product == null || client == null)
            {
                return BadRequest();
            }

            data.ProductsClients.Remove(client);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await data.Products
                .AsNoTracking()
                .Where(p => p.IsDeleted == false && p.Id == id)
                .Select(p => new DeleteViewModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    AddedOn = p.AddedOn.ToString(EntityDateFormat),
                    SellerId = p.SellerId,
                    Seller = p.Seller.UserName
                }).FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            var product = await data.Products
                .Where(p => p.IsDeleted==false && p.Id == model.Id)
                .FirstOrDefaultAsync();

            if (product != null)
            {
                product.IsDeleted = true;
                await data.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return await data.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? String.Empty;
        }
    }
}
