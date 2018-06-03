using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Southworks.Prode.Data.Models;
using Southworks.Prode.Services.Data;
using Southworks.Prode.Web.Models;

namespace Southworks.Prode.Web.Controllers
{
    public class StandingsController : Controller
    {
        private readonly IBetResultsService betResultsService;
        private readonly IUsersService usersService;

        public StandingsController(
            IBetResultsService betResultsService,
            IUsersService usersService)
        {
            this.betResultsService = betResultsService;
            this.usersService = usersService;
        }

        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: StandingsPartial
        public ActionResult StandingsPartial(int count = 0)
        {
            var users = this.usersService.Get().ToList();
            var model = this.betResultsService.GetBetResults().ToList()
                .GroupBy(x => x.UserId)
                .Select(x => GetPlayerStandings(x, users))
                .OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.ExactResult)
                .ThenByDescending(x => x.Penalties)
                .ToList();

            if (count > 0)
            {
                model = model.Take(count).ToList();
            }

            return View(model);
        }

        private StandingsViewModel GetPlayerStandings(IGrouping<Guid, BetResultEntity> bets, IEnumerable<UserEntity> users)
        {
            var model = new StandingsViewModel
            {
                UserId = bets.Key,
                UserName = users.FirstOrDefault(u => u.Id.Equals(bets.Key)).Name,
                UserEmail = users.FirstOrDefault(u => u.Id.Equals(bets.Key)).Email,
                Points = bets.Sum(b => b.Points),
                Results = bets.Count(b => b.HitResult),
                ExactResult = bets.Count(b => b.HitExactResult),
                Penalties = bets.Count(b => b.HitPenalties),
            };

            model.Extra = model.Points - 3 * (model.Results + model.ExactResult + model.Penalties);

            return model; 
        }
    }
}