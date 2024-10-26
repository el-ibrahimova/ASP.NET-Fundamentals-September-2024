using CinemaApp.Services.Data.Interfaces;
using CinemaApp.Web.ViewModels.Movie;

namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using ViewModels.Cinema;

    public class CinemaController : BaseController
    {
        private readonly CinemaDbContext dbContext;

        private readonly ICinemaService cinemaService;

        public CinemaController(CinemaDbContext dbContext, ICinemaService cinemaService)
        {
            this.dbContext = dbContext;
            this.cinemaService = cinemaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // the next logic is moved to CinemaService

            //var cinemas = await this.dbContext.Cinemas
            //     .Select(c => new CinemaIndexViewModel()
            //     {
            //         Id = c.Id.ToString(),
            //         Name = c.Name,
            //         Location = c.Location
            //     })
            //     .OrderBy(c => c.Location)
            //     .ToArrayAsync();

            var cinemas = await this.cinemaService
                .IndexGetAllOrderedByLocationAsync();

            return this.View(cinemas);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCinemaFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            //Cinema cinema = new Cinema()
            //{
            //    Name = model.Name,
            //    Location = model.Location
            //};

            //await this.dbContext.Cinemas.AddAsync(cinema);
            //await this.dbContext.SaveChangesAsync();

            await this.cinemaService.AddCinemaAsync(model);

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            Guid cinemaGuid = Guid.Empty;
            bool isIdValid = this.IsGuidValid(id, ref cinemaGuid);

            if (!isIdValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            CinemaDetailsViewModel? viewModel = await this.cinemaService
                .GetCinemaDetailsByIdAsync(cinemaGuid)

            // Invalid (non-existing) Guid in the URL
            if (viewModel == null)
            {
                return this.RedirectToAction(nameof(Index));
            }
            
            return this.View(viewModel);
        }
    }
}
