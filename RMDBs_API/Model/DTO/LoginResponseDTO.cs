using RMDBs_Web.Models.DTO;

namespace RMDBs_API.Model.DTO
{
    public class LoginResponseDTO
    {
        public User User{ get; set;}
        public string Token{ get; set;}
    }
}
