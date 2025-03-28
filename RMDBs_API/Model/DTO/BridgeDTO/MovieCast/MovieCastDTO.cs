using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model.DTO
{
    public class MovieCastDTO
    {
       
        public int ID { get; set; }

        public int MovieID { get; set; }

        public int ActorID { get; set; }

        public int PositionID { get; set; }

        public string Role { get; set; }
        public bool ActiveFlag { get; set; } = true;



    }
}
