using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class PositionCreateDTO
    {

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }


        [Column(TypeName = "decimal(5,4)")]
        public decimal SortOrder { get; set; }

    }
}
