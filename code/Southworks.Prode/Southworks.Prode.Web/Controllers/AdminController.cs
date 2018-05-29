using System.Web.Mvc;
using Southworks.Prode.Services.Data;

namespace Southworks.Prode.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMatchesService matchesService;
        private readonly ICountriesService countriesService;

        // GET: Admin
        public ActionResult Matches()
        {
            return View();
        }
    }
}