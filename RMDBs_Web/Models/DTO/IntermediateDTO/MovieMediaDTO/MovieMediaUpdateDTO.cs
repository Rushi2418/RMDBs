using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace RMDBs_Web.Models.DTO
{
    public class MovieMediaUpdateDTO
    {
        
        [Required]

        public int Id { get; set; } 
        [Required]
        public MediaType MediaType { get; set; }

        [MaxLength(1000)]
        public string FilePath { get; set; }

        public bool DefaultFlag { get; set; }
        public bool ActorImgFlag { get; set; }
        public bool MovieImgFlag { get; set; }
        public decimal SortOrder { get; set; }
        public DateTime UploadDate { get; set; }
        public bool ActiveFlag { get; set; }

    }
}
