using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMDBs_API.Model;
using RMDBs_API.Model.DTO;
using RMDBs_API.Repositories;
using System.Net;

namespace RMDBs_API.Controllers.Master
{
    [Route("api/Position")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IGenericRepository<Position> _positionRepository;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public PositionController(IGenericRepository<Position> positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
            _response = new APIResponse();
        }

        // Get all positions
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> GetPositions()
        {
            var positions = await _positionRepository.FindAsync(position => position.ActiveFlag == true);

            if (!positions.Any())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "No active positions found." };
                _response.statusCode = HttpStatusCode.NoContent;
                return NoContent();
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<IEnumerable<PositionDTO>>(positions);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // Get position by ID
        [HttpGet("{id:int}", Name = "GetPosition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetPosition(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var position = await _positionRepository.GetByIdAsync(id);
            if (position == null || !position.ActiveFlag)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Position not found or inactive." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<PositionDTO>(position);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // Create a new position
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreatePosition([FromBody] PositionCreateDTO positionDTO)
        {
            if (positionDTO == null || !ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            // Check if the position name already exists
            var existingPosition = await _positionRepository.FindAsync(position => position.Name == positionDTO.Name);
            if (existingPosition.Any())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Position with the same name already exists." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var position = _mapper.Map<Position>(positionDTO);
            await _positionRepository.AddAsync(position);

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<PositionDTO>(position);
            _response.statusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetPosition", new { id = position.ID }, _response);
        }

        // Update an existing position
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePosition(int id, [FromBody] PositionUpdateDTO positionDTO)
        {
            if (id <= 0 || positionDTO == null || id != positionDTO.ID)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data or ID mismatch." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var existingPosition = await _positionRepository.GetByIdAsync(id);
            if (existingPosition == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Position not found." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _mapper.Map(positionDTO, existingPosition);
            await _positionRepository.UpdateAsync(existingPosition);

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }

        // Delete a position
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePosition(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var position = await _positionRepository.GetByIdAsync(id);
            if (position == null || !position.ActiveFlag)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Position not found or already inactive." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            position.ActiveFlag = false;
            await _positionRepository.UpdateAsync(position);

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }
    }
}
