using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        public DateTime? DateJoined { get; set; }

        public string ProfilePicture { get; set; }

        public long? MobileNumber { get; set; }

        public string Address { get; set; }
        public string Role { get; set; }

        public bool ActiveFlag { get; set; } = true;

    }
}
