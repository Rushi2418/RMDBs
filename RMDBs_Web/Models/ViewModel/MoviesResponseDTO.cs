using RMDBs_Web.Models.ViewModel;
using System.Collections.Generic;

namespace RMDBs_Web.Models.DTO
{
    public class MoviesResponseDTO
    {
        public List<MovieDetailsWithRatings> MoviesWithRatings { get; set; }
        public List<MovieDetailsWithRatings> MoviesByRating { get; set; }
        public List<MoviesByGenreDTO1> MoviesByGenre { get; set; }
    }
}
