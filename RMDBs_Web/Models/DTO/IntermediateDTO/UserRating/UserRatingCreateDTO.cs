using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class UserRatingCreateDTO
    {
        

        public int UserID { get; set; }

        public int MovieID { get; set; }

        [Column(TypeName = "decimal(3,2)")]

        public decimal Rating { get; set; }

        public DateTime RatingDate { get; set; }

        public string ReviewText { get; set; }

        [Column(TypeName = "decimal(3,2)")]

        public decimal DefaultRatingScore { get; set; } = 10.0m;

    }
}
