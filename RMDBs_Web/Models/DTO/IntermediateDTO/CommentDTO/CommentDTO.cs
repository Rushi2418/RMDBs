using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class CommentDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public UserDTO User { get; set; }

        [ForeignKey("Movie")]
        public int MovieID { get; set; }
        public MovieDTO Movie { get; set; }

        public string CommentText { get; set; }

        public DateTime CommentDate { get; set; }
        public bool ActiveFlag { get; set; } = true;

    }
}
