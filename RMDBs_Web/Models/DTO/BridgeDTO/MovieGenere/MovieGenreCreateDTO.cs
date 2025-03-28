namespace RMDBs_Web.Models.DTO
{
    public class MovieGenreCreateDTO
    {
        public int MovieID { get; set; }
        public int GenreID { get; set; }
        public bool ActiveFlag { get; set; } = true;
    }
}
