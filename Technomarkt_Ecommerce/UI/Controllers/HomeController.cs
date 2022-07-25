﻿using DAL.Entity;
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
        //todo: Create paypal getaway!
        //todo: Add SMS sender!

        ProductService productService = new ProductService();
        SubCategoryService subCategoryService = new SubCategoryService();
        BrandService brandService = new BrandService();
        AppUserService appUserService = new AppUserService();


        public ActionResult Index()
        {
            return View(productService.GetList());
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
            //Remove all cookies
            string[] cookies = Request.Cookies.AllKeys;
            foreach(string cookie in cookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }

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