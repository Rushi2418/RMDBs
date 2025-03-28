using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMDBs_API.Model;
using RMDBs_API.Model.DTO;
using RMDBs_API.Repositories;
using System.Net;

namespace RMDBs_API.Controllers.Master
{
    [Route("api/ReciverType")]
    [ApiController]
    public class ReciverTypeController : ControllerBase
    {
        private readonly IGenericRepository<ReceiverType> _reciverTypeRepository;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public ReciverTypeController(IGenericRepository<ReceiverType> reciverTypeRepository, IMapper mapper)
        {
            _reciverTypeRepository = reciverTypeRepository;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> GetReciverTypes()
        {
            var types = await _reciverTypeRepository.FindAsync(type => type.ActiveFlag == true);

            if (!types.Any())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "No active Reciver Types found." };
                _response.statusCode = HttpStatusCode.NoContent;
                return NoContent();
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<IEnumerable<ReciverTypeDTO>>(types);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("{id:int}", Name = "GetReciverType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetReciverType(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var type = await _reciverTypeRepository.GetByIdAsync(id);
            if (type == null || !type.ActiveFlag)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Reciver Type not found or inactive." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<ReciverTypeDTO>(type);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateReciverType([FromBody] ReciverTypeCreateDTO reciverTypeDTO)
        {
            if (reciverTypeDTO == null || !ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var reciverType = _mapper.Map<ReceiverType>(reciverTypeDTO);
            await _reciverTypeRepository.AddAsync(reciverType);

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<ReciverTypeDTO>(reciverType);
            _response.statusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetReciverType", new { id = reciverType.Id }, _response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateReciverType(int id, [FromBody] ReciverTypeUpdateDTO reciverTypeDTO)
        {
            if (id <= 0 || reciverTypeDTO == null || id != reciverTypeDTO.Id)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data or ID mismatch." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var existingReciverType = await _reciverTypeRepository.GetByIdAsync(id);
            if (existingReciverType == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Reciver Type not found." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _mapper.Map(reciverTypeDTO, existingReciverType);
            await _reciverTypeRepository.UpdateAsync(existingReciverType);

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReciverType(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var reciverType = await _reciverTypeRepository.GetByIdAsync(id);
            if (reciverType == null || !reciverType.ActiveFlag)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Reciver Type not found or already inactive." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            reciverType.ActiveFlag = false;
            await _reciverTypeRepository.UpdateAsync(reciverType);

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }
    }
}