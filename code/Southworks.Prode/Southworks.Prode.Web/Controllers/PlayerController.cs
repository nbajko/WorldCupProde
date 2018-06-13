using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Southworks.Prode.Data.Models;
using Southworks.Prode.Services.Data;
using Southworks.Prode.Web.Helpers;
using Southworks.Prode.Web.Models;

namespace Southworks.Prode.Web.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IMatchesService matchesService;
        private readonly IMatchResultsService matchResultsService;
        private readonly IMatchBetsService matchBetsService;
        private readonly IBetResultsService betResultsService;
        private readonly IUsersService usersService;
        private readonly ICountriesService countriesService;

        public PlayerController(
            IMatchesService matchesService,
            IMatchResultsService matchResultsService,
            IMatchBetsService matchBetsService,
            IBetResultsService betResultsService,
            IUsersService usersService,
            ICountriesService countriesService)
        {
            this.matchesService = matchesService;
            this.matchResultsService = matchResultsService;
            this.matchBetsService = matchBetsService;
            this.betResultsService = betResultsService;
            this.usersService = usersService;
            this.countriesService = countriesService;
        }

        // GET: Matches
        public ActionResult AllMatches()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SetBetResult(MatchResultModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    if (ModelState.Any())
                    {
                        throw new Exception(ModelState.FirstOrDefault().Value.Errors[0].ErrorMessage);
                    }
                    else
                    {
                        throw new Exception("El modelo no es valido!");
                    }
                }

                var match = this.matchesService.GetMatch(model.Id);
                if (match.PlayedOn.Value <= DateTime.UtcNow)
                {
                    throw new Exception("El partido ya ha iniciado!");
                }

                await this.matchBetsService.SaveBetAsync(new MatchBetEntity
                {
                    MatchId = model.Id,
                    UserId = IdentityHelper.GetUserId(User.Identity),
                    HomeGoals = model.HomeGoals,
                    AwayGoals = model.AwayGoals,
                    HomePenalties = model.HomePenalties,
                    AwayPenalties = model.AwayPenalties,
                    Result = model.GetResult(match)
                });

                return Json("Ok");
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new
                {
                    ExceptionMessage = ex.Message
                });
            }
        }

        [HttpGet]
        public ActionResult BetResults(Guid id)
        {
            var user = this.usersService.GetUser(id);
            if (user == null)
            {
                return RedirectToAction("AllMatches");
            }

            var betResults = this.betResultsService.GetBetResults().Where(x => x.UserId.Equals(id)).ToList();
            var matches = this.matchesService.GetMatches().ToList().Where(x => betResults.Any(b => b.MatchId.Equals(x.Id)));
            var countries = this.countriesService.GetCountries().ToList();
            var results = this.matchResultsService.GetResults().ToList().Where(x => betResults.Any(b => b.MatchId.Equals(x.MatchId)));
            var bets = this.matchBetsService.GetUserBets(id).ToList().Where(x => betResults.Any(b => b.MatchId.Equals(x.MatchId)));

            var list = matches.Select(x => MatchesController.ToMatchViewModel(x, results, countries))
                .Select(x => ToBetResultViewModel(x, betResults.FirstOrDefault(b => b.MatchId.Equals(x.Id)), bets.FirstOrDefault(b => b.MatchId.Equals(x.Id))))
                .OrderBy(x => x.PlayedOn);

            return View(new UserBetResultsListViewModel { User = user, BetResults = list });
        }

        private UserBetResultViewModel ToBetResultViewModel(MatchViewModel match, BetResultEntity betResult, MatchBetEntity bet)
        {
            return new UserBetResultViewModel
            {
                AwayTeam = match.AwayTeam,
                AwayTeamCode = match.AwayTeamCode,
                AwayTeamGoals = match.AwayTeamGoals.Value,
                AwayTeamPenalties = match.AwayTeamPenalties,
                HomeTeam = match.HomeTeam,
                HomeTeamCode = match.HomeTeamCode,
                HomeTeamGoals = match.HomeTeamGoals.Value,
                HomeTeamPenalties = match.HomeTeamPenalties,
                PenaltiesDefinition = match.PenaltiesDefinition,
                PlayedOn = match.PlayedOn,
                Stage = match.Stage,
                BetAwayTeamGoals = bet.AwayGoals,
                BetAwayTeamPenalties = bet.AwayPenalties,
                BetHomeTeamGoals = bet.HomeGoals,
                BetHomeTeamPenalties = bet.HomePenalties,
                Points = betResult.Points,
                HitResult = betResult.HitResult,
                HitExactResult = betResult.HitExactResult,
                ExtraPoint = betResult.ExtraPoint,
                BetPenalties = match.Stage.SupportPenalties() && bet.AwayGoals == bet.HomeGoals
            };
        }
    }
}