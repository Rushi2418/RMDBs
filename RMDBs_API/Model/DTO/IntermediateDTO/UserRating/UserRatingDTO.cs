using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model.DTO
{
    public class UserRatingDTO
    {
       
        public int ID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public UserDTO User { get; set; }

        [ForeignKey("Movie")]
        public int MovieID { get; set; }
        public MovieDTO Movie { get; set; }

        [Column(TypeName = "decimal(3,2)")]

        public decimal Rating { get; set; }

        public DateTime RatingDate { get; set; }

        public string ReviewText { get; set; }

        [Column(TypeName = "decimal(3,2)")]

        public decimal DefaultRatingScore { get; set; } = 10.0m;

    }
}
