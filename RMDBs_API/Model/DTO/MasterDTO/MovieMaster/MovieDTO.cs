using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static RMDB_Utility.Class1;
namespace RMDBs_API.Model.DTO
{
    public class MovieDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string ProductionStatus { get; set; } // Include ProductionStatus
        public string Language { get; set; }
        public decimal Budget { get; set; }
        public decimal BoxOfficeCollection { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool ActiveFlag { get; set; } 

        


    }
}
