using System.Collections.Generic;

namespace Southworks.Prode.Web.Helpers
{
    public class BetResultPointsHelper
    {
        public static IDictionary<string, int> BetResultPoints = new Dictionary<string, int>
        {
            {"HitResult", 3},
            {"HitHomeGoals", 1},
            {"HitAwayGoals", 1},
            {"HitGoalsDif", 1},
            {"HitExactResult", 3},
            {"HitPenalties", 3},
        };

        public static int GetBetResultPoints(string bet)
        {
            return BetResultPoints[bet];
        }
    }
}