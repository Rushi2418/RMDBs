using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model.DTO
{
    public class UserRatingUpdateDTO
    {

        public int ID { get; set; }
        public int MovieID { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal Rating { get; set; }
        public string ReviewText { get; set; }



    }
}
