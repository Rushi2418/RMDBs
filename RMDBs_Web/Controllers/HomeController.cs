using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RMDBs_Web.Models;
using RMDBs_Web.Models.DTO;
using RMDBs_Web.Models.ViewModel;
using RMDBs_Web.Services.IServices;
using System.Collections.Generic;

namespace RMDBs_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieRatingService _movieServices;

        public HomeController(IMovieRatingService movieServices)
        {
            _movieServices = movieServices;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _movieServices.GetMovieRatings();

            if (response == null || !response.IsSuccess)
            {
                return View(new MoviesResponseDTO
                {
                    MoviesWithRatings = new List<MovieDetailsWithRatings>(),
                    MoviesByRating = new List<MovieDetailsWithRatings>(),
                    MoviesByGenre = new List<MoviesByGenreDTO1>()
                });

            }

            // Deserialize into MoviesResponseDTOac
            var moviesResponse = response.Result as MoviesResponseDTO;
            foreach (var genre in moviesResponse.MoviesByGenre)
            {
                Console.WriteLine($"GenreID: {genre.id}, GenreName: {genre.Genre}");
            }

            return View(moviesResponse);
        }

    }
}
