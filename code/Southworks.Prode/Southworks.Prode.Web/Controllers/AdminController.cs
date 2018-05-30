using System.Linq;
using System.Web.Mvc;
using Southworks.Prode.Data.Models;
using Southworks.Prode.Services.Data;
using Southworks.Prode.Web.Models;

namespace Southworks.Prode.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMatchesService matchesService;
        private readonly ICountriesService countriesService;

        public AdminController(
            IMatchesService matchesService,
            ICountriesService countriesService)
        {
            this.matchesService = matchesService;
            this.countriesService = countriesService;
        }

        // GET: Matches
        public ActionResult Matches()
        {
            this.ViewBag.Countries = this.countriesService.GetCountries().ToList();

            var model = this.matchesService.GetMatches()
                .Select(x => new MatchModel
                {
                    Id = x.Id,
                    HomeTeam = x.HomeTeam,
                    AwayTeam = x.AwayTeam,
                    PlayedOn = x.PlayedOn.Value,
                    Stage = x.Stage
                })
                .OrderBy(x => x.PlayedOn);

            return View(model);
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
        public ActionResult NewMatch(MatchModel model)
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

            this.matchesService.SaveMatch(new MatchEntity
            {
                HomeTeam = model.HomeTeam,
                AwayTeam = model.AwayTeam,
                PlayedOn = model.PlayedOn.ToUniversalTime(),
                Stage = model.Stage
            });

            return RedirectToAction("Matches");
        }
    }
}