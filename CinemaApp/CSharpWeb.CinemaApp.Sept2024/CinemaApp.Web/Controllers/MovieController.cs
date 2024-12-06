using CinemaApp.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NuGet.Common;

namespace CinemaApp.Web.Controllers
{
    using CinemaApp.Web.ViewModels.Cinema;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Movie;
    using static Common.EntityValidationConstants.Movie;

    public class MovieController : BaseController
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService, IManagerService managerService)
        : base(managerService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(AllMoviesSearchFilterViewModel inputModel)
        {
            IEnumerable<AllMoviesIndexViewModel> allMovies = await this.movieService.GetAllMoviesAsync(inputModel);

            AllMoviesSearchFilterViewModel viewModel = new AllMoviesSearchFilterViewModel();


            viewModel.Movies = allMovies;
            viewModel.AllGenres = await this.movieService.GetAllGenresAsync();

            viewModel.CurrentPage = inputModel.CurrentPage;

            viewModel.TotalPages = (int)Math.Ceiling(((double)allMovies.Count() / inputModel.EntitiesPerPage!.Value));


            return this.View(viewModel);
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

            AddMovieToCinemaInputModel? viewModel = await this.movieService
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

            Guid movieGuid = Guid.Empty;
            bool isIdValid = this.IsGuidValid(id, ref movieGuid);

            if (!isIdValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            EditMovieViewModel? formModel = await this.movieService
                .GetEditMovieViewModelByIdAsync(movieGuid); if (formModel == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(formModel); return this.View(formModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditMovieViewModel formModel)
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

            bool isUpdated = await this.movieService.EditMovieAsync(formModel);

            if (!isUpdated)
            {
                ModelState.AddModelError(String.Empty, "Unexpected error occurred while updating.");
                return this.View(formModel);
            }

            return this.RedirectToAction(nameof(Details), "Movie", new { id = formModel.Id });
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

            //TODO: Implement the same filtering, search and pagination here

            IEnumerable<AllMoviesIndexViewModel> movies = await this.movieService.GetAllMoviesAsync(new AllMoviesSearchFilterViewModel());

            return this.View(movies);
        }
    }
}