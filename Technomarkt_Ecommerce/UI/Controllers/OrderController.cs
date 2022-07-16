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
        AppUserService appUserService = new AppUserService();

        //List orders that belongs to the current user
        public ActionResult Index()
        {
            var currentUserId = appUserService.GetDefault(x => x.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().ID;
            var userOrders = orderService.GetDefault(x => x.AppUserID == currentUserId).ToList();
            return View(userOrders);
        }
    }
}