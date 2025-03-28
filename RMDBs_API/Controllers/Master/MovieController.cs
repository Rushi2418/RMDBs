using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMDBs_API.Model;
using RMDBs_API.Model.DTO;
using RMDBs_API.Repositories;
using System.Net;

namespace RMDBs_API.Controllers.Master
{
    [Route("api/Movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IGenericRepository<Movie> _movieRepository;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public MovieController(IGenericRepository<Movie> movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _response = new APIResponse();
        }

        // Get all movies
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> GetMovies()
        {
            var movies = await _movieRepository.FindAsync(movie => movie.ActiveFlag == true);

            if (!movies.Any())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "No active movies found." };
                _response.statusCode = HttpStatusCode.NoContent;
                return NoContent();
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<IEnumerable<MovieDTO>>(movies);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // Get movie by ID
        [HttpGet("{id:int}", Name = "GetMovie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetMovie(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null || !movie.ActiveFlag)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Movie not found or inactive." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<MovieDTO>(movie);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // Create a new movie
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateMovie([FromBody] MovieCreateDTO movieDTO)
        {
            if (movieDTO == null || !ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var existingMovie = await _movieRepository.FindAsync(movie => movie.Name == movieDTO.Name);
            if (existingMovie.Any())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Movie with the same title already exists." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var movie = _mapper.Map<Movie>(movieDTO);
            await _movieRepository.AddAsync(movie);

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<MovieDTO>(movie);
            _response.statusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetMovie", new { id = movie.ID }, _response);
        }

        // Update an existing movie
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieUpdateDTO movieDTO)
        {
            if (id <= 0 || movieDTO == null || id != movieDTO.ID)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data or ID mismatch." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var existingMovie = await _movieRepository.GetByIdAsync(id);
            if (existingMovie == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Movie not found." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _mapper.Map(movieDTO, existingMovie);
            await _movieRepository.UpdateAsync(existingMovie);

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }

        // Delete a movie
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null || !movie.ActiveFlag)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Movie not found or already inactive." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            movie.ActiveFlag = false;
            await _movieRepository.UpdateAsync(movie);

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }
    }
}
