using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model
{
    public class MovieCast
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Movie")]
        public int MovieID { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("Actor")]
        public int ActorID { get; set; }
        public Actor Actor { get; set; }

        [ForeignKey("Position")]
        public int PositionID { get; set; }
        public Position Position { get; set; }

        public string Role { get; set; }
        public bool ActiveFlag { get; set; } = true;



    }
}
