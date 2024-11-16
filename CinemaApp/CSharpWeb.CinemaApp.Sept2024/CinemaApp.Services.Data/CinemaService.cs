using CinemaApp.Services.Mapping;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Data
{
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using Interfaces;
    using Web.ViewModels.Cinema;

    public class CinemaService : BaseService, ICinemaService
    {
        private readonly IRepository<Cinema, Guid> cinemaRepository;

        public CinemaService(IRepository<Cinema, Guid> cinemaRepository)
        {
            this.cinemaRepository = cinemaRepository;
        }

        public async Task<IEnumerable<CinemaIndexViewModel>> IndexGetAllOrderedByLocationAsync()
        {
            //var cinemas = await this.dbContext.Cinemas
            //     .Select(c => new CinemaIndexViewModel()
            //     {
            //         Id = c.Id.ToString(),
            //         Name = c.Name,
            //         Location = c.Location
            //     })
            //     .OrderBy(c => c.Location)
            //     .ToArrayAsync();

            var cinemas = await this.cinemaRepository
                .GetAllAttached()
                .Where(c=>c.IsDeleted==false)
                .OrderBy(c => c.Location)
                .To<CinemaIndexViewModel>()
                .ToArrayAsync();

            return cinemas;
        }

        public async Task AddCinemaAsync(AddCinemaFormModel model)
        {
            //Cinema cinema = new Cinema()
            //{
            //    Name = model.Name,
            //    Location = model.Location
            //};

            //await this.dbContext.Cinemas.AddAsync(cinema);
            //await this.dbContext.SaveChangesAsync();
            
            Cinema cinema = new Cinema();

            AutoMapperConfig.MapperInstance.Map(model, cinema);

            await this.cinemaRepository.AddAsync(cinema);
        }

        public async Task<CinemaDetailsViewModel?> GetCinemaDetailsByIdAsync(Guid id)
        {
            Cinema? cinema = await this.cinemaRepository
                .GetAllAttached()
                .Include(c => c.MovieCinemas)
                .ThenInclude(cm => cm.Movie)
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);

            CinemaDetailsViewModel? viewModel = null;

            // invalid (non-existing) GUID in the URL
            if (cinema != null)
            {
                viewModel = new CinemaDetailsViewModel()
                {
                    Id=cinema.Id.ToString(),
                    Name = cinema.Name,
                    Location = cinema.Location,
                    Movies = cinema
                        .MovieCinemas
                        .Where(cm => cm.IsDeleted == false)
                        .Select(cm => new CinemaMovieViewModel()
                        {
                            Genre = cm.Movie.Genre,
                            Description = cm.Movie.Description,
                            Title = cm.Movie.Title,
                            Duration = cm.Movie.Duration
                        })
                        .ToArray()
                };
            }

            return viewModel;
        }

        public async Task<EditCinemaFormModel?> GetCinemaForEditByIdAsync(Guid id)
        {
            EditCinemaFormModel? cinemaModel = await this.cinemaRepository
                .GetAllAttached()
                .Where(c=>c.IsDeleted==false)
                .To<EditCinemaFormModel>()
                .FirstOrDefaultAsync(c=>c.Id.ToLower()== id.ToString().ToLower());

            return cinemaModel;
        }

        public async Task<bool> EditCinemaAsync(EditCinemaFormModel model)
        {
            Cinema cinemaEntity = AutoMapperConfig.MapperInstance.Map<EditCinemaFormModel, Cinema>(model);

            bool result = await this.cinemaRepository.UpdateAsync(cinemaEntity);

            return result;
        }

        public async Task<DeleteCinemaViewModel?> GetCinemaForDeleteByIdAsync(Guid id)
        {
            DeleteCinemaViewModel? cinemaToDelete = await this.cinemaRepository
                .GetAllAttached()
                .Where(c => c.IsDeleted == false)
                .To<DeleteCinemaViewModel>()
                .FirstOrDefaultAsync(c => c.Id.ToString().ToLower() == id.ToString().ToLower());

            return cinemaToDelete;
        }

        public async Task<bool> SoftDeleteCinemaAsync(Guid id)
        {
            Cinema? cinemaToDelete = await this.cinemaRepository
                .FirstOrDefaultAsync(c => c.Id.ToString().ToLower() == id.ToString().ToLower());

            if (cinemaToDelete == null)
            {
                return false;
            }

            cinemaToDelete.IsDeleted = true;
           return await this.cinemaRepository.UpdateAsync(cinemaToDelete);
        }
    }
}


