using CinemaApp.Data.Models;
using CinemaApp.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static CinemaApp.Common.ErrorMessages.Watchlist;

namespace CinemaApp.Web.Controllers
{
    [Authorize]
    public class WatchlistController : BaseController
    {
        private readonly IWatchlistService watchlistService;
        private readonly UserManager<ApplicationUser> userManager;

        public WatchlistController(UserManager<ApplicationUser> userManager, 
            IWatchlistService watchlistService, IManagerService managerService)
        :base(managerService)
        {
            this.userManager = userManager;
            this.watchlistService = watchlistService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = this.userManager.GetUserId(User)!;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.RedirectToPage("/Identity/Account/Login");
            }

            var watchlist = await this.watchlistService.GetUserWatchlistByUserIdAsync(userId);

            return View(watchlist);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWatchlist(string? movieId)
        {
            string userId = this.userManager.GetUserId(User)!;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.RedirectToPage("/Identity/Account/Login");
            }

            bool result =await this.watchlistService
                .AddMovieToUserWatchlistAsync(movieId, userId);

            if (result == false)
            {
                TempData[nameof(AddToWatchlistNotSuccessfullMessage)] = AddToWatchlistNotSuccessfullMessage;
                
                return this.RedirectToAction("Index", "Movie");
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWatchlist(string? movieId)
        {

            string userId = this.userManager.GetUserId(User)!;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.RedirectToPage("/Identity/Account/Login");
            }

            bool result = await this.watchlistService.RemoveMovieFromUserWatchlistAsync(movieId, userId);

            if (result == false)
            {
                // TODO: Implement a way to transfer the Error Message to the View
                return this.RedirectToAction("Index", "Movie");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
