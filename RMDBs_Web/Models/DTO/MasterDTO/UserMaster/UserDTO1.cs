using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class UserDTO1
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }
        public required string Password { get; set; }

        public DateTime? DateJoined { get; set; }

        public string? ProfilePicture { get; set; }
        public string Role { get; set; }

        public long? MobileNumber { get; set; }

        public required string Address { get; set; }

        public bool ActiveFlag { get; set; } = true;

    }
}
