using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XPOS.Reporsitory;
using XPOS.ViewModel;

namespace XPOS.MVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        //Get
        public ActionResult List()
        {
            return PartialView("_List", CategoryRepo.All());
        }

        //Get
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            ResponseResult result = CategoryRepo.Update(model);
            return Json(new {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        // Get
        public ActionResult Edit(int id)
        {
            return PartialView("_Edit", CategoryRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel model)
        {
            ResponseResult result = CategoryRepo.Update(model);
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }

        //Get for delete
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", CategoryRepo.ById(id));
        }

        [HttpPost]
        public ActionResult Delete(CategoryViewModel model)
        {
            ResponseResult result = CategoryRepo.Delete(model.Id);
            result.Entity = model;
            return Json(new
            {
                success = result.Success,
                message = result.Message,
                entity = result.Entity
            }, JsonRequestBehavior.AllowGet);
        }
    }
}