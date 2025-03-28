using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class AwardCategoryUpdateDTO
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public bool ActiveFlag { get; set; } = true;
    }
}
