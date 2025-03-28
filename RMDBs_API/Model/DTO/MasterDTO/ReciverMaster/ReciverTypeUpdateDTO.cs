namespace RMDBs_API.Model.DTO
{
    public class ReciverTypeUpdateDTO
    {
        public int Id { get; set; }
        public required string Type { get; set; }
        public bool ActiveFlag {  get; set; }
    }
}
