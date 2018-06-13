using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Web.Models
{
    public class UserBetResultsListViewModel
    {
        public IEnumerable<UserBetResultViewModel> BetResults { get; set; }

        public UserEntity User { get; set; }
    }
}