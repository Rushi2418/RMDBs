using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model.DTO
{
    public class PopularityCreateDTO
    {
        [Required]
        public int MovieID { get; set; }

        [Required]
        [Range(0, 9.9999)]
        public decimal PopularityScore { get; set; }
    }
}
