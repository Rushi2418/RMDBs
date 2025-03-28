using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model.DTO
{
    public class GenreUpdateDTO
    {
       
        public int ID { get; set; }

        [MaxLength(255)]
        public required string Name { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal SortOrder { get; set; }
        public bool ActiveFlag { get; set; } = true;


    }
}
