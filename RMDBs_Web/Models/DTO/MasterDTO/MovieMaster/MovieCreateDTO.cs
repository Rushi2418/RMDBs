using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static RMDB_Utility.Class1;
namespace RMDBs_Web.Models.DTO
{
    public class MovieCreateDTO
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }

        public string Description { get; set; }

        [Required]
        public ProductionStatus ProductionStatus { get; set; } = ProductionStatus.InProduction;

        public string Language { get; set; }

        [Column(TypeName = "decimal(10,4)")]
        public decimal Budget { get; set; }

        [Column(TypeName = "decimal(10,4)")]
        public decimal BoxOfficeCollection { get; set; }

        public DateTime ReleaseDate { get; set; }



    }
}
