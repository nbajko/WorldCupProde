using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Southworks.Prode.Data.Models;
using Southworks.Prode.Services.Data;
using Southworks.Prode.Web.Features;
using Southworks.Prode.Web.Helpers;
using Southworks.Prode.Web.Models;

namespace Southworks.Prode.Web.Controllers
{
    [RoleAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IMatchesService matchesService;
        private readonly IMatchResultsService matchResultsService;
        private readonly IMatchBetsService matchBetsService;
        private readonly IBetResultsService betResultsService;
        private readonly ICountriesService countriesService;
        private readonly IUsersService usersService;

        public AdminController(
            IMatchesService matchesService,
            IMatchResultsService matchResultsService,
            IMatchBetsService matchBetsService,
            IBetResultsService betResultsService,
            ICountriesService countriesService,
            IUsersService usersService)
        {
            this.matchesService = matchesService;
            this.matchResultsService = matchResultsService;
            this.matchBetsService = matchBetsService;
            this.betResultsService = betResultsService;
            this.countriesService = countriesService;
            this.usersService = usersService;
        }

        // GET: Matches
        public ActionResult Matches(MatchFilter filter)
        {
            return View(filter);
        }

        // GET: NewMatch
        [HttpGet]
        public ActionResult NewMatch()
        {
            this.ViewBag.Countries = this.countriesService.GetCountries()
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                .OrderBy(x => x.Text)
                .ToList();

            return View();
        }

        // POST: NewMatch
        [HttpPost]
        public async Task<ActionResult> NewMatch(MatchModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    this.ViewBag.Countries = this.countriesService.GetCountries()
                        .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                        .OrderBy(x => x.Text)
                        .ToList();

                    return View();
                }

                if (matchesService.ExistsMatch(model.HomeTeam, model.AwayTeam, model.Stage))
                {
                    this.ModelState.AddModelError(string.Empty, "El partido especificado ya existe.");

                    this.ViewBag.Countries = this.countriesService.GetCountries()
                        .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                        .OrderBy(x => x.Text)
                        .ToList();

                    return View();
                }

                await this.matchesService.SaveMatch(new MatchEntity
                {
                    HomeTeam = model.HomeTeam,
                    AwayTeam = model.AwayTeam,
                    PlayedOn = model.PlayedOn.ToUniversalTime(),
                    Stage = model.Stage
                });

                return RedirectToAction("Matches");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, $"Ups! Hubo un error: {ex.Message}");
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetMatchBetsResults(Guid matchId)
        {
            var match = this.matchesService.GetMatch(matchId);
            if (match == null)
            {
                throw new Exception("El partido especificado no existe!");
            }

            var result = this.matchResultsService.GetResultByMatch(matchId);
            if (match == null)
            {
                throw new Exception("No esta cargado el resultado para el partido especificado!");
            }

            var bets = this.matchBetsService.GetBetsByMatch(matchId).ToList();
            var users = this.usersService.Get().ToList();

            var countries = this.countriesService.GetCountries().ToList();
            var homeTeam = countries.FirstOrDefault(x => match.HomeTeam.Equals(x.Id));
            var awayTeam = countries.FirstOrDefault(x => match.AwayTeam.Equals(x.Id));

            var model = new BetResultsListViewModel
            {
                BetResultsList = bets.Select(x => new BetResultViewModel { Bet = x, BetResult = GetBetResult(x, result), User = users.FirstOrDefault(u => u.Id.Equals(x.UserId)) }).OrderByDescending(x => x.BetResult.Points),
                Match = new MatchViewModel
                {
                    Id = match.Id,
                    HomeTeamId = match.HomeTeam,
                    HomeTeam = homeTeam.Name,
                    HomeTeamCode = homeTeam.Code,
                    HomeTeamGoals = result.HomeGoals,
                    HomeTeamPenalties = result.HomePenalties,
                    AwayTeamId = match.AwayTeam,
                    AwayTeam = awayTeam.Name,
                    AwayTeamCode = awayTeam.Code,
                    AwayTeamGoals = result.AwayGoals,
                    AwayTeamPenalties = result.AwayPenalties,
                    PlayedOn = match.PlayedOn.Value,
                    Stage = match.Stage,
                    Completed = match.PlayedOn <= DateTime.UtcNow,
                    PenaltiesDefinition = match.Stage.SupportPenalties() && result.HomeGoals == result.AwayGoals
                }
            };

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> SaveMatchBetResults(Guid matchId)
        {
            var match = this.matchesService.GetMatch(matchId);
            if (match == null)
            {
                throw new Exception("El partido especificado no existe!");
            }

            var result = this.matchResultsService.GetResultByMatch(matchId);
            if (match == null)
            {
                throw new Exception("No esta cargado el resultado para el partido especificado!");
            }

            var bets = this.matchBetsService.GetBetsByMatch(matchId).ToList();

            var betsResults = bets.Select(x => GetBetResult(x, result));

            await this.betResultsService.SaveBetResults(betsResults);

            return View("Matches");
        }

        public BetResultEntity GetBetResult(MatchBetEntity bet, MatchResultEntity result)
        {
            var hitPenalties = false;
            if (result.HomePenalties.HasValue && result.AwayPenalties.HasValue)
            {
                hitPenalties = result.HomePenalties.Equals(bet.HomePenalties) && result.AwayPenalties.Equals(bet.AwayPenalties);
            }

            var betResult = new BetResultEntity
            {
                Id = bet.Id,
                UserId = bet.UserId,
                MatchId = bet.MatchId,
                ResultId = result.Id,
                HitResult = bet.Result.Equals(result.Result),
                HitHomeGoals = bet.HomeGoals.Equals(result.HomeGoals),
                HitAwayGoals = bet.AwayGoals.Equals(result.AwayGoals),
                HitGoalsDif = bet.HomeGoals - bet.AwayGoals == result.HomeGoals - result.AwayGoals,
                HitExactResult = bet.HomeGoals.Equals(result.HomeGoals) && bet.AwayGoals.Equals(result.AwayGoals),
                HitPenalties = hitPenalties
            };

            var betPoints = 0;
            betPoints += betResult.HitResult ? BetResultPointsHelper.BetResultPoints["HitResult"] : 0;

            if (betResult.HitPenalties)
            {
                betPoints += BetResultPointsHelper.BetResultPoints["HitPenalties"];
                betResult.ExtraPoint = true;
            }
            
            if (betResult.HitExactResult)
            {
                betPoints += BetResultPointsHelper.BetResultPoints["HitExactResult"];
            }
            else
            {
                betPoints += betResult.HitHomeGoals ? BetResultPointsHelper.BetResultPoints["HitHomeGoals"] : 0;
                betPoints += betResult.HitAwayGoals ? BetResultPointsHelper.BetResultPoints["HitAwayGoals"] : 0;
                betPoints += betResult.HitGoalsDif && !bet.HomeGoals.Equals(bet.AwayGoals) ? BetResultPointsHelper.BetResultPoints["HitGoalsDif"] : 0;
                betResult.ExtraPoint = betResult.ExtraPoint || betResult.HitHomeGoals || betResult.HitAwayGoals || (betResult.HitGoalsDif && !bet.HomeGoals.Equals(bet.AwayGoals));
            }

            betResult.Points = betPoints;

            return betResult;
        }

        [HttpPost]
        public async Task<JsonResult> SetMatchResult(MatchResultModel model)
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
                if (match.PlayedOn.Value > DateTime.UtcNow)
                {
                    throw new Exception("El partido aun no ha iniciado!");
                }

                await this.matchResultsService.SaveResultAsync(new MatchResultEntity
                {
                    MatchId = model.Id,
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
    }
}