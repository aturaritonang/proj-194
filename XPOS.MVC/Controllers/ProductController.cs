using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XPOS.Reporsitory;
using XPOS.ViewModel;

namespace XPOS.MVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return PartialView("_List", ProductRepo.All());
        }

        public ActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(CategoryRepo.All(), "Id", "Name");
            ViewBag.VariantList = new SelectList(VariantRepo.All(), "Id", "Name");
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            ResponseResult result = ProductRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(long id)
        {
            ProductViewModel model = ProductRepo.ById(id);
            ViewBag.CategoryList = new SelectList(CategoryRepo.All(), "Id", "Name");
            ViewBag.VariantList = new SelectList(VariantRepo.ByCatId(model.CategoryId), "Id", "Name");
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            ResponseResult result = ProductRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}