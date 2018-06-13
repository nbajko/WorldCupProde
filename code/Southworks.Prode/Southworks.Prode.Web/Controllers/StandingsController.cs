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
            var betResults = this.betResultsService.GetBetResults().ToList();

            var model = users.Select(x => GetPlayerStandings(betResults.Where(b => b.UserId.Equals(x.Id)), x))
                .OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.ExactResult)
                .ThenByDescending(x => x.ExtraPoints)
                .ToList();

            if (count > 0)
            {
                model = model.Take(count).ToList();
            }

            return View(model);
        }

        private StandingsViewModel GetPlayerStandings(IEnumerable<BetResultEntity> bets, UserEntity user)
        {
            return new StandingsViewModel
            {
                UserId = user.Id,
                UserName = user.Name,
                UserEmail = user.Email,
                Points = bets.Sum(b => b.Points),
                Results = bets.Count(b => b.HitResult),
                ExactResult = bets.Count(b => b.HitExactResult),
                ExtraPoints = bets.Count(b => b.ExtraPoint)
            };
        }
    }
}