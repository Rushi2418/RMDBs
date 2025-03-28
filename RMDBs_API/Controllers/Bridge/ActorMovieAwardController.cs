using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMDBs_API.Data.Repositories;
using RMDBs_API.Model.DTO;
using RMDBs_API.Model;
using RMDBs_API.Repositories;
using System.Net;

namespace RMDBs_API.Controllers
{
    [ApiController]
    [Route("api/ActorMovieAward")]
    public class ActorMovieAwardController : ControllerBase
    {
        private readonly IGenericRepository3<ActorMovieAward> _repository;
        private readonly IMapper _mapper;

        public ActorMovieAwardController(IGenericRepository3<ActorMovieAward> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActorMovieAward()
        {
            var response = new APIResponse();

            try
            {
                var actorMovieAwards = await _repository.GetAllAsync(
                    include: query => query
                        .Include(c => c.Actor)
                        .Include(c => c.Movie)
                        .Include(c => c.Award)
                        .Include(c => c.AwardCategory)
                        .Include(c => c.ReciverType)
                );

                // Map to DTOs
                var actorMovieAwardDTOs = _mapper.Map<IEnumerable<ActorMovieAwardDTO>>(actorMovieAwards);

                // Build response
                response.IsSuccess = true;
                response.statusCode = HttpStatusCode.OK;
                response.Result = actorMovieAwardDTOs;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.statusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return StatusCode((int)response.statusCode, response);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetActorMovieAwardById(int id)
        {
            var response = new APIResponse();

            try
            {
                var actorMovieAward = await _repository.GetByIdAsync(id, include: query => query
                    .Include(c => c.Actor)
                    .Include(c => c.Movie)
                    .Include(c => c.Award)
                    .Include(c => c.AwardDiscription)
                    .Include(c => c.AwardCategory));

                if (actorMovieAward == null)
                {
                    response.IsSuccess = false;
                    response.statusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("ActorMovieAward not found.");
                    return StatusCode((int)response.statusCode, response);
                }

                var actorMovieAwardDTO = _mapper.Map<ActorMovieAwardDTO>(actorMovieAward);
                response.IsSuccess = true;
                response.statusCode = HttpStatusCode.OK;
                response.Result = actorMovieAwardDTO;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.statusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add(ex.Message);
            }

            return StatusCode((int)response.statusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActorMovieAward([FromBody] ActorMovieAwardCreateDTO actorMovieAwardCreateDTO)
        {
            var response = new APIResponse();

            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.statusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.AddRange(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return StatusCode((int)response.statusCode, response);
            }

            try
            {
                var actorMovieAward = _mapper.Map<ActorMovieAward>(actorMovieAwardCreateDTO);

                if (actorMovieAward.Typeid == 1)
                {
                    actorMovieAward.MovieID = null;
                }
                else if (actorMovieAward.Typeid == 2)
                {
                    actorMovieAward.ActorID = null;
                }
                else if (actorMovieAward.Typeid == 3)
                {
                    actorMovieAward.MovieID = actorMovieAwardCreateDTO.MovieID;
                    actorMovieAward.ActorID = actorMovieAwardCreateDTO.ActorID;


                }

                await _repository.AddAsync(actorMovieAward);

                var createdActorMovieAwardDTO = _mapper.Map<ActorMovieAwardDTO>(actorMovieAward);
                response.IsSuccess = true;
                response.statusCode = HttpStatusCode.Created;
                response.Result = createdActorMovieAwardDTO;
            }
            catch (DbUpdateException ex)
            {
                response.IsSuccess = false;
                response.statusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add($"Error while saving data: {ex.InnerException?.Message}");
            }

            return StatusCode((int)response.statusCode, response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateActorMovieAward(int id, [FromBody] ActorMovieAwardUpdateDTO actorMovieAwardUpdateDTO)
        {
            var response = new APIResponse();

            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.statusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.AddRange(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return StatusCode((int)response.statusCode, response);
            }

            try
            {
                var actorMovieAward = await _repository.GetByIdAsync(id);
                if (actorMovieAward == null)
                {
                    response.IsSuccess = false;
                    response.statusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("ActorMovieAward not found.");
                    return StatusCode((int)response.statusCode, response);
                }

                _mapper.Map(actorMovieAwardUpdateDTO, actorMovieAward);
                await _repository.UpdateAsync(actorMovieAward);

                response.IsSuccess = true;
                response.statusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.statusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add(ex.Message);
            }

            return StatusCode((int)response.statusCode, response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteActorMovieAward(int id)
        {
            var response = new APIResponse();

            try
            {
                var actorMovieAward = await _repository.GetByIdAsync(id);
                if (actorMovieAward == null)
                {
                    response.IsSuccess = false;
                    response.statusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("ActorMovieAward not found.");
                    return StatusCode((int)response.statusCode, response);
                }

                await _repository.DeleteAsync(id);
                response.IsSuccess = true;
                response.statusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.statusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add(ex.Message);
            }

            return StatusCode((int)response.statusCode, response);
        }
    }
}
