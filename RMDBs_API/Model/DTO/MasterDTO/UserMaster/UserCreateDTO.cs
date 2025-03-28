using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.DTOs
{
    public class UserCreateDTO
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }  // Will be hashed before saving
        public string Role { get; set; }  // Will be hashed before saving

        public DateTime? DateJoined { get; set; } = DateTime.UtcNow;

        public string? ProfilePicture { get; set; }
        public long? MobileNumber { get; set; }
        public string? Address { get; set; }
    }
}
