using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMDBs_API.Model;
using RMDBs_API.Model.DTO;
using RMDBs_API.Repositories;
using System.Net;

namespace RMDBs_API.Controllers.Master
{
    [Route("api/AwardCategory")]
    [ApiController]
    public class AwardCategoryController : ControllerBase
    {
        private readonly IGenericRepository<AwardCategory> _awardCategoryRepository;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public AwardCategoryController(IGenericRepository<AwardCategory> awardCategoryRepository, IMapper mapper)
        {
            _awardCategoryRepository = awardCategoryRepository;
            _mapper = mapper;
            _response = new APIResponse();
        }

        // Get all award categories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> GetAwardCategories()
        {
            var categories = await _awardCategoryRepository.FindAsync(category => category.ActiveFlag == true);

            if (!categories.Any())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "No active award categories found." };
                _response.statusCode = HttpStatusCode.NoContent;
                return NoContent();
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<IEnumerable<AwardCategoryDTO>>(categories);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // Get award category by ID
        [HttpGet("{id:int}", Name = "GetAwardCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAwardCategory(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var category = await _awardCategoryRepository.GetByIdAsync(id);
            if (category == null || !category.ActiveFlag)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Award category not found or inactive." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<AwardCategoryDTO>(category);
            _response.statusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // Create a new award category
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateAwardCategory([FromBody] AwardCategoryCreateDTO categoryDTO)
        {
            if (categoryDTO == null || !ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            // Check for duplicate category name
            var existingCategory = await _awardCategoryRepository.FindAsync(category => category.Name == categoryDTO.Name);
            if (existingCategory.Any())
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Award category with the same name already exists." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var category = _mapper.Map<AwardCategory>(categoryDTO);
            await _awardCategoryRepository.AddAsync(category);

            _response.IsSuccess = true;
            _response.Result = _mapper.Map<AwardCategoryDTO>(category);
            _response.statusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetAwardCategory", new { id = category.ID}, _response);
        }

        // Update an existing award category
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAwardCategory(int id, [FromBody] AwardCategoryUpdateDTO categoryDTO)
        {
            if (id <= 0 || categoryDTO == null || id != categoryDTO.ID)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid input data or ID mismatch." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var existingCategory = await _awardCategoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Award category not found." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _mapper.Map(categoryDTO, existingCategory);
            await _awardCategoryRepository.UpdateAsync(existingCategory);

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }

        // Delete an award category
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAwardCategory(int id)
        {
            if (id <= 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid ID. ID must be greater than 0." };
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var category = await _awardCategoryRepository.GetByIdAsync(id);
            if (category == null || !category.ActiveFlag)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Award category not found or already inactive." };
                _response.statusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            category.ActiveFlag = false;
            await _awardCategoryRepository.UpdateAsync(category);

            _response.IsSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;
            return NoContent();
        }
    }
}
