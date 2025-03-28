using RMDBs_Web.Models;
using RMDBs_Web.Models.DTO;
using RMDBs_Web.Models.ViewModel;

namespace RMDBs_Web.Services.IServices
{
    public interface IMovieRatingService
    {
        Task<APIResponse<MoviesResponseDTO>> GetMovieRatings();
        Task<APIResponse<MovieDetailsVM>> GetMovieDetails(int id);

    }
}
