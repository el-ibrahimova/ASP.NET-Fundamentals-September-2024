﻿using CinemaApp.Web.ViewModels.Movie;

namespace CinemaApp.Services.Data.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesAsync();

        Task <bool> AddMovieAsync(AddMovieInputModel inputModel);

    }
}