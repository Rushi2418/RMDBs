using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RMDBs_Web.Models;
using RMDBs_Web.Models.DTO;
using RMDBs_Web.Models.ViewModel;
using RMDBs_Web.Services.IServices;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using static RMDB_Utility.Class1;

namespace RMDBs_Web.Services
{
    public class AuthService : BaseServices, IAuthService
    {
        private readonly string _authUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpClientFactory httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            : base(httpClient)
        {
            _authUrl = configuration.GetValue<string>("ServiceUrls:RMDBAPI");
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<APIResponse<LoginResponseDTO>> Login(LoginRequestDTO model)
        {
            var apiRequest = new APIRequest
            {
                Url = $"{_authUrl}/api/Auth/login", 
                apiType = ApiType.POST,
                Data = model
            };

            var response = await SendAsync<LoginResponseDTO>(apiRequest);
            if (response != null && response.IsSuccess && response.Result != null)
            {
                _httpContextAccessor.HttpContext?.Session.SetString("JWTToken", response.Result.Token);
            }

            return response ?? new APIResponse<LoginResponseDTO>
            {
                statusCode = HttpStatusCode.InternalServerError,
                IsSuccess = false,
                ErrorMessages = new List<string> { "Login failed. Please try again." }
            };
        }

        public async Task<APIResponse<UserDTO>> Register(RegisterationRequestDTO model)
        {
            var apiRequest = new APIRequest
            {
                Url = $"{_authUrl}/api/Auth/registe",  
                apiType = ApiType.POST,
                Data = model
            };

            var response = await SendAsync<UserDTO>(apiRequest);

            return response ?? new APIResponse<UserDTO>
            {
                statusCode = HttpStatusCode.InternalServerError,
                IsSuccess = false,
                ErrorMessages = new List<string> { "Registration failed. Please try again." }
            };
        }
    }
}
