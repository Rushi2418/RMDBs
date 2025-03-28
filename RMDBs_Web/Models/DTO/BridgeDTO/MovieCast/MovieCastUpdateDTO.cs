namespace RMDBs_Web.Models.DTO
{
    public class MovieCastUpdateDTO
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int ActorID { get; set; }
        public int PositionID { get; set; }
        public string Role { get; set; }
        public bool ActiveFlag { get; set; } = true;
    }
}
