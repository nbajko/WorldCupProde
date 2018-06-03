using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Web.Models
{
    public class BetResultViewModel
    {
        public UserEntity User { get; set; }

        public MatchBetEntity Bet { get; set; }

        public BetResultEntity BetResult { get; set; }
    }
}