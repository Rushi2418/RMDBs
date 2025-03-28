using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMDBs_API.Model;
using RMDBs_API.Model.DTO;
using RMDBs_API.Repositories;
using System.Net;

namespace RMDBs_API.Controllers
{
    [ApiController]
    [Route("api/Popularity")]
    public class PopularityController : ControllerBase
    {
        private readonly IGenericRepository2<Popularity> _popularityRepository;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public PopularityController(IGenericRepository2<Popularity> popularityRepository, IMapper mapper)
        {
            _popularityRepository = popularityRepository;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPopularity()
        {
            try
            {
                var popularities = await _popularityRepository.GetAllAsync(
                    include: qu => qu.Include(qu => qu.Movie)
                );

                if (!popularities.Any())
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "No active popularity records found." };
                    _response.statusCode = HttpStatusCode.NoContent;
                    return NoContent();
                }

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<IEnumerable<PopularityDTO>>(popularities);
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPopularityById(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                var popularity = await _popularityRepository.GetByIdAsync(id,
                                        include: qu => qu.Include(qu => qu.Movie).Where(c => c.ID == id)
                );

                if (popularity == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Popularity record not found." };
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<PopularityDTO>(popularity);
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

        [HttpPost]
        public async Task<IActionResult> CreatePopularity([FromBody] PopularityCreateDTO popularityCreateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var popularity = _mapper.Map<Popularity>(popularityCreateDTO);

                if (popularity == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Error on Movie ID." };
                }
                await _popularityRepository.AddAsync(popularity);

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<PopularityDTO>(popularity);
                _response.statusCode = HttpStatusCode.Created;
                return CreatedAtAction(nameof(GetPopularityById), new { id = popularity.ID }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.statusCode = HttpStatusCode.InternalServerError;
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePopularity(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                var popularity = await _popularityRepository.GetByIdAsync(id);

                if (popularity == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Popularity record not found." };
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _popularityRepository.DeleteAsync(id);

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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePopularity(int id, [FromBody] PopularityUpdateDTO popularityUpdateDTO)
        {
            if (popularityUpdateDTO == null || id <= 0 || id != popularityUpdateDTO.ID)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data or ID mismatch." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                var popularity = await _popularityRepository.GetByIdAsync(id);

                if (popularity == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Popularity record not found." };
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _mapper.Map(popularityUpdateDTO, popularity);
                await _popularityRepository.UpdateAsync(popularity);

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
