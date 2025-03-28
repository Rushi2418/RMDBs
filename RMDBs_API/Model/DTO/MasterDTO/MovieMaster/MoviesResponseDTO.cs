using RMDBs_Web.Models.ViewModel;
using System.Collections.Generic;

namespace RMDBs_API.Model.DTO
{
    public class MoviesResponseDTO
    {
        public List<MovieDetailsWithRatings> MoviesWithRatings { get; set; }
        public List<MovieDetailsWithRatings> MoviesByRating { get; set; }
        public List<MoviesByGenreDTO> MoviesByGenre { get; set; }
    }
}
