using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Southworks.Prode.Data.Models
{
    [Table("MatchBets")]
    public class MatchBetEntity : BaseMatchResult, IIdentifiable<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Index]
        public Guid UserId { get; set; }
    }
}
