using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.CustomFilters;

namespace UI.Areas.Depot.Controllers
{
    [Authorize(Roles = "depot")]
    [AuthFilter]
    public class HomeController : Controller
    {
        OrderService orderService = new OrderService();

        // GET: Depot/Home
        public ActionResult Index()
        {
            return View(orderService.GetList());
        }

        public ActionResult SendToCargo(Guid id)
        {
            try
            {
                TempData["info"] = orderService.ShipOrder(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index"); 
            }
        }

        public ActionResult Deliver(Guid id)
        {
            try
            {
                TempData["info"] = orderService.DeliverOrder(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}