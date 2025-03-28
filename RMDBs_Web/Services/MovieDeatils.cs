using Microsoft.Extensions.Configuration;
using RMDBs_Web.Models;
using RMDBs_Web.Models.ViewModel;
using RMDBs_Web.Services.IServices;
using System.Net;
using static RMDB_Utility.Class1;

namespace RMDBs_Web.Services
{
    public class MovieDeatils : BaseServices, IMovieDeatils
    {
        private readonly string _movieUrl;

        public MovieDeatils(IHttpClientFactory httpClient, IConfiguration configuration)
            : base(httpClient)
        {
            _movieUrl = configuration.GetValue<string>("ServiceUrls:RMDBAPI");
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
                ErrorMessages = new List<string> { "Failed to retrieve movie ratings." }
            };
        }
        public async Task<APIResponse<List<MovieDetailsWithRatings>>> GetMoviesByGenreAsync(int genreId)
        {
            var apiRequest = new APIRequest
            {
                //Url = $"{_movieUrl}/api/v1/MovieDetails/GetMoviesByGenre/{genreId}",
                Url = $"{_movieUrl}/api/MovieDetails/GetMoviesByGenre/{genreId}",
                apiType = ApiType.GET
            };

            var response = await SendAsync<List<MovieDetailsWithRatings>>(apiRequest);

            return response ?? new APIResponse<List<MovieDetailsWithRatings>>
            {
                statusCode = HttpStatusCode.InternalServerError,
                IsSuccess = false,
                ErrorMessages = new List<string> { "Failed to retrieve movies by genre." }
            };
        }
        public async Task<APIResponse<List<MovieCastDTO2>>> GetCastByMovieid(int id)
        {
            var apiRequest = new APIRequest
            {
                //Url = $"{_movieUrl}/api/v1/ActorsDetails/ActorList?id={id}",
                Url = $"{_movieUrl}/api/ActorsDetails/ActorList?id={id}",
                apiType = ApiType.GET
            };

            var response = await SendAsync<List<MovieCastDTO2>>(apiRequest);

            return response ?? new APIResponse<List<MovieCastDTO2>>
            {
                statusCode = HttpStatusCode.InternalServerError,
                IsSuccess = false,
                ErrorMessages = new List<string> { "Failed to retrieve movies by genre." }
            };
        }
    }
}
