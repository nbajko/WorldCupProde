using System;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Web.Models
{
    public class MatchViewModel
    {
        public Guid Id { get; set; }

        public Guid HomeTeamId { get; set; }

        public string HomeTeam { get; set; }

        public string HomeTeamCode { get; set; }

        public int? HomeTeamGoals { get; set; }

        public int? HomeTeamPenalties { get; set; }

        public Guid AwayTeamId { get; set; }

        public string AwayTeam { get; set; }

        public string AwayTeamCode { get; set; }

        public int? AwayTeamGoals { get; set; }

        public int? AwayTeamPenalties { get; set; }

        public DateTime PlayedOn { get; set; }
        
        public MatchStage Stage { get; set; }

        public bool Completed { get; set; }

        public bool PenaltiesDefinition { get; set; }

        public bool Alert { get; set; }

        public bool AllowToSave { get; set; }
    }
}