using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMDBs_API.Model;
using RMDBs_API.Model.DTO;
using RMDBs_API.Repositories;
using System.Linq.Expressions;
using System.Net;

namespace RMDBs_API.Controllers.Master
{

    [Route("api/Award")]
    [ApiController]
    public class AwardController : ControllerBase
    {
        private readonly IGenericRepository<Award> _awardRepository;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public AwardController(IGenericRepository<Award> awardRepository, IMapper mapper)
        {
            _awardRepository = awardRepository;
            _mapper = mapper;
            _response = new APIResponse();
        }

        // Get all awards
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> GetAwards()
        {
            var awards = await _awardRepository.FindAsync(award => award.ActiveFlag == true);

            if (!awards.Any())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "No active awards found." };
                _response.statusCode = HttpStatusCode.NoContent;
                return NoContent();
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<IEnumerable<AwardDTO>>(awards);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // Get award by ID
        [HttpGet("{id:int}", Name = "GetAward")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAward(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var award = await _awardRepository.GetByIdAsync(id);
            if (award == null || !award.ActiveFlag)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Award not found or inactive." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<AwardDTO>(award);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // Create a new award
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> CreateAward([FromBody] AwardCreateDTO awardDTO)
        {
            try {
                if (awardDTO == null || !ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Invalid input data." };
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                // Check for duplicate award name
                var existingAward = await _awardRepository.FindAsync(award => award.Name == awardDTO.Name);
                if (existingAward.Any())
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Award with the same name already exists." };
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var award = _mapper.Map<Award>(awardDTO);
                await _awardRepository.AddAsync(award);

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<AwardDTO>(award);
                _response.statusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetAward", new { id = award.ID }, _response);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Award with the same name already exists."+e.Message };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            }

          [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAward(int id, [FromBody] AwardUpdateDTO awardDTO)
        {
            if (id <= 0 || awardDTO == null || id != awardDTO.ID)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data or ID mismatch." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var existingAward = await _awardRepository.GetByIdAsync(id);
            if (existingAward == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Award not found." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _mapper.Map(awardDTO, existingAward);
            await _awardRepository.UpdateAsync(existingAward);

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAward(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var award = await _awardRepository.GetByIdAsync(id);
            if (award == null || !award.ActiveFlag)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Award not found or already inactive." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            award.ActiveFlag = false;
            await _awardRepository.UpdateAsync(award);

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }
    }
}
