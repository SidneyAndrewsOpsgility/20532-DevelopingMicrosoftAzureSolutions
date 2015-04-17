using Contoso.Events.ViewModels;
using System.Net;
using System.Web.Mvc;

namespace Contoso.Events.Web.Controllers
{
    public class HomeController : Controller
    {
        private const int EVENTS_GRID_SIZE = 10;

        [HttpGet]
        public ActionResult Index()
        {
            VerificationViewModel viewModel = new VerificationViewModel();

            return View("Index", viewModel);
        }

        [HttpPost]
        public ActionResult Index(VerificationViewModel viewModel)
        {
            if (!viewModel.Verify())
            {
                return View("Index", viewModel);
            }
            else
            {
                ViewData["IPAddress"] = viewModel.IPAddress;

                return RedirectToAction("Events", new { ipaddress = viewModel.IPAddress.ToString() });
            }

        }

        [HttpGet]
        public ActionResult Events(string ipaddress)
        {
            IPAddress server;
            if (IPAddress.TryParse(ipaddress, out server))
            {
                EventsGridViewModel viewModel = new EventsGridViewModel(server);

                return View("Events", viewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}