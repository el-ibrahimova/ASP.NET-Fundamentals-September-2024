using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models;
using static SeminarHub.Common.ApplicationConstants;

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
        public async Task<IActionResult> All()
        {
            var model = await data.Seminars
                .AsNoTracking()
                .Select(s => new AllSeminarViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Organizer = s.Organizer.UserName ?? String.Empty,
                    Lecturer = s.Lecturer,
                    Category = s.Category.Name,
                    DateAndTime = s.DateAndTime.ToString(DateFormat)
                })
                .ToListAsync();

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddSeminarViewModel();

            model.Categories = await GetCategories();

            return View(model);
        }

       [HttpPost]
        public async Task<IActionResult> Add(AddSeminarViewModel model)
        {
            DateTime dateAndTime;
            if (DateTime.TryParseExact(model.DateAndTime, DateFormat, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out dateAndTime) == false)
            {
                ModelState.AddModelError(nameof(model.DateAndTime), "Invalid date format");

                model.Categories = await GetCategories();
                return View(model);
            }
          
            if (ModelState.IsValid == false)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            Seminar seminar = new Seminar()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                DateAndTime = dateAndTime,
                Duration = model.Duration,
                CategoryId =model.CategoryId,
                OrganizerId = GetUserId()
            };

            await data.Seminars.AddAsync(seminar);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return await data.Categories
                .AsNoTracking()
                .Select(t => new CategoryViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .ToListAsync();
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
