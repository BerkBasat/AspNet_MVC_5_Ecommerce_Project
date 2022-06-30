
using DAL.Entity;
using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Utils;

namespace UI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        ProductService productService = new ProductService();
        SubCategoryService subCategoryService = new SubCategoryService();
        BrandService brandService = new BrandService();
        SupplierService supplierService = new SupplierService();

        public ActionResult Index()
        {
            return View(productService.GetList());
        }

        public ActionResult Create()
        {
            ViewBag.SubCategories = subCategoryService.GetList();
            ViewBag.Brands = brandService.GetList();
            ViewBag.Suppliers = supplierService.GetList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase fileImage)
        {
            try
            {
                product.ProductImagePath = ImageUploader.UploadImage("~/Content/img/", fileImage);
                string result = productService.Add(product);
                TempData["info"] = result;
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return View();
        }

        public ActionResult Update(Guid id)
        {
            ViewBag.SubCategories = subCategoryService.GetList();
            ViewBag.Brands = brandService.GetList();
            ViewBag.Suppliers = supplierService.GetList();

            try
            {
                Product updated = productService.GetById(id);
                return View(updated);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Update(Product updated)
        {
            try
            {
                string result = productService.Update(updated);
                TempData["info"] = result;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            var deleted = productService.GetById(id);

            try
            {
                TempData["info"] = productService.Delete(deleted);
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