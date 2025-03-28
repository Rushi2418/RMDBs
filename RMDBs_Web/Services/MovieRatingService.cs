using Microsoft.Extensions.Configuration;
using RMDBs_Web.Models;
using RMDBs_Web.Models.DTO;
using RMDBs_Web.Models.ViewModel;
using RMDBs_Web.Services.IServices;
using System.Net;
using static RMDB_Utility.Class1;

namespace RMDBs_Web.Services
{
    public class MovieRatingService : BaseServices, IMovieRatingService
    {
        private readonly string _movieUrl;

        public MovieRatingService(IHttpClientFactory httpClient, IConfiguration configuration)
            : base(httpClient)
        {
            _movieUrl = configuration.GetValue<string>("ServiceUrls:RMDBAPI");
        }

        // Return APIResponse with MoviesResponseDTO type
        public async Task<APIResponse<MoviesResponseDTO>> GetMovieRatings()
        {
            var apiRequest = new APIRequest
            {
                //Url = $"{_movieUrl}/api/v1/MovieDetails/GetRatings",
                Url = $"{_movieUrl}/api/MovieDetails/GetRatings",
                apiType = ApiType.GET
            };

            var response = await SendAsync<MoviesResponseDTO>(apiRequest);

            return response ?? new APIResponse<MoviesResponseDTO>
            {
                statusCode = HttpStatusCode.InternalServerError,
                IsSuccess = false,
                ErrorMessages = new List<string> { "Failed to retrieve movie ratings." }
            };
        }

        public async Task<APIResponse<MovieDetailsVM>> GetMovieDetails(int id)
        {
            var apiRequest = new APIRequest
            {
                //Url = $"{_movieUrl}/api/v1/MovieDetails/GetMovieDetails/{id}",
                Url = $"{_movieUrl}/api/MovieDetails/GetMovieDetails/{id}",
                apiType = ApiType.GET
            };

            var response = await SendAsync<MovieDetailsVM>(apiRequest);

            return response ?? new APIResponse<MovieDetailsVM>
            {
                statusCode = HttpStatusCode.InternalServerError,
                IsSuccess = false,
                ErrorMessages = new List<string> { "Failed to retrieve movie details." }
            };
        }
    }
}
