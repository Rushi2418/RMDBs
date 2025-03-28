using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class GenreCreateDTO
    {
       

        [MaxLength(255)]
        public required string Name { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal SortOrder { get; set; }

    }
}
