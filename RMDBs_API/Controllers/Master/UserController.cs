using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using RMDBs_API.DTOs;
using RMDBs_API.Model.DTO;
using RMDBs_API.Model;
using RMDBs_API.Data;

namespace RMDBs_API.Controllers.Master
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public UserController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _response = new APIResponse();
        }

        // Get all users
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            if (!users.Any())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "No users found." };
                _response.statusCode = HttpStatusCode.NoContent;
                return NoContent();
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<IEnumerable<UserDTO>>(users);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // Get a user by ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "User not found." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<UserDTO>(user);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // Register a new user
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> RegisterUser([FromBody] UserCreateDTO userDTO)
        {
            if (userDTO == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var user = _mapper.Map<User>(userDTO);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<UserDTO>(user);
            _response.statusCode = HttpStatusCode.Created;
            return CreatedAtAction(nameof(GetUser), new { id = user.ID }, _response);
        }

        // Update an existing user
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDTO userDTO)
        {
            if (userDTO == null || id != userDTO.ID)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data or ID mismatch." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "User not found." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _mapper.Map(userDTO, user);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }

        // Delete a user
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "User not found." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }
    }
}