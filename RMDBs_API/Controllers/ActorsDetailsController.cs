using Microsoft.AspNetCore.Mvc;
using RMDBs_API.Data;
using RMDBs_API.Model;
using RMDBs_API.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace RMDBs_API.Controllers
{
    
    [Route("api/ActorsDetails")]
    [ApiController]
    public class ActorsDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly APIResponse _response;

        public ActorsDetailsController(ApplicationDbContext context)
        {
            _context = context;
            _response = new APIResponse();
        }

        [HttpGet("ActorDetail1")]
        public async Task<IActionResult> GetActorDetails([FromQuery] int id, [FromQuery] int movieId)
        {
            if (id <= 0)
            {
                _response.statusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid actor ID" };
                return BadRequest(_response);
            }

            var actorDetails = await _context.Actors
                .Where(a => a.ID == id)
                .Select(a => new ActorDetailsDTO
                {
                    ID = a.ID,
                    Name = a.Name,
                    Age = a.Age,
                    Height = a.Height,
                    BornDate = a.BornDate,
                    BornPlace = a.BornPlace,
                    Parents = a.Parents,
                    Chilldren = a.Chilldren,
                    DebutMovie = a.DebutMovie,
                    Biography = a.Biography,
                    ActorImage = _context.MovieMedias1
                        .Where(mm => mm.ActroId == a.ID && mm.ActorImgFlag == true && mm.ActiveFlag == true)
                        .Select(mm => mm.FilePath)
                        .FirstOrDefault() ?? "Default.jpg", 
                    Movies = _context.Movies
                        .Where(m => _context.MovieCasts
                            .Any(mc => mc.MovieID == m.ID && mc.ActorID == a.ID && mc.ActiveFlag == true))
                        .Select(m => new MovieByActorDTO
                        {
                            ID = m.ID,
                            MovieID = m.ID,
                            Name = m.Name,
                            Description = m.Description,
                            ReleaseDate = m.ReleaseDate,
                            Role = _context.MovieCasts
                                .Where(mc => mc.MovieID == m.ID && mc.ActorID == a.ID)
                                .Select(mc => mc.Role)
                                .FirstOrDefault() ?? "Unknown",
                            MovieImg = _context.MovieMedias1
                                        .Where(mm => mm.MovieID == m.ID && mm.MovieImgFlag == true && mm.ActiveFlag == true)
                                        .Select(mm => mm.FilePath)
                                        .FirstOrDefault() ?? "Default.jpg"
                        }).ToList(),


                })
                .FirstOrDefaultAsync();

            if (actorDetails == null)
            {
                _response.statusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Actor not found" };
                return NotFound(_response);
            }

            _response.statusCode = System.Net.HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = actorDetails;

            return Ok(_response);
        }

        [HttpGet("ActorList")]

        public async Task<IActionResult> GetActors(int id)
        {
            if (id <= 0)
            {
                _response.statusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid actor ID" };
                return BadRequest(_response);
            }

            var actorsByMovie = await _context.MovieCasts
                .Where(ac => ac.MovieID == id)
                .Select(ac => new MovieCastDTO2
                {
                    ActorID = ac.ActorID,
                    MovieID = ac.MovieID,
                    PositionName = ac.Position.Name,
                    Role = ac.Role,
                    ActorName = ac.Actor.Name,
                    Img = _context.MovieMedias1
                        .Where(mm => mm.ActroId == ac.ActorID && mm.ActorImgFlag == true && mm.ActiveFlag == true)
                        .Select(mm => mm.FilePath)
                        .FirstOrDefault() ?? "Images/Home/Default.png"
                })

                .ToListAsync();

            if (actorsByMovie == null)
            {
                _response.statusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Actor not found" };
                return NotFound(_response);
            }
            _response.statusCode = System.Net.HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = actorsByMovie;
            return Ok(_response);
        }

    }
}
