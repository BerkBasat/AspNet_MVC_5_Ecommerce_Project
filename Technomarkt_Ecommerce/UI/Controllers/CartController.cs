using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Entity;
using Service.Concrete;
using UI.Models;
using UI.Utils;

namespace UI.Controllers
{
    public class CartController : Controller
    {

        ProductService productService = new ProductService();
        OrderService orderService = new OrderService();
        OrderDetailService orderDetailService = new OrderDetailService();

        public ActionResult Index()
        {
            if (Session["cart"] != null)
            {
                return View();
            }
            else
            {
                TempData["error"] = "Your cart is empty!";
                return RedirectToAction("Index");
            }
        }

        public ActionResult BillingDetails()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult OrderComplete()
        {
            Cart cart = Session["cart"] as Cart;
            AppUser user = Session["login"] as AppUser;

            List<OrderDetail> orderDetailList = new List<OrderDetail>();


            if (cart != null)
            {

                Random rnd = new Random();
                string productList = "";

                Order order = new Order();
                order.AppUserID = user.ID;
                order.OrderNo = rnd.Next(1000, 100000);
                Order result = orderService.Add(order);

                foreach (var item in cart.myCart)
                {

                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = result.ID;
                    orderDetail.ProductId = item.Id;
                    orderDetail.ProductImage = item.ProductImage;
                    orderDetail.ProductName = item.ProductName;
                    orderDetail.UnitPrice = (decimal)item.Price;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.SubTotal = (decimal)item.SubTotal;

                    orderDetailList.Add(orderDetail);
                    orderDetailService.Add(orderDetail);

                    Product product = productService.GetById(item.Id);
                    product.UnitsInStock -= Convert.ToInt16(item.Quantity);

                    productList = $"Product: {item.ProductName} - Price: {item.Price} - Total: {item.SubTotal}";
                }


                //string content = $"We have received your order. Order No: {order.OrderNo}\tYour order: {productList}";
                //MailSender.SendEmail(user.Email, "Order Info", content);

                Session.Remove("cart");

                orderDetailList = orderDetailService.GetDefault(x => x.OrderId == result.ID);

            }

            return View(orderDetailList);
        }

        public ActionResult DeleteCartItem(Guid id)
        {
            Cart cart = Session["cart"] as Cart;

            if (cart != null)
            {
                cart.DeleteItem(id);
            }

            return RedirectToAction("Index");
        }
    }
}