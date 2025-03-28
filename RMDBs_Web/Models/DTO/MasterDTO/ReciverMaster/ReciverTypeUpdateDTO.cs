namespace RMDBs_Web.Models.DTO
{
    public class ReciverTypeUpdateDTO
    {
        public int Id { get; set; }
        public required string Type { get; set; }
        public bool ActiveFlag {  get; set; }
    }
}
