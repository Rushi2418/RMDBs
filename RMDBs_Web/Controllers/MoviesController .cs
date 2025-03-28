using Microsoft.AspNetCore.Mvc;
using RMDBs_Web.Models.ViewModel;
using RMDBs_Web.Services;
using RMDBs_Web.Services.IServices;

namespace RMDBs_Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieDeatils _movieServices;

        public MoviesController(IMovieDeatils movieServices)
        {
            _movieServices = movieServices;


        }
        public IActionResult Details(int id)
        {
            var response = _movieServices.GetMovieDetails(id).Result;

            if (response == null || !response.IsSuccess || response.Result == null)
            {
                return View(new MovieDetailsVM());
            }

            return View(response.Result); // This will render the 'Details' view
        }
        public async Task<IActionResult> MoviesByGenre(int genreId)
        {
            if (genreId == 0)
            {
                return View(new List<MovieDetailsWithRatings>());
            }

            var response = await _movieServices.GetMoviesByGenreAsync(genreId);

            if (response == null || !response.IsSuccess || response.Result == null)
            {
                ViewBag.ErrorMessage = "No movies found for the selected genre.";
                return View(new List<MovieDetailsWithRatings>());
            }

            return View(response.Result);
        }
        public async Task<IActionResult> MovieCrew(int id)
        {
            var response = await _movieServices.GetCastByMovieid(id);
            if (response == null || !response.IsSuccess || response.Result == null)
            {
                ViewBag.ErrorMessage = "No movies found for the selected genre.";
                return View(new List<MovieCastDTO2>());
            }
            return View(response.Result);
        }

    }
}
