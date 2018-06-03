using System.Collections.Generic;

namespace Southworks.Prode.Web.Models
{
    public class BetResultsListViewModel
    {
        public IEnumerable<BetResultViewModel> BetResultsList { get; set; }
        
        public MatchViewModel Match { get; set; }
    }
}