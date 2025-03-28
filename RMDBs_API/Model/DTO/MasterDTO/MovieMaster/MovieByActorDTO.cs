using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static RMDB_Utility.Class1;
namespace RMDBs_API.Model.DTO
{
    public class MovieByActorDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int MovieID { get; set; }

        public string? Role { get; set; }
        public string? MovieImg{ get; set; }





    }
}
