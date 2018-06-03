using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Southworks.Prode.Data.Models
{
    public class BaseMatchResult
    {
        [Index]
        public Guid MatchId { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public int? HomePenalties { get; set; }

        public int? AwayPenalties { get; set; }

        public MatchResult Result { get; set; }
    }
}
