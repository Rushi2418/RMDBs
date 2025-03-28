namespace RMDBs_Web.Models.DTO
{
    public class UserCreateDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public string Address { get; set; }
        public string? ProfilePicture { get; set; }
        public long? MobileNumber { get; set; }
        public bool ActiveFlag { get; set; } = true;
    }
}
