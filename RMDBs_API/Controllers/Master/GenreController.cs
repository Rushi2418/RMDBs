using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMDBs_API.Model;
using RMDBs_API.Model.DTO;
using RMDBs_API.Repositories;
using System.Net;

namespace RMDBs_API.Controllers.Master
{
    [Route("api/Genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenericRepository<Genre> _genreRepository;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public GenreController(IGenericRepository<Genre> genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
            _response = new APIResponse();
        }

        // Get all genres
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> GetGenres()
        {
            var genres = await _genreRepository.FindAsync(genre => genre.ActiveFlag == true);

            if (!genres.Any())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "No active genres found." };
                _response.statusCode = HttpStatusCode.NoContent;
                return NoContent();
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<IEnumerable<GenreDTO>>(genres);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // Get genre by ID
        [HttpGet("{id:int}", Name = "GetGenre")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetGenre(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null || !genre.ActiveFlag)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Genre not found or inactive." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<GenreDTO>(genre);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // Create a new genre
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateGenre([FromBody] GenreCreateDTO genreDTO)
        {
            if (genreDTO == null || !ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            // Check if the genre name already exists
            var existingGenre = await _genreRepository.FindAsync(genre => genre.Name == genreDTO.Name);
            if (existingGenre.Any())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Genre with the same name already exists." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var genre = _mapper.Map<Genre>(genreDTO);
            await _genreRepository.AddAsync(genre);

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<GenreDTO>(genre);
            _response.statusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetGenre", new { id = genre.ID }, _response);
        }

        // Update an existing genre
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreUpdateDTO genreDTO)
        {
            if (id <= 0 || genreDTO == null || id != genreDTO.ID)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data or ID mismatch." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var existingGenre = await _genreRepository.GetByIdAsync(id);
            if (existingGenre == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Genre not found." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _mapper.Map(genreDTO, existingGenre);
            await _genreRepository.UpdateAsync(existingGenre);

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }

        // Delete a genre
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null || !genre.ActiveFlag)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Genre not found or already inactive." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            genre.ActiveFlag = false;
            await _genreRepository.UpdateAsync(genre);

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }
    }
}
