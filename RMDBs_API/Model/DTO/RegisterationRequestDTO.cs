using System.Numerics;

namespace RMDBs_API.Model.DTO
{
    public class RegisterationRequestDTO
    {
        public string Email{ get; set;}
        public string Name{ get; set;}
        public string UserName{ get; set;}
        public string Password{ get; set;}
        public long Mobilenumber{ get; set;}
        public DateTime? DateJoined { get; set; } = DateTime.UtcNow;
        public string ProfilePicture { get; set; }
        public string Address { get; set; }


        public string Role{ get; set;}

    }
}
