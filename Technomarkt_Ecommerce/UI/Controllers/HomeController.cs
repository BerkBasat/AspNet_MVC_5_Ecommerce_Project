using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        //todo 4: CAROUSEL IMAGES IN HOME PAGE DOESN'T WORK, FIX IT!!!

        ProductService productService = new ProductService();
        SubCategoryService subCategoryService = new SubCategoryService();
        BrandService brandService = new BrandService();

        public ActionResult Index()
        {
            return View(productService.GetList());
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