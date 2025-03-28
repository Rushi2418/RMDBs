using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class MovieDirectorDTO
    {
        public int ActorID { get; set; }
        public string ActorName { get; set; }
        public string PositionName { get; set; }
    }
}
