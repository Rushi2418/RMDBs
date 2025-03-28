using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RMDB_Utility;
using static RMDB_Utility.Class1;

namespace RMDBs_API.Model
{
    public class MovieMedia1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]

        [ForeignKey("ReceiverType")]
        public int ReceiverId { get; set; }
        public ReceiverType ReceiverType { get; set; }


        [ForeignKey("Movie")]
        public int? MovieID { get; set; }  
        public Movie Movie { get; set; }

        [ForeignKey("Actor")]
        public int? ActroId { get; set; }
        public Actor Actor { get; set; }

        [Required]
        public MediaType MediaType { get; set; } = MediaType.Video;
        [MaxLength(1000)]
        public string FilePath { get; set; }

        public bool DefaultFlag { get; set; }
        public bool MovieTrailer { get; set; }

        public bool ActorImgFlag { get; set; }

        public bool MovieImgFlag { get; set; }

        [Column(TypeName = "decimal(5,4)")]

        public decimal SortOrder { get; set; }

        public DateTime UploadDate { get; set; }

        public bool ActiveFlag { get; set; } = true;


    }
}
