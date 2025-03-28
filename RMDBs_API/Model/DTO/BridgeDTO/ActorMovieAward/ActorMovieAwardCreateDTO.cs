using System.ComponentModel.DataAnnotations;

namespace RMDBs_API.Model.DTO
{
    public class ActorMovieAwardCreateDTO : IValidatableObject
    {
        [Required]
    
        public int Typeid { get; set; }

        public int? ActorID { get; set; }
        public int? MovieID { get; set; }

        [Required]
        public int AwardID { get; set; }

        [Required]
        public int AwardCategoryID { get; set; }

        [Required]
        public int Year { get; set; }
        public string? AwardDiscription { get; set; }

        // Custom validation logic
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Typeid == 1 && ActorID == null)
            {
                yield return new ValidationResult("ActorID is required when Typeid is 1.", new[] { nameof(ActorID) });
            }

            if (Typeid == 2 && MovieID == null)
            {
                yield return new ValidationResult("MovieID is required when Typeid is 2.", new[] { nameof(MovieID) });
            }

            if (Typeid == 3)
            {
                if (ActorID == null || MovieID == null)
                {
                    yield return new ValidationResult("Both ActorID and MovieID are required when Typeid is 3.", new[] { nameof(ActorID), nameof(MovieID) });
                }
            }
        }
    }
}
