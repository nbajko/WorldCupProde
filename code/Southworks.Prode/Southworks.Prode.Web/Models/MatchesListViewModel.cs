using System.Collections.Generic;

namespace Southworks.Prode.Web.Models
{
    public class MatchesListViewModel
    {
        public IEnumerable<MatchViewModel> Matches { get; set; }

        public MatchesListRequest Request { get; set; }
    }
}