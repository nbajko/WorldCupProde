using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Southworks.Prode.Data.Models;
using Southworks.Prode.Services.Data;
using Southworks.Prode.Web.Helpers;
using Southworks.Prode.Web.Models;

namespace Southworks.Prode.Web.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IMatchesService matchesService;
        private readonly IMatchResultsService matchResultsService;
        private readonly IMatchBetsService matchBetsService;
        private readonly ICountriesService countriesService;
        private readonly IBetResultsService betResultsService;

        public MatchesController(
            IMatchesService matchesService,
            IMatchResultsService matchResultsService,
            IMatchBetsService matchBetsService,
            ICountriesService countriesService, 
            IBetResultsService betResultsService)
        {
            this.matchesService = matchesService;
            this.matchResultsService = matchResultsService;
            this.matchBetsService = matchBetsService;
            this.countriesService = countriesService;
            this.betResultsService = betResultsService;
        }

        public ActionResult AllMatches()
        {
            return View();
        }

        public ActionResult MyMatches()
        {
            return View();
        }

        // GET: Matches
        public ActionResult AllMatchesPartial(MatchesListRequest request)
        {
            var matches = this.matchesService.GetMatches().ToList();
            var countries = this.countriesService.GetCountries().ToList();
            var results = this.matchResultsService.GetResults().ToList();
            var betResults = this.betResultsService.GetBetResults().ToList();
            
            var matchesList = matches.Select(x => ToMatchViewModel(x, results, countries))
                .Select(x => SetMatchBetResultsAlert(x, betResults))
                .Select(x => { x.AllowToSave = x.PlayedOn < DateTime.UtcNow; return x; })
                .ToList();

            if (request.ExcludeCompleted)
            {
                matchesList = matchesList.Where(x => !x.Completed).ToList();
            }

            if (request.ExcludePending)
            {
                matchesList = matchesList.Where(x => x.Completed).ToList();
            }

            if (request.MatchStage.HasValue)
            {
                matchesList = matchesList.Where(x => x.Stage.Equals(request.MatchStage.Value)).ToList();
            }

            if (request.OrderByDescending)
            {
                matchesList = matchesList.OrderByDescending(x => x.PlayedOn).ToList();
            }
            else
            {
                matchesList = matchesList.OrderBy(x => x.PlayedOn).ToList();
            }

            if (request.Count > 0)
            {
                matchesList = matchesList.Take(request.Count).ToList();
            }

            return View("AllMatchesPartial", new MatchesListViewModel { Matches = matchesList, Request = request });
        }

        // GET: Matches
        public ActionResult MyMatchesPartial(MatchesListRequest request)
        {
            var matches = this.matchesService.GetMatches().ToList();
            var countries = this.countriesService.GetCountries().ToList();
            var bets = this.matchBetsService.GetUserBets(User.Identity.GetUserId()).ToList();

            var matchesList = matches.Select(x => ToMatchViewModel(x, bets, countries))
                .Select(x => SetCloseToPlayAlert(x))
                .Select(x => { x.AllowToSave = x.PlayedOn > DateTime.UtcNow; return x; })
                .ToList();

            if (request.ExcludeCompleted)
            {
                matchesList = matchesList.Where(x => !x.Completed).ToList();
            }

            if (request.ExcludePending)
            {
                matchesList = matchesList.Where(x => x.Completed).ToList();
            }

            if (request.OrderByDescending)
            {
                matchesList = matchesList.OrderByDescending(x => x.PlayedOn).ToList();
            }
            else
            {
                matchesList = matchesList.OrderBy(x => x.PlayedOn).ToList();
            }

            if (request.Count > 0)
            {
                matchesList = matchesList.Take(request.Count).ToList();
            }

            return View("AllMatchesPartial", new MatchesListViewModel { Matches = matchesList, Request = request });
        }

        private MatchViewModel SetCloseToPlayAlert(MatchViewModel match)
        {
            match.Alert = match.PlayedOn.AddHours(-1) < DateTime.UtcNow && (!match.HomeTeamGoals.HasValue || !match.AwayTeamGoals.HasValue);

            return match;
        }

        private MatchViewModel SetMatchBetResultsAlert(MatchViewModel match, List<BetResultEntity> betResults)
        {
            match.Alert = match.Completed && !betResults.Any(x => x.MatchId.Equals(match.Id));

            return match;
        }

        private MatchViewModel ToMatchViewModel(MatchEntity match, IEnumerable<BaseMatchResult> results, IEnumerable<CountryEntity> countries)
        {
            var result = results.FirstOrDefault(x => match.Id.Equals(x.MatchId));
            var homeTeam = countries.FirstOrDefault(x => match.HomeTeam.Equals(x.Id));
            var awayTeam = countries.FirstOrDefault(x => match.AwayTeam.Equals(x.Id));

            return new MatchViewModel
            {
                Id = match.Id,
                HomeTeamId = match.HomeTeam,
                HomeTeam = homeTeam.Name,
                HomeTeamCode = homeTeam.Code,
                HomeTeamGoals = result?.HomeGoals,
                HomeTeamPenalties = result?.HomePenalties,
                AwayTeamId = match.AwayTeam,
                AwayTeam = awayTeam.Name,
                AwayTeamCode = awayTeam.Code,
                AwayTeamGoals = result?.AwayGoals,
                AwayTeamPenalties = result?.AwayPenalties,
                PlayedOn = match.PlayedOn.Value,
                Stage = match.Stage,
                Completed = match.PlayedOn <= DateTime.UtcNow,
                PenaltiesDefinition = result != null && match.Stage.SupportPenalties() && result.HomeGoals == result.AwayGoals
            };
        }
    }
}