using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Southworks.Prode.Data.Models;
using Southworks.Prode.Services.Data;
using Southworks.Prode.Web.Models;

namespace Southworks.Prode.Web.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IMatchesService matchesService;
        private readonly IMatchResultsService matchResultsService;
        private readonly ICountriesService countriesService;

        public MatchesController(
            IMatchesService matchesService,
            IMatchResultsService matchResultsService,
            ICountriesService countriesService)
        {
            this.matchesService = matchesService;
            this.matchResultsService = matchResultsService;
            this.countriesService = countriesService;
        }

        // GET: Matches
        public ActionResult AllMatchesPartial(MatchesListRequest request)
        {
            var matches = this.matchesService.GetMatches().ToList();
            var countries = this.countriesService.GetCountries().ToList();
            var results = this.matchResultsService.GetResults().ToList();
            
            var matchesList = matches.Select(x => ToMatchViewModel(x, results, countries)).ToList();

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

            this.ViewBag.AllowSaveResults = request.AllowSaveResults;
            this.ViewBag.AllowCalculateResults = request.AllowCalculateResults;

            return View(matchesList);
        }

        private MatchViewModel ToMatchViewModel(MatchEntity match, IEnumerable<MatchResultEntity> results, IEnumerable<CountryEntity> countires)
        {
            var result = results.FirstOrDefault(x => match.Id.Equals(x.Id));
            var homeTeam = countires.FirstOrDefault(x => match.HomeTeam.Equals(x.Id));
            var awayTeam = countires.FirstOrDefault(x => match.AwayTeam.Equals(x.Id));

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
                Completed = result != null,
                PenaltiesDefinition = result != null && match.Stage.SupportPenalties() && result.AwayGoals == result.AwayPenalties
            };
        }
    }
}