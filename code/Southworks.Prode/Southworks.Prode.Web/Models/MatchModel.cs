using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Web.Models
{
    public class MatchModel : IValidatableObject
    {
        [Required]
        public Guid HomeTeam { get; set; }

        [Required]
        public Guid AwayTeam { get; set; }

        [Required]
        public DateTime PlayedOn { get; set; }

        [Required]
        public MatchStage Stage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.HomeTeam.Equals(this.AwayTeam))
            {
                yield return new ValidationResult("El equipo local no puede ser igual al visitante.");
            }
        }
    }
}