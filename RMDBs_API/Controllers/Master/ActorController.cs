using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMDBs_API.Model;
using RMDBs_API.Model.DTO;
using RMDBs_API.Repositories;
using System.Net;

namespace RMDBs_API.Controllers.Master
{

    [Route("api/Actor")]
    [ApiController]

    public class ActorController : ControllerBase
    {
        private readonly IGenericRepository<Actor> _actorRepository;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public ActorController(IGenericRepository<Actor> actorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
            _response = new APIResponse();
        }

        // Get all actors
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> GetActors()
        {
            var actors = await _actorRepository.FindAsync(actor => actor.ActiveFlag == true);

            if (!actors.Any())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "No active actors found." };
                _response.statusCode = HttpStatusCode.NoContent;
                return NoContent();
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<IEnumerable<ActorDTO>>(actors);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // Get actor by ID
        [HttpGet("{id:int}", Name = "GetActor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetActor(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var actor = await _actorRepository.GetByIdAsync(id);
            if (actor == null || !actor.ActiveFlag)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Actor not found or inactive." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<ActorDTO>(actor);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // Create a new actor
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateActor([FromBody] CreateActor actorDTO)
        {
            if (actorDTO == null || !ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            // Check for duplicate actor name
            var existingActor = await _actorRepository.FindAsync(actor => actor.Name == actorDTO.Name);
            if (existingActor.Any())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Actor with the same name already exists." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var actor = _mapper.Map<Actor>(actorDTO);
            await _actorRepository.AddAsync(actor);

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<ActorDTO>(actor);
            _response.statusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetActor", new { id = actor.ID }, _response);
        }

        // Update an existing actor
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateActor(int id, [FromBody] UpdatetActor actorDTO)
        {
            if (id <= 0 || actorDTO == null || id != actorDTO.ID)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data or ID mismatch." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var existingActor = await _actorRepository.GetByIdAsync(id);
            if (existingActor == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Actor not found." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _mapper.Map(actorDTO, existingActor);
            await _actorRepository.UpdateAsync(existingActor);

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }

        // Delete an actor
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteActor(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var actor = await _actorRepository.GetByIdAsync(id);
            if (actor == null || !actor.ActiveFlag)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Actor not found or already inactive." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            actor.ActiveFlag = false;
            await _actorRepository.UpdateAsync(actor);

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }
    }
}
