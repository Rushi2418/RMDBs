using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class MovieCastDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Movie")]
        public int MovieID { get; set; }
        public MovieDTO Movie { get; set; }

        [ForeignKey("Actor")]
        public int ActorID { get; set; }
        public ActorDTO Actor { get; set; }

        [ForeignKey("Position")]
        public int PositionID { get; set; }
        public PositionDTO Position { get; set; }

        public string Role { get; set; }
        public bool ActiveFlag { get; set; } = true;



    }
}
