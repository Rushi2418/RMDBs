using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMDBs_API.Model.DTO;
using RMDBs_API.Model;
using RMDBs_API.Data.Repositories;
using RMDBs_API.Repositories;
using System.Net;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/MovieGenre")]
//[Route("api/v1/MovieGenre")]
public class MovieGenreController : ControllerBase
{
    private readonly IGenericRepository3<MovieGenre> _repository;
    private readonly IMapper _mapper;

    public MovieGenreController(IGenericRepository3<MovieGenre> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMovieGenre()
    {
        var response = new APIResponse();
        try
        {
            var movieGenres = await _repository.GetAllAsync(include: query => query
                                               .Include(c => c.Movie)
                                               .Include(c => c.Genre));
            response.Result = _mapper.Map<IEnumerable<MovieGenreDTO>>(movieGenres);
            response.statusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add(ex.Message);
            response.statusCode = HttpStatusCode.InternalServerError;
        }
        return StatusCode((int)response.statusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieGenreById(int id)
    {
        var response = new APIResponse();
        try
        {
            var movieGenre = await _repository.GetByIdAsync(id,include: query => query
                                               .Include(c=>c.Movie)
                                               .Include(c=>c.Genre));
            if (movieGenre == null)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add("MovieGenre not found.");
                response.statusCode = HttpStatusCode.NotFound;
                return StatusCode((int)response.statusCode, response);
            }

            response.Result = _mapper.Map<MovieGenreDTO>(movieGenre);
            response.statusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add(ex.Message);
            response.statusCode = HttpStatusCode.InternalServerError;
        }
        return StatusCode((int)response.statusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovieGenre([FromBody] MovieGenreCreateDTO movieGenreCreateDTO)
    {
        var response = new APIResponse();
        try
        {
            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.ErrorMessages.AddRange(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)));
                response.statusCode = HttpStatusCode.BadRequest;
                return StatusCode((int)response.statusCode, response);
            }

            var movieGenre = _mapper.Map<MovieGenre>(movieGenreCreateDTO);
            await _repository.AddAsync(movieGenre);

            response.Result = _mapper.Map<MovieGenreDTO>(movieGenre);
            response.statusCode = HttpStatusCode.Created;
            response.IsSuccess = true;
            return CreatedAtAction(nameof(GetMovieGenreById), new { id = movieGenre.ID }, response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add(ex.Message);
            response.statusCode = HttpStatusCode.InternalServerError;
        }
        return StatusCode((int)response.statusCode, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovieGenre(int id, [FromBody] MovieGenreUpdateDTO movieGenreUpdateDTO)
    {
        var response = new APIResponse();
        try
        {
            if (!ModelState.IsValid)
            {
                response.IsSuccess = false;
                response.ErrorMessages.AddRange(ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)));
                response.statusCode = HttpStatusCode.BadRequest;
                return StatusCode((int)response.statusCode, response);
            }

            if (id != movieGenreUpdateDTO.ID)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add("ID mismatch.");
                response.statusCode = HttpStatusCode.BadRequest;
                return StatusCode((int)response.statusCode, response);
            }

            var movieGenre = await _repository.GetByIdAsync(id, include: query => query
                                               .Include(c => c.Movie)
                                               .Include(c => c.Movie));
            if (movieGenre == null)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add("MovieGenre not found.");
                response.statusCode = HttpStatusCode.NotFound;
                return StatusCode((int)response.statusCode, response);
            }

            _mapper.Map(movieGenreUpdateDTO, movieGenre);
            await _repository.UpdateAsync(movieGenre);

            response.statusCode = HttpStatusCode.NoContent;
            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add(ex.Message);
            response.statusCode = HttpStatusCode.InternalServerError;
        }
        return StatusCode((int)response.statusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovieGenre(int id)
    {
        var response = new APIResponse();
        try
        {
            var movieGenre = await _repository.GetByIdAsync(id, include: query => query
                                               .Include(c => c.Movie)
                                               .Include(c => c.Movie));
            if (movieGenre == null)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add("MovieGenre not found.");
                response.statusCode = HttpStatusCode.NotFound;
                return StatusCode((int)response.statusCode, response);
            }

            await _repository.DeleteAsync(id);

            response.statusCode = HttpStatusCode.NoContent;
            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.ErrorMessages.Add(ex.Message);
            response.statusCode = HttpStatusCode.InternalServerError;
        }
        return StatusCode((int)response.statusCode, response);
    }
}
