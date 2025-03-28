using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class AwardCategoryCreateDTO
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
