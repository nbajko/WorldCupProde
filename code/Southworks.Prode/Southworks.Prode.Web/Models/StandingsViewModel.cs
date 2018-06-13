using System;

namespace Southworks.Prode.Web.Models
{
    public class StandingsViewModel
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public int Points { get; set; }

        public int Results { get; set; }

        public int ExactResult { get; set; }

        public int ExtraPoints { get; set; }
    }
}