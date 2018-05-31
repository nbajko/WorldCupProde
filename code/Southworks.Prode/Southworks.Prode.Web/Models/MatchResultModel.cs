using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Southworks.Prode.Services.Data;

namespace Southworks.Prode.Web.Models
{
    public class MatchResultModel : IValidatableObject
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int HomeGoals { get; set; }

        public int? HomePenalties { get; set; }

        [Required]
        public int AwayGoals { get; set; }

        public int? AwayPenalties { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.HomeGoals < 0 || this.AwayGoals < 0)
            {
                yield return new ValidationResult("Los goles tienen que ser positivos!");
            }
            
            if (this.HomePenalties.HasValue || this.AwayPenalties.HasValue)
            {
                if (this.HomeGoals != this.AwayGoals)
                {
                    yield return new ValidationResult("No se pueden especificar los penales si no es empate!");
                }

                if (this.HomePenalties < 0 || this.AwayPenalties < 0)
                {
                    yield return new ValidationResult("Los penales tienen que ser positivos!");
                }

                if (this.HomePenalties == this.AwayPenalties)
                {
                    yield return new ValidationResult("Tenes que especificar un ganador por penales!");
                }
            }
        }
    }
}