using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Web.Models
{
    public class MatchesListRequest
    {
        public int Count { get; set; }

        public bool ExcludeCompleted { get; set; }

        public bool ExcludePending { get; set; }

        public bool OrderByDescending { get; set; }

        public bool AllowSaveResults { get; set; }

        public bool AllowCalculateResults { get; set; }

        public MatchStage? MatchStage { get; set; }
    }
}