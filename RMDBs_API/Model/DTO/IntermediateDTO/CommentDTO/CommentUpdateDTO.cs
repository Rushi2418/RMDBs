using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model.DTO
{
    public class CommentUpdateDTO
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int MovieID { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "CommentText cannot exceed 500 characters.")]
        public string CommentText { get; set; }

        public bool ActiveFlag { get; set; }
    }
}
