using RMDBs_Web.Models;
using RMDBs_Web.Models.ViewModel;

namespace RMDBs_Web.Services.IServices
{
    public interface IActorDeatils
    {
        Task<APIResponse<ActorDetailsDTO>> GetActorDetails(int actorId, int movieId);
    }
}
