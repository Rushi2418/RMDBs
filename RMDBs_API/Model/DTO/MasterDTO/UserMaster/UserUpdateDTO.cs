namespace RMDBs_API.Model.DTO
{
    public class UserUpdateDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? ProfilePicture { get; set; }
        public long? MobileNumber { get; set; }
        public string Address { get; set; }
        public bool ActiveFlag { get; set; }
    }
}
