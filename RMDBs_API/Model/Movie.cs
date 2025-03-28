using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static RMDB_Utility.Class1;
namespace RMDBs_API.Model
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }

        public string Description { get; set; }

        [Required]
        public ProductionStatus productionStatus { get; set; } = ProductionStatus.InProduction;
        public string Language { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Budget { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BoxOfficeCollection { get; set; }

        public DateTime ReleaseDate { get; set; }

        public bool ActiveFlag { get; set; } = true;


    }
}
