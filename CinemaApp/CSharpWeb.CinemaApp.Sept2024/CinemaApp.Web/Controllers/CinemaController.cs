using System.Security.Cryptography;
using CinemaApp.Common;
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

        public CinemaController(ICinemaService cinemaService, IManagerService managerService)
        : base(managerService)
        {
            this.cinemaService = cinemaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cinemas = await this.cinemaService
    .IndexGetAllOrderedByLocationAsync();

            return this.View(cinemas);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            bool isManager = await this.IsUserManagerAsync();

            if (!isManager)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(AddCinemaFormModel model)
        {
            bool isManager = await this.IsUserManagerAsync();

            if (!isManager)
            {
                return this.RedirectToAction(nameof(Index));
            }

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

            await this.AppendUserCookieAsync();

            return this.View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            bool isManager = await this.IsUserManagerAsync();

            if (!isManager)
            {
                return this.RedirectToAction(nameof(Index));
            }

            IEnumerable<CinemaIndexViewModel> cinemas = await this.cinemaService
                .IndexGetAllOrderedByLocationAsync();

            return this.View(cinemas);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            bool isManager = await this.IsUserManagerAsync();
            if (!isManager)
            {
                //TODO: Implement notifications for error and warning messages
                return RedirectToAction(nameof(Index));
            }

            Guid cinemaGuid = Guid.Empty;
            bool isIdValid = this.IsGuidValid(id, ref cinemaGuid);

            if (!isIdValid)
            {
                return this.RedirectToAction(nameof(Manage));
            }

            EditCinemaFormModel? formModel = await this.cinemaService
                .GetCinemaForEditByIdAsync(cinemaGuid);

            if (formModel == null)
            {
                return this.RedirectToAction(nameof(Manage));
            }

            return this.View(formModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditCinemaFormModel formModel)
        {
            bool isManager = await this.IsUserManagerAsync();
            if (!isManager)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return this.View(formModel);
            }

            bool isUpdated = await this.cinemaService.EditCinemaAsync(formModel);

            if (!isUpdated)
            {
                ModelState.AddModelError(String.Empty, "Unexpected error occurred while updating.");
                return this.View(formModel);
            }

            return this.RedirectToAction(nameof(Details), "Cinema", new { id = formModel.Id });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
             bool isManager = await this.IsUserManagerAsync();
            if (await this.IsUserManagerAsync())
            {
                return this.RedirectToAction(nameof(Index));
            } 
            
            Guid cinemaGuid = Guid.Empty;
            if (this.IsGuidValid(id, ref cinemaGuid))
            {
                return this.RedirectToAction(nameof(Manage));
            }
            
            DeleteCinemaViewModel? cinemaToDeleteViewModel =
                await this.cinemaService.GetCinemaForDeleteByIdAsync(cinemaGuid);

            if (cinemaToDeleteViewModel == null)
            {
                return this.RedirectToAction(nameof(Manage));
            }

            return this.View(cinemaToDeleteViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SoftDeleteConfirmed(DeleteCinemaViewModel cinema)
        {
            bool isManager = await this.IsUserManagerAsync();
            if (await this.IsUserManagerAsync())
            {
                return this.RedirectToAction(nameof(Index));
            }

            Guid cinemaGuid = Guid.Empty;
            if (this.IsGuidValid(cinema.Id, ref cinemaGuid))
            {
                return this.RedirectToAction(nameof(Manage));
            }

            bool isDeleted = await this.cinemaService.SoftDeleteCinemaAsync(cinemaGuid);

            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Unexpected error occurred while trying to delete the cinema. Please contact system administrator";

                return this.RedirectToAction(nameof(Delete), new { id = cinema.Id});
            }

            return this.RedirectToAction(nameof(Manage));
        }
    }
}
