using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XPOS.Reporsitory;
using XPOS.ViewModel;

namespace XPOS.MVC.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductList(string search = "")
        {
            return PartialView("_ProductList", ProductRepo.GetBySearch(search));
        }

        public ActionResult OrderList()
        {
            return PartialView("_OrderList");
        }

        public ActionResult OrderByProduct(long id)
        {
            // id => Product Id
            return PartialView("_OrderByProduct", OrderRepo.ByProduct(id));
        }

        [HttpPost]
        public ActionResult Payment(OrderHeaderViewModel model, int type = 0)
        {
            if (type == 0)
            {
                return PartialView("_Payment", model);
            } else
            {
                ResponseResult result = OrderRepo.Insert(model);
                return Json(new
                {
                    success = result.Success,
                    message = result.Message,
                    entity = result.Entity
                }, JsonRequestBehavior.AllowGet);
            }
        }

        
    }
}