using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class ActorMovieAwardDTO1
    {

        public int ID { get; set; }



        public int? ActorID { get; set; }

        public int? MovieID { get; set; }

        public int AwardID { get; set; }
        public string AwardName { get; set; }

        public int AwardCategoryID { get; set; }

        public int Year { get; set; }
        public string? AwardDiscription { get; set; }


    }
}
