using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RMDBs_Web.Services.IServices;

namespace YourNamespace.Web.Controllers
{
    public class ActorDetailsController : Controller
    {
        private readonly IActorDeatils _actorService;

        public ActorDetailsController(IActorDeatils actorService)
        {
            _actorService = actorService;
        }



        public async Task<IActionResult> ActorDetails(int actorId, int movieId)
        {
            var response = await _actorService.GetActorDetails(actorId, movieId);

            if (response == null || !response.IsSuccess)
            {
                return NotFound();
            }

            // Extract the actual DTO from the API response
            var actorDetails = response.Result;

            return View(actorDetails);
        }

    }
}
