using System.ComponentModel.DataAnnotations;

namespace RMDBs_Web.Models.ViewModel
{
    public class MovieCastDTO1
    {
        public int ActorID { get; set; } // Actor ID should not be nullable (Primary Key)

        public string? PositionName { get; set; } // Nullable (Some actors might not have a position)
        public string? Role { get; set; } // Nullable (Some actors might not have a specific role)
        public string ActorName { get; set; } = string.Empty; // Not nullable (Every actor must have a name)
        public string? Img { get; set; } // Nullable (Some actors might not have an image)
    }
}
