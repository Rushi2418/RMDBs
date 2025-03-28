using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMDBs_API.Model.DTO
{
    public class ActorMovieAwardUpdateDTO
    {
        public int ID { get; set; }

        public int? Typeid { get; set; }
        public int? ActorID { get; set; }
        public int? MovieID { get; set; }
        public int? AwardID { get; set; }
        public int? AwardCategoryID { get; set; }
        public string? AwardDiscription { get; set; }

        public int? Year { get; set; }
    }
}
