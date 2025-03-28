using System;

namespace RMDBs_API.Model.DTO
{
    public class MovieDetailsWithRatings
    {
        public int MovieID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal AVGrating { get; set; }
        public string FilePath { get; set; } 
        public string TrailerUrl { get; set; } 
        public DateTime ReleaseDate {  get; set; }  


    }
}
