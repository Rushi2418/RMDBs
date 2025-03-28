using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model.DTO
{
    public class MovieWriterDTO
    {
        public int ActorID { get; set; }
        public string ActorName { get; set; }
        public string PositionName { get; set; }
    }
}
