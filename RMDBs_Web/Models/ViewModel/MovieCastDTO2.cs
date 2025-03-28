using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.ViewModel
{
    public class MovieCastDTO2
    {
        public int ActorID { get; set; } 
        public int MovieID { get; set; } 


        public string? PositionName { get; set; } 
        public string? Role { get; set; } 
        public string ActorName { get; set; } = string.Empty; 
        public string? Img { get; set; } 
    }
}
