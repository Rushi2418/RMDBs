using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model
{
    public class ActorMovieAward
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [ForeignKey("ReciverType")]

        public int Typeid { get; set; }
        public ReceiverType ReciverType { get; set; }

        [ForeignKey("Actor")]
        public int? ActorID { get; set; }
        public Actor Actor { get; set; }

        [ForeignKey("Movie")]
        public int? MovieID { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("Award")]
        public int AwardID { get; set; }
        public Award Award { get; set; }

        [ForeignKey("AwardCategory")]
        public int AwardCategoryID { get; set; }
        public AwardCategory AwardCategory { get; set; }

        public string? AwardDiscription { get; set; }

        public int Year { get; set; }

    }
}
