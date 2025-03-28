using RMDBs_API.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Watchlist
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Required]
    public int UserID { get; set; }

    [ForeignKey("UserID")]
    public User User { get; set; }

    [Required]
    [ForeignKey("Movie")]
    public int MovieID { get; set; }
    public Movie Movie { get; set; }

    public DateTime AddedDate { get; set; } = DateTime.UtcNow;

    public bool IsWatched { get; set; }

}
