using RMDBs_API.Model;
using RMDBs_API.Model.DTO;

namespace RMDBs_API.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUserUnique(string email);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<User> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
