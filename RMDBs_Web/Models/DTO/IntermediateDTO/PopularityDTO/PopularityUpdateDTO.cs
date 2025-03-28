using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.DTO
{
    public class PopularityUpdateDTO
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int MovieID { get; set; }

        [Required]
        [Range(0, 999.9999)]
        public decimal PopularityScore { get; set; }
    }
}
