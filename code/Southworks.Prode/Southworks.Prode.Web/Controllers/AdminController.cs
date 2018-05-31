using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Southworks.Prode.Data.Models;
using Southworks.Prode.Services.Data;
using Southworks.Prode.Web.Models;

namespace Southworks.Prode.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMatchesService matchesService;
        private readonly IMatchResultsService matchResultsService;
        private readonly ICountriesService countriesService;

        public AdminController(
            IMatchesService matchesService,
            IMatchResultsService matchResultsService,
            ICountriesService countriesService)
        {
            this.matchesService = matchesService;
            this.matchResultsService = matchResultsService;
            this.countriesService = countriesService;
        }

        // GET: Matches
        public ActionResult Matches()
        {
            return View();
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
                if (match == null)
                {
                    throw new Exception("El partido especificado no existe!");
                }

                if (!match.Stage.SupportPenalties() && (model.HomePenalties.HasValue || model.AwayPenalties.HasValue))
                {
                    throw new Exception("No se pueden especificar penales en un partido de esta etapa!");
                }

                MatchResult result;
                if (model.HomeGoals > model.AwayGoals)
                {
                    result = MatchResult.HomeVictory;
                }
                else if (model.HomeGoals < model.AwayGoals)
                {
                    result = MatchResult.AwayVictory;
                }
                else if (match.Stage.SupportPenalties())
                {
                    result = model.HomePenalties > model.AwayPenalties ? MatchResult.HomeVictory : MatchResult.AwayVictory;
                }
                else
                {
                    result = MatchResult.Draw;
                }

                await this.matchResultsService.SaveResultAsync(new MatchResultEntity
                {
                    Id = model.Id,
                    HomeGoals = model.HomeGoals,
                    AwayGoals = model.AwayGoals,
                    HomePenalties = model.HomePenalties,
                    AwayPenalties = model.AwayPenalties,
                    Result = result
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