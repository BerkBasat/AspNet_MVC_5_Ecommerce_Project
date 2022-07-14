using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Accountant.Controllers
{
    public class HomeController : Controller
    {
        // GET: Accountant/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}