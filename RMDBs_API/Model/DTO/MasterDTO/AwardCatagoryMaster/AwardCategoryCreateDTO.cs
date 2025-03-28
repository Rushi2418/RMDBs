using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model.DTO
{
    public class AwardCategoryCreateDTO
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
