using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class MovieGenreDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Movie")]
        public int MovieID { get; set; }
        public MovieDTO Movie { get; set; }

        [ForeignKey("Genre")]
        public int GenreID { get; set; }
        public GenreDTO Genre { get; set; }

        public bool ActiveFlag { get; set; } = true;

    }
}
