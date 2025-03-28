using RMDBs_Web.Models;
using RMDBs_Web.Models.ViewModel;

namespace RMDBs_Web.Services.IServices
{
    public interface IMovieDeatils
    {
        Task<APIResponse<MovieDetailsVM>> GetMovieDetails(int id);
        Task<APIResponse<List<MovieDetailsWithRatings>>> GetMoviesByGenreAsync(int genreId);
        Task<APIResponse<List<MovieCastDTO2>>> GetCastByMovieid(int id);


    }
}
