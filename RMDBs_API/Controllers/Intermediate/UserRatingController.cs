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
    [Route("api/UserRating")]
    public class UserRatingController : ControllerBase
    {
        private readonly IGenericRepository2<UserRating> _userRatingRepository;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public UserRatingController(IGenericRepository2<UserRating> userRatingRepository, IMapper mapper)
        {
            _userRatingRepository = userRatingRepository;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserRatings()
        {
            try
            {
                var userRatings = await _userRatingRepository.GetAllAsync(
                    include: query => query.Include(u => u.User)
                );

                if (!userRatings.Any())
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "No user ratings found." };
                    _response.statusCode = HttpStatusCode.NoContent;
                    return NoContent();
                }

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<IEnumerable<UserRatingDTO>>(userRatings);
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
        public async Task<IActionResult> GetUserRatingById(int id)
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
                var userRating = await _userRatingRepository.GetByIdAsync(id, include: query => query.Include(u => u.User));

                if (userRating == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "User rating not found." };
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<UserRatingDTO>(userRating);
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
        public async Task<IActionResult> CreateUserRating([FromBody] UserRatingCreateDTO userRatingCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                var userRating = _mapper.Map<UserRating>(userRatingCreateDTO);
                await _userRatingRepository.AddAsync(userRating);

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<UserRatingDTO>(userRating);
                _response.statusCode = HttpStatusCode.Created;
                return CreatedAtAction(nameof(GetUserRatingById), new { id = userRating.ID }, _response);
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
        public async Task<IActionResult> UpdateUserRating(int id, [FromBody] UserRatingUpdateDTO userRatingUpdateDTO)
        {
            if (id <= 0 || id != userRatingUpdateDTO.ID)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data or ID mismatch." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                var userRating = await _userRatingRepository.GetByIdAsync(id, include: query => query.Where(c => c.ID == id));
                if (userRating == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "User rating not found." };
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _mapper.Map(userRatingUpdateDTO, userRating);
                await _userRatingRepository.UpdateAsync(userRating);

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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUserRating(int id)
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
                var userRating = await _userRatingRepository.GetByIdAsync(id, include: query => query.Where(c => c.ID == id));
                if (userRating == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "User rating not found." };
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _userRatingRepository.DeleteAsync(id);

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
