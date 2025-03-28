using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class MovieMediaDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Movie")]
        public int MovieID { get; set; }
        public MovieDTO Movie { get; set; }

        [Required]
        public string MediaType { get; set; }
        [MaxLength(1000)]
        public string FilePath { get; set; }

        public bool DefaultFlag { get; set; }

        public bool ActorImgFlag { get; set; }

        public bool MovieImgFlag { get; set; }

        [Column(TypeName = "decimal(5,4)")]

        public decimal SortOrder { get; set; }

        public DateTime UploadDate { get; set; }

        public bool ActiveFlag { get; set; } = true;


    }
}
