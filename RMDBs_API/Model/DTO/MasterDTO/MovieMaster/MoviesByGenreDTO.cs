using System.Collections.Generic;

namespace RMDBs_API.Model.DTO
{
    public class MoviesByGenreDTO
    {

        public int id { get; set; }
        public string Genre { get; set; }
         public List<MovieDetailsWithRatings> TopMovies { get; set; } = new List<MovieDetailsWithRatings>();
    }
}
