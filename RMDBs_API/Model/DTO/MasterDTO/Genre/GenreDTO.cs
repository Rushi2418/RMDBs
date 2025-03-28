using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model.DTO
{
    public class GenreDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(255)]
        public required string Name { get; set; }

        public bool ActiveFlag { get; set; } = true;

        [Column(TypeName = "decimal(5,2)")]
        public decimal SortOrder { get; set; }

    }
}
