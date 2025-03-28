using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMDBs_API.Model
{
    public class UserRating
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int UserID { get; set; } 

        [ForeignKey("UserID")]
        public User User { get; set; } 

        [Required]
        public int MovieID { get; set; }

        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }

        public int Rating { get; set; }

        public DateTime RatingDate { get; set; } = DateTime.UtcNow;

        public string ReviewText { get; set; }

        public int DefaultRatingScore { get; set; }
    }
}
