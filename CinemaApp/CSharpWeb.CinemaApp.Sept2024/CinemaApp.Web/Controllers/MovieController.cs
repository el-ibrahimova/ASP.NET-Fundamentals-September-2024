using CinemaApp.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Movie;
    using static Common.EntityValidationConstants.Movie;

    public class MovieController : BaseController
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService, IManagerService managerService)
        :base(managerService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allMovies = await this.movieService.GetAllMoviesAsync();
            return this.View(allMovies);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            bool isManager = await this.IsUserManagerAsync();

            if (!isManager)
            {
                return RedirectToAction(nameof(Index));
            }
            
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(AddMovieInputModel inputModel)
        {
            bool isManager = await this.IsUserManagerAsync();

            if (!isManager)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!this.ModelState.IsValid)
            {
                // Render the same form with user entered values + model errors
                return this.View(inputModel);
            }

            bool result = await this.movieService.AddMovieAsync(inputModel);

            if (result == false)
            {
                this.ModelState.AddModelError(nameof(inputModel.ReleaseDate), string.Format("The Release Date must be in the following format: {0}", ReleaseDateFormat));

                return this.View(inputModel);
            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            Guid movieGuid = Guid.Empty;

            bool isGuidValid = this.IsGuidValid(id, ref movieGuid);
            if (!isGuidValid)
            {
                // invalid id format
                return this.RedirectToAction(nameof(Index));
            }

            MovieDetailsViewModel? movie = await this.movieService
                .GetMovieDetailsByIdAsync(movieGuid);

            if (movie == null)
            {
                // Non-existing movie guid
                return this.RedirectToAction(nameof(Index));
            }
            
            return this.View(movie);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddToProgram(string? id)
        {
            bool isManager = await this.IsUserManagerAsync();

            if (!isManager)
            {
                return RedirectToAction(nameof(Index));
            }

           Guid movieGuid = Guid.Empty;

            bool isGuidValid = this.IsGuidValid(id, ref movieGuid);
            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            AddMovieToCinemaInputModel? viewModel =await this.movieService
                .GetAddMovieToCinemaInputModelByIdAsync(movieGuid);

            if (viewModel == null)
            {
                RedirectToAction(nameof(Index));
            }


            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToProgram(AddMovieToCinemaInputModel model)
        {
            bool isManager = await this.IsUserManagerAsync();

            if (!isManager)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidValid(model.Id, ref movieGuid);

            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            bool result = await this.movieService
                .AddMovieToCinemasAsync(movieGuid, model);

            if (result == false)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Index), "Cinema");
        }
    }
}