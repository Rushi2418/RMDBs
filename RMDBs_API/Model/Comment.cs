using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMDBs_API.Model
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; } // 👈 Add navigation property

        [Required]
        public int MovieID { get; set; }

        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }

        public string CommentText { get; set; }

        public DateTime CommentDate { get; set; } = DateTime.UtcNow;

        public bool ActiveFlag { get; set; } = true;
    }
}
