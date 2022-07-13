using DAL.Entity;
using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UI.Models;
using UI.Utils;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        //todo: Add text to carousel images!
        //todo: Fix layout!
        //todo: Add user roles
        //todo: Create accountant and depot areas
        //todo: User authentication

        ProductService productService = new ProductService();
        SubCategoryService subCategoryService = new SubCategoryService();
        BrandService brandService = new BrandService();
        AppUserService appUserService = new AppUserService();
        OrderService orderService = new OrderService();
        OrderDetailService orderDetailService = new OrderDetailService();

        public ActionResult Index(Guid? id)
        {
            if(id == null)
            {
                return View(productService.GetList());
            }
            else
            {
                return View(productService.GetDefault(x => x.SubCategoryId == id));
            }
        }

        public ActionResult Contact()
        {
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser();
                appUser.Username = registerVM.UserName;
                appUser.Email = registerVM.Email;
                appUser.Password = registerVM.Password;
                var result = appUserService.Add(appUser);

                TempData["info"] = result;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(registerVM);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                bool result = appUserService.Any(x => x.Username == loginVM.UserName && x.Password == loginVM.Password);
                if (result)
                {
                    AppUser user = appUserService.GetDefault(x => x.Username == loginVM.UserName).FirstOrDefault();
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                    Session["login"] = user;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["error"] = $"Username or Password is incorrect! Please try again!";
                    return View(loginVM);
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("login");
            return RedirectToAction("Index");
        }

        public ActionResult ProductDetails(Guid id)
        {
            var product = productService.GetById(id);
            if(product != null)
            {
                return View(product);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //Shopping Cart

        public ActionResult AddToCart(Guid id)
        {
            try
            {
                Product product = productService.GetById(id);
                Cart cart = null;

                if (Session["cart"] == null)
                {
                    cart = new Cart();
                }
                else
                {
                    cart = Session["cart"] as Cart;
                }

                CartItem cartItem = new CartItem();
                cartItem.Id = product.ID;
                cartItem.ProductImage = product.ProductImagePath;
                cartItem.ProductName = product.ProductName;
                cartItem.Price = product.UnitPrice;
                cart.AddItem(cartItem);
                Session["cart"] = cart;

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = $"Could not find a product with id no:{id}";
                return View();
            }
        }

        public ActionResult DeleteCartItem(Guid id)
        {
            Cart cart = Session["cart"] as Cart;

            if(cart != null)
            {
                cart.DeleteItem(id);
            }

            return RedirectToAction("MyCart");
        }

        public ActionResult MyCart()
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

        //Wishlist

        public ActionResult AddToWishlist(Guid id)
        {
            try
            {
                Product product = productService.GetById(id);
                Wishlist wishlist = null;

                if (Session["wishlist"] == null)
                {
                    wishlist = new Wishlist();
                }
                else
                {
                    wishlist = Session["wishlist"] as Wishlist;
                }

                WishlistItem wishlistItem = new WishlistItem();
                wishlistItem.Id = product.ID;
                wishlistItem.ProductImage = product.ProductImagePath;
                wishlistItem.ProductName = product.ProductName;
                wishlistItem.SubCategory = product.SubCategory.SubCategoryName;
                wishlist.AddItem(wishlistItem);
                Session["wishlist"] = wishlist;

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = $"Could not find a product with id no:{id}";
                return View();
            }
        }

        public ActionResult DeleteWishlistItem(Guid id)
        {
            Wishlist wishlist = Session["wishlist"] as Wishlist;

            if (id != null)
            {
                wishlist.DeleteItem(id);
            }

            return RedirectToAction("Wishlist");
        }


        public ActionResult Wishlist()
        {
            if (Session["wishlist"] != null)
            {
                return View();
            }
            else
            {
                TempData["error"] = "Your wishlist is empty!";
                return RedirectToAction("Index");
            }
        }


        //Partial Views
        public PartialViewResult _CategoryPartial()
        {
            return PartialView(subCategoryService.GetList());
        }

        public PartialViewResult _BrandPartial()
        {
            return PartialView(brandService.GetList());
        }
    }
}