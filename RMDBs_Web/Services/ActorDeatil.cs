using Microsoft.Extensions.Configuration;
using RMDBs_Web.Models;
using RMDBs_Web.Models.ViewModel;
using RMDBs_Web.Services.IServices;
using System.Net;
using static RMDB_Utility.Class1;

namespace RMDBs_Web.Services
{
    public class ActorDeatil : BaseServices, IActorDeatils
    {
        private readonly string _actorUrl;

        public ActorDeatil(IHttpClientFactory httpClient, IConfiguration configuration)
            : base(httpClient)
        {
            _actorUrl = configuration.GetValue<string>("ServiceUrls:RMDBAPI");
        }

        public async Task<APIResponse<ActorDetailsDTO>> GetActorDetails(int actorId, int movieId)
        {
            var apiRequest = new APIRequest
            {
                //Url = $"{_actorUrl}/api/v1/ActorsDetails/ActorDetail?id={actorId}&movieId={movieId}",
                Url = $"{_actorUrl}/api/ActorsDetails/ActorDetail1?id={actorId}&movieId={movieId}",
                apiType = ApiType.GET
            };

            var response = await SendAsync<ActorDetailsDTO>(apiRequest);

            return response ?? new APIResponse<ActorDetailsDTO>
            {
                statusCode = HttpStatusCode.InternalServerError,
                IsSuccess = false,
                ErrorMessages = new List<string> { "Failed to retrieve actor details." }
            };
        }
    }
}
