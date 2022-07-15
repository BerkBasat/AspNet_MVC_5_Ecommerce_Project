using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Depot.Controllers
{
    public class HomeController : Controller
    {
        OrderService orderService = new OrderService();

        // GET: Depot/Home
        public ActionResult Index()
        {
            return View(orderService.GetList());
        }
    }
}