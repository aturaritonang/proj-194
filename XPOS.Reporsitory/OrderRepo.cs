using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPOS.DataModel;
using XPOS.ViewModel;

namespace XPOS.Reporsitory
{
    public class OrderRepo
    {
        // Get order detail by product
        public static OrderDetailViewModel ByProduct(long id)
        {
            //id => Product Id
            OrderDetailViewModel result = new OrderDetailViewModel();
            using (var db = new XPosContext())
            {
                result = (from p in db.Products
                          where p.Id == id
                          select new OrderDetailViewModel
                          {
                              ProductId = p.Id,
                              ProductName = p.Name,
                              Price = p.Price
                          }).FirstOrDefault();
            }
            return result;
        }

        //  Get new reference number
        public static string GetNewReference()
        {
            string yearMonth = DateTime.Now.ToString("yy") + DateTime.Now.Month.ToString("D2");
            string result = "SLS-" + yearMonth + "-";
            using (var db = new XPosContext())
            {
                var maxRef = db.OrderHeaders
                    .Where(oh => oh.Reference.Contains(result))
                    .Select(o => new { reference = o.Reference })
                    .OrderByDescending(o => o.reference)
                    .FirstOrDefault();

                if (maxRef != null)
                {
                    string[] oldRef = maxRef.reference.Split('-');
                    int newInc = int.Parse(oldRef[2]) + 1;
                    result += newInc.ToString("D4");
                }
                else
                {
                    result += "0001";
                }
            }
            return result;
        }

        // Save order
        public static ResponseResult Insert(OrderHeaderViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {


                using (var db = new XPosContext())
                {
                    string newRef = GetNewReference();
                    OrderHeader oh = new OrderHeader();
                    oh.Amount = entity.Amount;
                    oh.Reference = newRef;
                    oh.Active = true;

                    oh.CreatedBy = "Atur";
                    oh.CreatedDate = DateTime.Now;

                    db.OrderHeaders.Add(oh);

                    foreach (var item in entity.Details)
                    {
                        OrderDetail od = new OrderDetail();
                        od.HeaderId = oh.Id;
                        od.ProductId = item.ProductId;
                        od.Price = item.Price;
                        od.Quantity = item.Quantity;
                        od.Active = true;

                        od.CreatedBy = "Atur";
                        od.CreatedDate = DateTime.Now;

                        db.OrderDetails.Add(od);
                    }

                    db.SaveChanges();

                    entity.Reference = newRef;
                    result.Entity = entity;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
