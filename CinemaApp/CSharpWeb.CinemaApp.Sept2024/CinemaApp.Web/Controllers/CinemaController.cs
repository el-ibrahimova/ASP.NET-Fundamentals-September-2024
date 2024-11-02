using CinemaApp.Services.Data.Interfaces;
using CinemaApp.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Cinema;

    public class CinemaController : BaseController
    {
        private readonly ICinemaService cinemaService;
        private readonly IManagerService managerService;

        public CinemaController(ICinemaService cinemaService, IManagerService managerService )
        {
            this.cinemaService = cinemaService;
            this.managerService = managerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
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
                .GetCinemaDetailsByIdAsync(cinemaGuid);

            // Invalid (non-existing) Guid in the URL
            if (viewModel == null)
            {
                return this.RedirectToAction(nameof(Index));
            }
            
            return this.View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            string userId = this.User.GetUserId();

            bool isManager = await this.managerService.IsUserManagerAsync(userId);

            if (!isManager)
            {
                return this.RedirectToAction(nameof(Index));
            }

            IEnumerable<CinemaIndexViewModel> cinemas = await this.cinemaService
                .IndexGetAllOrderedByLocationAsync();

            return this.View(cinemas);

        }
    }
}
