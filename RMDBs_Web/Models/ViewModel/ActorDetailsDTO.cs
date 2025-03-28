using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using RMDBs_Web.Model.DTO;

namespace RMDBs_Web.Models.ViewModel
{

    public class ActorDetailsDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public decimal? Height { get; set; }
        public DateTime? BornDate { get; set; }
        public string? BornPlace { get; set; }
        public string? Parents { get; set; }
        public string? Chilldren { get; set; }
        public string? DebutMovie { get; set; }
        public string? Biography { get; set; }
        public string? ActorImage { get; set; }
        public List<MovieByActorDTO>? Movies { get; set; }

    }
}
