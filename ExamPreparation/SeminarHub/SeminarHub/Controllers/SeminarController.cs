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
                CategoryId = model.CategoryId,
                OrganizerId = GetUserId()
            };

            await data.Seminars.AddAsync(seminar);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var model = await data.SeminarsParticipants
                .Where(s => s.ParticipantId == userId)
                .AsNoTracking()
                .Select(s => new JoinedSeminarViewModel()
                {
                    Id = s.Seminar.Id,
                    Topic = s.Seminar.Topic,
                    Lecturer = s.Seminar.Lecturer,
                    Organizer = userId,
                    DateAndTime = s.Seminar.DateAndTime.ToString(DateFormat)
                })
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var seminar = await data.Seminars
                .Where(s => s.Id == id)
                .Include(sp => sp.SeminarsParticipants)
                .FirstOrDefaultAsync();

            if (seminar == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!seminar.SeminarsParticipants.Any(p => p.ParticipantId == userId))
            {
                seminar.SeminarsParticipants.Add(new SeminarParticipant()
                {
                    SeminarId = seminar.Id,
                    ParticipantId = userId
                });

                await data.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Joined));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var seminar = await data.Seminars
                .Where(s => s.Id == id)
                .Include(sp => sp.SeminarsParticipants)
                .FirstOrDefaultAsync();

            string userId = GetUserId();

            var participant = await data.SeminarsParticipants
                 .Where(sp => sp.ParticipantId == userId)
                 .FirstOrDefaultAsync();

            if (seminar == null || participant == null)
            {
                return BadRequest();
            }

            data.SeminarsParticipants.Remove(participant);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var seminar = await data.Seminars
                .FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            // we are not the creator of the seminar
            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            var editedModel = new AddSeminarViewModel()
            {
                Topic = seminar.Topic,
                Lecturer = seminar.Lecturer,
                Details = seminar.Details,
                DateAndTime = seminar.DateAndTime.ToString(DateFormat),
                Duration = seminar.Duration,
                CategoryId = seminar.CategoryId
            };

            editedModel.Categories = await GetCategories();

            return View(editedModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddSeminarViewModel model, int id)
        {
            var seminarToEdit = await data.Seminars
                .FindAsync(id);

            if (seminarToEdit == null)
            {
                return BadRequest();
            }

            // we are not the creator of the seminar
            if (seminarToEdit.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            DateTime start;

            if (!DateTime.TryParseExact(model.DateAndTime, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
            {
                ModelState.AddModelError(nameof(model.DateAndTime), $"Invalid date! Format must be: {DateFormat}");

                model.Categories = await GetCategories();
                return View(model);
            }

            if (ModelState.IsValid == false)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            seminarToEdit.Topic = model.Topic;
            seminarToEdit.Lecturer = model.Lecturer;
            seminarToEdit.Details = model.Details;
            seminarToEdit.Duration = model.Duration;
            seminarToEdit.DateAndTime = start;
            seminarToEdit.CategoryId = model.CategoryId;

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await data.Seminars
                             .Where(s => s.Id == id)
                             .AsNoTracking() 
                            .Select(s => new DetailsViewModel()
                            {
                                Id=s.Id,
                                Lecturer = s.Lecturer,
                                Duration = s.Duration,
                                DateAndTime = s.DateAndTime.ToString(DateFormat),
                                Category = s.Category.Name,
                                Details = s.Details,
                                Organizer = s.Organizer.UserName??String.Empty
                            })
                            .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await data.Seminars
                .Where(s => s.Id == id)
                .AsNoTracking()
                .Select(s => new DeleteViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    DateAndTime = s.DateAndTime.ToString(DateFormat)
                })
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(DeleteViewModel model)
        {
            var seminar = await data.Seminars
                .Where(s => s.Id == model.Id)
                .FirstOrDefaultAsync();

            if (seminar != null)
            {
                data.Seminars.Remove(seminar);
                await data.SaveChangesAsync();
            }

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
