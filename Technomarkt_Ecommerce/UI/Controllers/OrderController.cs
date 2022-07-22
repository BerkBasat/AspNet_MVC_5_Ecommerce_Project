using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class OrderController : Controller
    {
        OrderService orderService = new OrderService();
        OrderDetailService orderDetailService = new OrderDetailService();
        AppUserService appUserService = new AppUserService();

        //List orders that belongs to the current user
        public ActionResult Index()
        {
            var currentUserId = appUserService.GetDefault(x => x.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().ID;
            var userOrders = orderService.GetDefault(x => x.AppUserID == currentUserId).ToList();
            return View(userOrders);
        }

        public ActionResult OrderDetails(Guid id)
        {
            var order = orderDetailService.GetDefault(x => x.OrderId == id);
            return View(order);
        }

        public ActionResult Cancel(Guid id)
        {
            try
            {
                TempData["info"] = orderService.CancelOrder(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Refund(Guid id)
        {
            try
            {
                TempData["info"] = orderService.RefundOrder(id);
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