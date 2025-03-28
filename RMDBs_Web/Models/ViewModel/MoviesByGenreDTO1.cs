using RMDBs_Web.Models.ViewModel;
using System.Collections.Generic;

namespace RMDBs_Web.Models.DTO
{
    public class MoviesByGenreDTO1
    {

        public int id { get; set; }
        public string Genre { get; set; }
    public List<MovieDetailsWithRatings> TopMovies { get; set; } = new List<MovieDetailsWithRatings>();
    }
}
