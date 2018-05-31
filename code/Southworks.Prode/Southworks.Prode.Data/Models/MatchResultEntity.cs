using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Southworks.Prode.Data.Models
{
    [Table("MatchResults")]
    public class MatchResultEntity : IIdentifiable<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        
        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public int? HomePenalties { get; set; }

        public int? AwayPenalties { get; set; }

        public MatchResult? Result { get; set; }
    }

    public enum MatchResult
    {
        HomeVictory,
        Draw,
        AwayVictory
    }
}
