using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model.DTO
{
    public class MovieCastDTO1
    {
        public int ActorID { get; set; }

        public string PositionName { get; set; }

        public string Role { get; set; }
        public string ActorName { get; set; }  // Actor's Name
        public string Img { get; set; }  // Actor's Name


    }
}
