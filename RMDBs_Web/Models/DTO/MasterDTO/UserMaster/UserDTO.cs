using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class UserDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(255)]
        public required string Name { get; set; }

        [MaxLength(255)]
        public required string Email { get; set; }

        [MaxLength(255)]
        public required string Password { get; set; }

        public DateTime? DateJoined { get; set; }

        public string? ProfilePicture { get; set; }

        public long? MobileNumber { get; set; }

        public required string Address { get; set; }

        public bool ActiveFlag { get; set; } = true;

    }
}
