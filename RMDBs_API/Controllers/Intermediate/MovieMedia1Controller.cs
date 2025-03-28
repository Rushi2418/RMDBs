using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMDBs_API.Model;
using RMDBs_API.Model.DTO;
using RMDBs_API.Repositories;
using RMDBs_Web.Models.DTO;
using System.Net;

namespace RMDBs_API.Controllers
{
    [ApiController]
    [Route("api/MovieMedia1")]
    public class MovieMedia1Controller : ControllerBase
    {
        private readonly IGenericRepository2<MovieMedia1> _movieMedia1Repository;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public MovieMedia1Controller(IGenericRepository2<MovieMedia1> movieMedia1Repository, IMapper mapper)
        {
            _movieMedia1Repository = movieMedia1Repository;
            _mapper = mapper;
            _response = new APIResponse();
        }

        // Get all MovieMedia1 records
        [HttpGet]
        public async Task<IActionResult> GetAllMovieMedia1s()
        {
            try
            {
                var movieMedia1s = await _movieMedia1Repository.GetAllAsync(
                    include: query => query.Include(m => m.Movie)
                                           .Include(m => m.Actor)
                                           .Include(m => m.ReceiverType)
                );

                if (!movieMedia1s.Any())
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "No movie media records found." };
                    _response.statusCode = HttpStatusCode.NoContent;
                    return NoContent();
                }

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<IEnumerable<MovieMedia1DTO>>(movieMedia1s);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.statusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }

        // Create MovieMedia1
        [HttpPost]
        public async Task<IActionResult> CreateMovieMedia([FromBody] MovieMedia1CreateDTO movieMediaCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            // Check the custom validation
            if (!movieMediaCreateDTO.IsValid())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid data: MovieID or ActorId cannot be null based on ReceiverId." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                var movieMedia = _mapper.Map<MovieMedia1>(movieMediaCreateDTO);

                if (movieMedia.ReceiverId == 1)
                {
                    movieMedia.MovieID = null; // Ensure MovieId is null
                    movieMedia.ActroId = movieMediaCreateDTO.ActorId; // ✅ Explicitly set ActorId

                }

                else if (movieMedia.ReceiverId == 2)
                {
                    movieMedia.ActroId = null; // Ensure MovieId is null
                    movieMedia.MovieID = movieMediaCreateDTO.MovieID; // Ensure MovieId is null

                }
                else
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Invalid ReceiverId. It must be either 1 or 2." };
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                // Save the entity to the database
                await _movieMedia1Repository.AddAsync(movieMedia);

                // Map response DTO
                var createdMovieMediaDTO = _mapper.Map<MovieMedia1DTO>(movieMedia);
                _response.IsSuccess = true;
                _response.Result = createdMovieMediaDTO;
                _response.statusCode = HttpStatusCode.Created;

                // Return CreatedAtAction response with the new resource URL
                return CreatedAtAction(nameof(GetMovieMedia1ById), new { id = movieMedia.ID }, _response);
            }
            catch (DbUpdateException ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { $"Database error: {ex.InnerException?.Message}" };
                _response.statusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.statusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }

        // Get MovieMedia1 by ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMovieMedia1ById(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                var movieMedia1 = await _movieMedia1Repository.GetByIdAsync(id,
                    include: query => query.Include(m => m.Movie)
                                           .Include(m => m.Actor)
                                           .Include(m => m.ReceiverType)
                                           
                                           .Where(m => m.ID == id));

                if (movieMedia1 == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Movie media not found." };
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<MovieMedia1DTO>(movieMedia1);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.statusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMovieMedia1(int id, [FromBody] MovieMedia1UpdateDTO movieMedia1UpdateDTO)
        {
            if (id <= 0 || id != movieMedia1UpdateDTO.ID)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data or ID mismatch." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                var movieMedia1 = await _movieMedia1Repository.GetByIdAsync(id,
                    include: query => query.Include(m => m.Movie)
                                           .Include(m => m.Actor)
                                           .Include(m => m.ReceiverType)

                                           .Where(m => m.ID == id));
                if (movieMedia1 == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Movie media not found." };
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                // Map the update DTO onto the existing entity
                _mapper.Map(movieMedia1UpdateDTO, movieMedia1);

                // Apply ReceiverId specific logic
                if (movieMedia1UpdateDTO.ReceiverId == 1)
                {
                    movieMedia1.MovieID = null;
                    movieMedia1.ActroId = movieMedia1UpdateDTO.ActorId; // ✅ Explicitly set ActorId

                }
                else if (movieMedia1UpdateDTO.ReceiverId == 2)
                {
                    movieMedia1.ActroId = null;
                    movieMedia1.MovieID = movieMedia1UpdateDTO.MovieID; // ✅ Explicitly set ActorId

                }
                else
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Invalid ReceiverId. It must be either 1 or 2." };
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                await _movieMedia1Repository.UpdateAsync(movieMedia1);

                _response.IsSuccess = true;
                _response.statusCode = HttpStatusCode.NoContent;
                return NoContent();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.statusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }

        // Delete MovieMedia1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovieMedia1(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                var movieMedia1 = await _movieMedia1Repository.GetByIdAsync(id);
                if (movieMedia1 == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Movie media not found." };
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                movieMedia1.ActiveFlag = false;
                await _movieMedia1Repository.UpdateAsync(movieMedia1);

                _response.IsSuccess = true;
                _response.statusCode = HttpStatusCode.NoContent;
                return NoContent();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.statusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }
    }
}
