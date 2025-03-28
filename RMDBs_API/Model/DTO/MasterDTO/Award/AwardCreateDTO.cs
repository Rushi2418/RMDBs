using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model.DTO
{
    public class AwardCreateDTO
    {
      

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public bool ActiveFlag { get; set; } = true;
    }
}
