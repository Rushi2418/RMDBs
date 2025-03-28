using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMDBs_API.Data;
using RMDBs_API.Model;
using RMDBs_API.Model.DTO;
using RMDBs_Web.Models.ViewModel;
using System.Net;
using MovieCastDTO1 = RMDBs_API.Model.DTO.MovieCastDTO1;
using MovieDetailsWithRatings = RMDBs_API.Model.DTO.MovieDetailsWithRatings;

namespace RMDBs_API.Controllers
{
    ///v1
    [Route("api/MovieDetails")]
    [ApiController]
    public class GetDetailsByIdController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly APIResponse _response;

        public GetDetailsByIdController(ApplicationDbContext context)
        {
            _context = context;
            _response = new APIResponse();
        }
        [HttpGet("GetRatings")]
        public async Task<IActionResult> GetRatings()
        {
            // Get movies and calculate the average rating per movie
            var moviesWithRatings = await _context.UserRatings
                .AsNoTracking()
                .Include(ur => ur.Movie)
                .GroupBy(ur => ur.Movie)
                .Select(group => new
                {
                    Movie = group.Key,
                    AVGrating = group.Average(ur => ur.Rating)
                })
                .ToListAsync();

            // Get movie images
            var movieImages = await _context.MovieMedias1.ToListAsync();

            // Get movie genres (including Genre and Movie navigation properties)
            var movieGenres = await _context.MovieGenres
                .Include(mg => mg.Genre)
                .Include(mg => mg.Movie)
                .ToListAsync();

            if (moviesWithRatings == null || !moviesWithRatings.Any())
            {
                return NotFound(new APIResponse
                {
                    IsSuccess = false,
                    statusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "No movies with ratings found." }
                });
            }

            // Map moviesWithRatings to MovieDTOWithAVGRating
            var moviesList = moviesWithRatings.Select(movieWithRating => new MovieDetailsWithRatings
            {
                MovieID = movieWithRating.Movie.ID,
                Name = movieWithRating.Movie.Name,
                Description = movieWithRating.Movie.Description,
                AVGrating = (decimal)movieWithRating.AVGrating,
                ReleaseDate = movieWithRating.Movie.ReleaseDate,
                TrailerUrl = movieImages
                    .Where(mi => mi.MovieID == movieWithRating.Movie.ID && mi.DefaultFlag && mi.MovieTrailer)
                    .Select(mi => mi.FilePath)
                    .FirstOrDefault(),
                FilePath = movieImages
                    .Where(mi => mi.MovieID == movieWithRating.Movie.ID && mi.DefaultFlag && mi.MovieImgFlag)
                    .Select(mi => mi.FilePath)
                    .FirstOrDefault()
            }).ToList();

            // Movies Sorted by Rating (descending order)
            var moviesByRating = moviesList
                .OrderByDescending(m => m.AVGrating)
                .ToList();

            // Movies Grouped by Genre
            var moviesByGenre = movieGenres

                .GroupBy(mg => new { mg.Genre.ID, mg.Genre.Name })
                .Select(group => new MoviesByGenreDTO
                {
                    id = group.Key.ID,
                    Genre = group.Key.Name,
                    TopMovies = group.Select(mg => new MovieDetailsWithRatings
                    {
                        MovieID = mg.Movie.ID,
                        Name = mg.Movie.Name,
                        Description = mg.Movie.Description,
                        // Use the rating from moviesList if available, otherwise default to 0
                        AVGrating = moviesList.FirstOrDefault(m => m.MovieID == mg.Movie.ID)?.AVGrating ?? 0,
                        ReleaseDate = mg.Movie.ReleaseDate,
                        TrailerUrl = movieImages
                            .Where(mi => mi.MovieID == mg.Movie.ID && mi.DefaultFlag && mi.MovieTrailer)
                            .Select(mi => mi.FilePath)
                            .FirstOrDefault(),
                        FilePath = movieImages
                            .Where(mi => mi.MovieID == mg.Movie.ID && mi.DefaultFlag && mi.MovieImgFlag)
                            .Select(mi => mi.FilePath)
                            .FirstOrDefault()
                    }).ToList()
                })
                .ToList();

            // Map moviesList and moviesByRating to the view model type MovieDetailsWithRatings
            var moviesWithRatingsVm = moviesList.Select(m => new MovieDetailsWithRatings
            {
                MovieID = m.MovieID,
                Name = m.Name,
                Description = m.Description,
                AVGrating = m.AVGrating,
                FilePath = m.FilePath,
                TrailerUrl = m.TrailerUrl,
                ReleaseDate = m.ReleaseDate
            }).ToList();

            var moviesByRatingVm = moviesByRating.Select(m => new MovieDetailsWithRatings
            {
                MovieID = m.MovieID,
                Name = m.Name,
                Description = m.Description,
                AVGrating = m.AVGrating,
                FilePath = m.FilePath,
                TrailerUrl = m.TrailerUrl,
                ReleaseDate = m.ReleaseDate
            }).ToList();

            // Create the overall response DTO. Note: if your view expects the view model types,
            // you might need to map MoviesByGenre as well. For now, we assume MoviesByGenre is already mapped.
            var responseDto = new MoviesResponseDTO
            {
                MoviesWithRatings = moviesWithRatingsVm,
                MoviesByRating = moviesByRatingVm,
                MoviesByGenre = moviesByGenre // Ensure MoviesByGenreDTO matches your view model
            };

            return Ok(new APIResponse
            {
                IsSuccess = true,
                statusCode = HttpStatusCode.OK,
                Result = responseDto
            });
        }


        [HttpGet("GetMovieDetails/{id}")]
        public async Task<IActionResult> GetMovieDetails(int id)
        {
            var movie = await _context.Movies
                .Where(m => m.ID == id)
                .Select(m => new MovieDetailsDTO
                {
                    MovieID = m.ID,
                    MovieName = m.Name,
                    MovieReleaseDate = m.ReleaseDate,
                    MovieLanguage = m.Language,
                    MovieBudget = m.Budget,
                    MovieDescription = m.Description,

                    // Fetch default movie image
                    MovieImage = _context.MovieMedias1
                        .Where(mm => mm.MovieID == m.ID && mm.DefaultFlag && mm.MovieImgFlag)
                        .Select(mm => mm.FilePath)
                        .FirstOrDefault(),

                    // Fetch default movie trailer
                    MovieTrailer = _context.MovieMedias1
                        .Where(mm => mm.MovieID == m.ID && mm.DefaultFlag && mm.MovieTrailer)
                        .Select(mm => mm.FilePath)
                        .FirstOrDefault(),

                    // Handle nullable SortOrder properly
                    SortOrder = _context.MovieMedias1
                        .Where(mm => mm.MovieID == m.ID)
                        .Select(mm => (int?)mm.SortOrder)
                        .FirstOrDefault() ?? 0,

                    // Handle possible null rating
                    MovieRating = _context.UserRatings
                        .Where(ur => ur.MovieID == m.ID)
                        .Average(ur => (double?)ur.Rating) ?? 0,

                    // Fetch movie review
                    MovieReview = _context.UserRatings
                        .Where(ur => ur.MovieID == m.ID)
                        .Select(ur => ur.ReviewText)
                        .FirstOrDefault(),

                    // Fetch movie awards
                   

                    AwardsDetail = _context.ActorMovieAwards
                            .Where(ma => ma.MovieID == m.ID)
                            .Select(ma => new ActorMovieAwardDTO1
                            {
                                ID = ma.ID,
                                ActorID = ma.ActorID,
                                MovieID = ma.MovieID,
                                AwardID = ma.AwardID,
                                AwardName = ma.Award.Name,
                                AwardCategoryID = ma.AwardCategoryID,
                                Year = ma.Year,
                                AwardDiscription = ma.AwardDiscription
                            })
                                         .ToList(),


                    // Fetch movie cast members
                    MovieCasts = _context.MovieCasts
                        .Where(mc => mc.MovieID == m.ID)
                        .Select(mc => new MovieCastDTO1
                        {
                            ActorID = mc.ActorID,
                            ActorName = mc.Actor.Name,
                            Role = mc.Role,
                            PositionName = mc.Position.Name,
                            Img = _context.MovieMedias1
                                .Where(mm => mm.ActorImgFlag && mm.ActroId == mc.ActorID)
                                .Select(mm => mm.FilePath)
                                .FirstOrDefault()
                        }).ToList(),

                    // Fetch movie genres
                    MovieGenre = _context.MovieGenres
                        .Where(mg => mg.MovieID == m.ID)
                        .Select(mg => new MovieGenreDTO1
                        {
                            id = mg.Genre.ID,
                            GenreName = mg.Genre.Name
                        }).ToList(),

                    Directors = _context.MovieCasts
                        .Where(mc => mc.MovieID == m.ID && mc.Position.Name == "Director")
                        .Select(mc => new MovieDirectorDTO
                        {
                            ActorID = mc.ActorID,
                            ActorName = mc.Actor.Name,
                            PositionName = mc.Position.Name,

                        }).Take(2).ToList(),

                    Writers = _context.MovieCasts
                        .Where(mc => mc.MovieID == m.ID && mc.Position.Name == "Writer")
                        .Select(mc => new MovieWriterDTO
                        {
                            ActorID = mc.ActorID,
                            ActorName = mc.Actor.Name,
                            PositionName = mc.Position.Name,

                        }).Take(2).ToList(),
                    MovieIMGs = _context.MovieMedias1
                        .Where(mm => mm.MovieID == m.ID
                                 && mm.DefaultFlag == false
                                 && mm.MovieImgFlag == true
                                 && mm.ActiveFlag == true)
                        .Select(mm => new MovieIMG
                        {
                            ID = mm.ID,
                            MovieID = mm.MovieID,
                            FilePath = mm.FilePath,
                            SortOrder = mm.SortOrder
                        }).ToList()


                })
                .FirstOrDefaultAsync();

            // Return NotFound response if movie is not found
            if (movie == null)
            {
                return NotFound(new APIResponse
                {
                    IsSuccess = false,
                    statusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "Movie not found." }
                });
            }

            // Return successful API response
            return Ok(new APIResponse
            {
                IsSuccess = true,
                statusCode = HttpStatusCode.OK,
                Result = movie
            });
        }
        [HttpGet("GetMoviesByGenre/{genreId}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId)
        {
            // Fetch movies belonging to the specified genre
            var moviesByGenre = await _context.MovieGenres
                .Where(mg => mg.GenreID == genreId)
                .Select(mg => new MovieDetailsWithRatings
                {
                    MovieID = mg.Movie.ID,
                    Name = mg.Movie.Name,
                    Description = mg.Movie.Description,
                    AVGrating = (decimal)(_context.UserRatings
                        .Where(ur => ur.MovieID == mg.Movie.ID)
                        .Average(ur => (double?)ur.Rating) ?? 0), // Calculate average rating

                    ReleaseDate = mg.Movie.ReleaseDate,
                    TrailerUrl = _context.MovieMedias1
                        .Where(mm => mm.MovieID == mg.Movie.ID && mm.DefaultFlag && mm.MovieTrailer)
                        .Select(mm => mm.FilePath)
                        .FirstOrDefault(),

                    FilePath = _context.MovieMedias1
                    .Where(mm => mm.MovieID == mg.Movie.ID && mm.DefaultFlag && mm.MovieImgFlag)
                    .Select(mm => mm.FilePath)
                    .FirstOrDefault()
                })
                .ToListAsync();

            // If no movies are found for the given genre, return NotFound
            if (moviesByGenre == null || !moviesByGenre.Any())
            {
                return NotFound(new APIResponse
                {
                    IsSuccess = false,
                    statusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new List<string> { "No movies found for the selected genre." }
                });
            }

            // Return the movies in an API response
            return Ok(new APIResponse
            {
                IsSuccess = true,
                statusCode = HttpStatusCode.OK,
                Result = moviesByGenre
            });
        }




    }
}

