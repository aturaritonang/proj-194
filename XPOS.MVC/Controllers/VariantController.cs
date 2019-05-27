using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XPOS.Reporsitory;
using XPOS.ViewModel;

namespace XPOS.MVC.Controllers
{
    public class VariantController : Controller
    {
        // GET: Variant
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult List()
        {
            return PartialView("_List", VariantRepo.All());
        }

        public ActionResult ListByCat(int id = 0)
        {
            return PartialView("_ListByCat", VariantRepo.ByCatId(id));
        }

        public ActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(CategoryRepo.All(), "Id", "Name");
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(VariantViewModel model)
        {
            ResponseResult result = VariantRepo.Update(model);
            return Json(new {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.CategoryList = new SelectList(CategoryRepo.All(), "Id", "Name");
            return PartialView("_Edit", VariantRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(VariantViewModel model)
        {
            ResponseResult result = VariantRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", VariantRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(VariantViewModel model)
        {
            ResponseResult result = VariantRepo.Delete(model.Id);
            return Json(new {
                success = result.Success,
                message = result.Message,
                entity = model
            }, JsonRequestBehavior.AllowGet);
        }
    }
}