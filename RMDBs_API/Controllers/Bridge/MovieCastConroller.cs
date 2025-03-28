using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMDBs_API.Model;
using RMDBs_API.Model.DTO;
using RMDBs_API.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using RMDBs_API.Repositories;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace RMDBs_API.Controllers
{
    [Route("api/MovieCast")]

    [ApiController]
    //[Route("api/MovieCast")]
    public class MovieCastController : ControllerBase
    {
        private readonly IGenericRepository3<MovieCast> _repository;
        private readonly IMapper _mapper;

        public MovieCastController(IGenericRepository3<MovieCast> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovieCast()
        {
            var response = new APIResponse();

            try
            {
                var movieCasts = await _repository.GetAllAsync(
                                        include: query => query
                                        .Include(c => c.Movie)
                                        .Include(c => c.Actor)
                                        .Include(c => c.Position));
                var movieCastDTOs = _mapper.Map<IEnumerable<MovieCastDTO>>(movieCasts);

                response.IsSuccess = true;
                response.statusCode = HttpStatusCode.OK;
                response.Result = movieCastDTOs;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.statusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add(ex.Message);
            }

            return StatusCode((int)response.statusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieCastById(int id)
        {
            var response = new APIResponse();

            try
            {
                var movieCast = await _repository.GetByIdAsync(id, 
                                        include: query => query
                                        .Include(c => c.Movie)
                                        .Include(c => c.Actor)
                                        .Include(c => c.Position));
                if (movieCast == null)
                {
                    response.IsSuccess = false;
                    response.statusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("MovieCast not found.");
                    return StatusCode((int)response.statusCode, response);
                }

                var movieCastDTO = _mapper.Map<MovieCastDTO>(movieCast);
                response.IsSuccess = true;
                response.statusCode = HttpStatusCode.OK;
                response.Result = movieCastDTO;
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
        public async Task<IActionResult> CreateMovieCast([FromBody] MovieCastCreateDTO movieCastCreateDTO)
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
                var movieCast = _mapper.Map<MovieCast>(movieCastCreateDTO);
                await _repository.AddAsync(movieCast);

                var createdMovieCastDTO = _mapper.Map<MovieCastDTO>(movieCast);
                response.IsSuccess = true;
                response.statusCode = HttpStatusCode.Created;
                response.Result = createdMovieCastDTO;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.statusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add(ex.Message);
            }

            return StatusCode((int)response.statusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovieCast(int id, [FromBody] MovieCastUpdateDTO movieCastUpdateDTO)
        {
            var response = new APIResponse();

            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.statusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.AddRange(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return StatusCode((int)response.statusCode, response);
            }

            if (id != movieCastUpdateDTO.ID)
            {
                response.IsSuccess = false;
                response.statusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add("ID mismatch.");
                return StatusCode((int)response.statusCode, response);
            }

            try
            {
                var movieCast = await _repository.GetByIdAsync(id,
                                        include: query => query
                                        .Include(c => c.Movie)
                                        .Include(c => c.Actor)
                                        .Include(c => c.Position));
                if (movieCast == null)
                {
                    response.IsSuccess = false;
                    response.statusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("MovieCast not found.");
                    return StatusCode((int)response.statusCode, response);
                }

                _mapper.Map(movieCastUpdateDTO, movieCast);
                await _repository.UpdateAsync(movieCast);

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieCast(int id)
        {
            var response = new APIResponse();

            try
            {
                var movieCast = await _repository.GetByIdAsync(id, include: query => query.Include(c => c.Movie).Include(c => c.Actor).Include(c => c.Position));
                if (movieCast == null)
                {
                    response.IsSuccess = false;
                    response.statusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("MovieCast not found.");
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
