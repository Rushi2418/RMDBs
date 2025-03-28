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
    [Route("api/comment")]
        public class CommentController : ControllerBase
    {
        private readonly IGenericRepository2<Comment> _commentRepository;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public CommentController(IGenericRepository2<Comment> commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            try
            {
                var comments = await _commentRepository.GetAllAsync(
                    include: query => query.Include(c => c.Movie).Include(c => c.User).Where(c => c.ActiveFlag == true)
                );

                if (!comments.Any())
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "No active comments found." };
                    _response.statusCode = HttpStatusCode.NoContent;
                    return NoContent();
                }

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<IEnumerable<CommentDTO>>(comments);
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
        public async Task<IActionResult> GetCommentById(int id)
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
                var comment = await _commentRepository.GetByIdAsync(
                    id,
                    include: query => query.Where(c => c.ID == id)
                    .Include(c => c.Movie).Include(c => c.User).Where(c => c.ActiveFlag == true)
                );

                if (comment == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Comment not found." };
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<CommentDTO>(comment);
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
        public async Task<IActionResult> CreateComment([FromBody] CommentCreateDTO commentCreateDTO)
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
                var comment = _mapper.Map<Comment>(commentCreateDTO);
                await _commentRepository.AddAsync(comment);

                _response.IsSuccess = true;
                _response.Result = _mapper.Map<CommentDTO>(comment);
                _response.statusCode = HttpStatusCode.Created;
                return CreatedAtAction(nameof(GetCommentById), new { id = comment.ID }, _response);
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
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentUpdateDTO commentUpdateDTO)
        {
            if (id <= 0 || id != commentUpdateDTO.ID)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data or ID mismatch." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            try
            {
                var comment = await _commentRepository.GetByIdAsync(id, include: query => query.Where(c => c.ID == id));
                if (comment == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Comment not found." };
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _mapper.Map(commentUpdateDTO, comment);
                await _commentRepository.UpdateAsync(comment);

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
        public async Task<IActionResult> DeleteComment(int id)
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
                var comment = await _commentRepository.GetByIdAsync(id, include: query => query.Where(c => c.ID == id));
                if (comment == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Comment not found." };
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                comment.ActiveFlag = false;
                await _commentRepository.UpdateAsync(comment);
                var isSaved = await _commentRepository.SaveChangesAsync();

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
