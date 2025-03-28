using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model.DTO
{
    public class PositionDTO
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public bool ActiveFlag { get; set; } = true;

        [Column(TypeName = "decimal(5,4)")]
        public decimal SortOrder { get; set; }

    }
}
