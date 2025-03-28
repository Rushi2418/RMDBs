using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static RMDB_Utility.Class1;
using System.Text.Json.Serialization;
namespace RMDBs_API.Model.DTO
{
    public class MovieUpdateDTO
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }

        public string Description { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))] // Ensures proper JSON handling for enums

        public ProductionStatus ProductionStatus { get; set; } = ProductionStatus.InProduction;

        public string Language { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Budget { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BoxOfficeCollection { get; set; }

        public DateTime ReleaseDate { get; set; }


        public bool ActiveFlag { get; set; } = true;


    }
}
