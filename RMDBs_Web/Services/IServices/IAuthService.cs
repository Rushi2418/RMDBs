using RMDBs_Web.Models.DTO;
using RMDBs_Web.Models;

namespace RMDBs_Web.Services.IServices
{
    public interface IAuthService
    {
        Task<APIResponse<LoginResponseDTO>> Login(LoginRequestDTO model);
        Task<APIResponse<UserDTO>> Register(RegisterationRequestDTO model);
    }
}
