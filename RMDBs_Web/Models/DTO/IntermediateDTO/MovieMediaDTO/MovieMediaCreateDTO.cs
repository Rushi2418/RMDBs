using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static RMDB_Utility.Class1;

namespace RMDBs_Web.Models.DTO
{
    public class MovieMediaCreateDTO
    {
       
        public int MovieID { get; set; }

        [Required]
        public MediaType MediaType { get; set; }=MediaType.Image;

        [Required]
        [MaxLength(1000)]
        public string FilePath { get; set; }

        public bool DefaultFlag { get; set; }

        public bool ActorImgFlag { get; set; }

        public bool MovieImgFlag { get; set; }

        [Column(TypeName = "decimal(5,4)")]
        public decimal SortOrder { get; set; }




    }
}
