using System;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class CommentCreateDTO
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public int MovieID { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "CommentText cannot exceed 500 characters.")]
        public string CommentText { get; set; }

        public DateTime CommentDate { get; set; } = DateTime.UtcNow;

        public bool ActiveFlag { get; set; } = true;
    }
}
