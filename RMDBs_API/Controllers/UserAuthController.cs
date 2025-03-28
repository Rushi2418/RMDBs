using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using RMDBs_API.Model;
using RMDBs_API.Model.DTO;
using RMDBs_API.Repository.IRepository;
using System.Net;

namespace RMDBs_API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class UserAuthController : Controller
    { 
        private readonly IUserRepository _repository;
        protected APIResponse _response;

        public UserAuthController(IUserRepository repository)
        {
           _repository = repository;
            this._response = new APIResponse();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO model)
        {
            var LoginRsponse = await _repository.Login(model);
            if (LoginRsponse == null || string.IsNullOrEmpty(LoginRsponse.Token)) {
                
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>();
                _response.IsSuccess = false;
                return BadRequest(_response);
            
            }


            _response.statusCode = HttpStatusCode.OK;
            _response.Result = LoginRsponse;
            _response.IsSuccess = true ;
            return Ok(_response);
        }  
        
        
        [HttpPost("registe")]
        public async Task<IActionResult> Registe([FromBody]RegisterationRequestDTO model)
        {
            bool ifUsernameUnique = _repository.IsUserUnique(model.Email);
            if (!ifUsernameUnique)
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>();
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            var user = await _repository.Register(model);
            if (user == null) {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Error while Regidtering");
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            _response.statusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = user;
            return Ok(_response);
        }
    }
}
