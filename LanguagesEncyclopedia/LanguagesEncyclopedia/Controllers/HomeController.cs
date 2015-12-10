using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguagesEncyclopedia.Views.Home;

namespace LanguagesEncyclopedia.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/


        EncyclopediaEntities db = new EncyclopediaEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Licenses()
        {
            List<License> licenses = new List<License>();

            licenses = db.Licenses.ToList();

            return View(licenses);
        }

    }
}
