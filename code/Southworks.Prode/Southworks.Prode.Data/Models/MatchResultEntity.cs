﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Southworks.Prode.Data.Models
{
    [Table("MatchResults")]
    public class MatchResultEntity : BaseMatchResult, IIdentifiable<Guid>
    {
        [Key]
        public Guid Id { get; set; }
    }

    public enum MatchResult
    {
        HomeVictory,
        Draw,
        AwayVictory
    }
}
