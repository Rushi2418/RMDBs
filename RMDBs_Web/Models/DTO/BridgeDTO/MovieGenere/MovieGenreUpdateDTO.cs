namespace RMDBs_Web.Models.DTO
{
    public class MovieGenreUpdateDTO
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int GenreID { get; set; }
        public bool ActiveFlag { get; set; } = true;
    }
}
