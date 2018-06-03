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
        private readonly IMatchBetsService matchBetsService;

        public PlayerController(
            IMatchesService matchesService,
            IMatchBetsService matchBetsService)
        {
            this.matchesService = matchesService;
            this.matchBetsService = matchBetsService;
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
    }
}