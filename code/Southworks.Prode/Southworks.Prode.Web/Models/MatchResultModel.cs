using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Southworks.Prode.Data.Models;
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
            var matchesService = DependencyResolver.Current.GetService<IMatchesService>() as IMatchesService;

            var match = matchesService.GetMatch(this.Id);
            if (match == null)
            {
                throw new Exception("El partido especificado no existe!");
            }

            if (this.HomeGoals < 0 || this.AwayGoals < 0)
            {
                yield return new ValidationResult("Los goles tienen que ser positivos!");
            }
            
            if (this.HomePenalties.HasValue || this.AwayPenalties.HasValue)
            {
                if (!match.Stage.SupportPenalties())
                {
                    throw new Exception("No se pueden especificar penales en un partido de esta etapa!");
                }

                if (this.HomeGoals != this.AwayGoals)
                {
                    yield return new ValidationResult("No se pueden especificar los penales si no es empate!");
                }
                
                if (!this.HomePenalties.HasValue || !this.AwayPenalties.HasValue || this.HomePenalties.Value < 0 || this.AwayPenalties.Value < 0)
                {
                    yield return new ValidationResult("Los penales tienen que ser positivos!");
                }

                if (this.HomePenalties.Value == this.AwayPenalties.Value)
                {
                    yield return new ValidationResult("Tenes que especificar un ganador por penales!");
                }
            }
        }

        public MatchResult GetResult(MatchEntity match = null)
        {
            if (match == null)
            {
                var matchesService = DependencyResolver.Current.GetService<IMatchesService>() as IMatchesService;
                match = matchesService.GetMatch(this.Id);
            }

            if (this.HomeGoals > this.AwayGoals)
            {
                return MatchResult.HomeVictory;
            }
            else if (this.HomeGoals < this.AwayGoals)
            {
                return MatchResult.AwayVictory;
            }
            else if (match.Stage.SupportPenalties())
            {
                return this.HomePenalties > this.AwayPenalties ? MatchResult.HomeVictory : MatchResult.AwayVictory;
            }
            else
            {
                return MatchResult.Draw;
            }
        }
    }
}