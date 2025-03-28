using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RMDBs_Web.Models.ViewModel;

namespace RMDBs_API.Model.DTO
{
    public class GenreMoviesViewModel
    {
        public int GenreID { get; set; }
        public string GenreName { get; set; }
        public List<MovieDetailsWithRatings> TopMovies { get; set; }
    }

}
