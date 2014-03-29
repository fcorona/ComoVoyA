using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using comovoya.Models;

namespace comovoya.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        public ActionResult Route(IEnumerable<MedioPuntaje> puntajes)
        {
            return PartialView(puntajes);
        }
    }
}
