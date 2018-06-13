using System;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Web.Models
{
    public class UserBetResultViewModel
    {
        public string HomeTeam { get; set; }

        public string HomeTeamCode { get; set; }

        public int HomeTeamGoals { get; set; }

        public int? HomeTeamPenalties { get; set; }

        public string AwayTeam { get; set; }

        public string AwayTeamCode { get; set; }

        public int AwayTeamGoals { get; set; }

        public int? AwayTeamPenalties { get; set; }

        public DateTime PlayedOn { get; set; }

        public MatchStage Stage { get; set; }

        public bool PenaltiesDefinition { get; set; }

        public int BetHomeTeamGoals { get; set; }

        public int? BetHomeTeamPenalties { get; set; }

        public int BetAwayTeamGoals { get; set; }

        public int? BetAwayTeamPenalties { get; set; }

        public bool BetPenalties { get; set; }

        public int Points { get; set; }
        
        public bool HitResult { get; set; }

        public bool HitExactResult { get; set; }

        public bool ExtraPoint { get; set; }
    }
}