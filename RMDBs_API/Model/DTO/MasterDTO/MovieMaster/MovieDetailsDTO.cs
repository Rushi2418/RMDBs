using RMDBs_API.Model.DTO;

public class MovieDetailsDTO
{
    // From Movie Table
    public int MovieID { get; set; }
    public string MovieName { get; set; }
    public DateTime? MovieReleaseDate { get; set; }
    public string? MovieLanguage { get; set; }
    public decimal? MovieBudget { get; set; }
    public string? MovieDescription { get; set; }
    public List<MovieIMG>? MovieIMGs { get; set; } = new List<MovieIMG>();


    // From Genre Table
    //public string? MovieGenre { get; set; }

    // From Movie Media
    public string? MovieImage { get; set; }
    public string? MovieTrailer { get; set; }
    public int? SortOrder { get; set; }


    // From User Rating
    public double? MovieRating { get; set; }
    public string? MovieReview { get; set; }

    // From Movie Actor Award
   
    public List<ActorMovieAwardDTO1>? AwardsDetail { get; set; } = new List<ActorMovieAwardDTO1>();


    // From Movie Cast (Multiple Cast Members)
    public List<MovieCastDTO1> MovieCasts { get; set; } = new List<MovieCastDTO1>();
    public List<MovieGenreDTO1> MovieGenre { get; set; } = new List<MovieGenreDTO1>();

    // New lists for Director and Writer
    public List<MovieDirectorDTO> Directors { get; set; } = new List<MovieDirectorDTO>();
    public List<MovieWriterDTO> Writers { get; set; } = new List<MovieWriterDTO>();
}
