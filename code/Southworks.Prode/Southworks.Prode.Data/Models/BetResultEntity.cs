using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Southworks.Prode.Data.Models
{
    [Table("BetResults")]
    public class BetResultEntity : IIdentifiable<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Index]
        public Guid UserId { get; set; }

        [Index]
        public Guid MatchId { get; set; }

        [Index]
        public Guid ResultId { get; set; }

        public bool HitResult { get; set; }

        public bool HitHomeGoals { get; set; }

        public bool HitAwayGoals { get; set; }

        public bool HitGoalsDif { get; set; }

        public bool HitExactResult { get; set; }

        public bool HitPenalties { get; set; }

        public bool ExtraPoint { get; set; }

        public int Points { get; set; }
    }
}
