using DAL.Entity;
using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        //todo 4: CAROUSEL IMAGES IN HOME PAGE DOESN'T WORK, FIX IT!!!

        ProductService productService = new ProductService();
        SubCategoryService subCategoryService = new SubCategoryService();
        BrandService brandService = new BrandService();
        AppUserService appUserService = new AppUserService();

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