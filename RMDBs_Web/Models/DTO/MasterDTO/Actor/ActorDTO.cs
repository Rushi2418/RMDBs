using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace RMDBs_Web.Models.DTO
{
    
    public class ActorDTO
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public int? Age { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Height { get; set; }

        public DateTime BornDate { get; set; }

        public string BornPlace { get; set; }

        public string? Parents { get; set; }
        public string? Chilldren { get; set; }

        public string DebutMovie { get; set; }

        public string Biography { get; set; }


    }
}
