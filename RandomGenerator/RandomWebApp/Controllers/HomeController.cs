using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RandomWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JsonTest()
        {
            return View();
        }

        public ActionResult JsonpTest()
        {
            return View();
        }
    }
}
