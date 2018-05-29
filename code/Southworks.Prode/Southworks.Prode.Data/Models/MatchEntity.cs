using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Southworks.Prode.Data.Models
{
    [Table("Matches")]
    public class MatchEntity : IIdentifiable<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Index]
        public Guid HomeTeam { get; set; }

        [Index]
        public Guid AwayTeam { get; set; }

        [Index]
        public DateTime? PlayedOn { get; set; }

        public MatchStage Stage { get; set; }
    }

    public enum MatchStage
    {
        GroupA,
        GroupB,
        GroupC,
        GroupD,
        GroupE,
        GroupF,
        GroupG,
        GroupH,
        Round16,
        QuarterFinals,
        SemiFinals,
        ThirdPlace,
        Final
    }
}
