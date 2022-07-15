using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Accountant.Controllers
{
    public class HomeController : Controller
    {
        OrderService orderService = new OrderService();

        // GET: Accountant/Home
        public ActionResult Index()
        {
            return View(orderService.GetList());
        }
    }
}