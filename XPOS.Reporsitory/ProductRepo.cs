using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPOS.DataModel;
using XPOS.ViewModel;

namespace XPOS.Reporsitory
{
    public class ProductRepo
    {
        // Get All
        public static List<ProductViewModel> All()
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            using (var db = new XPosContext())
            {
                result = db.Products
                    .Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        //Category
                        CategoryId = p.Variant.CategoryId,
                        CategoryName = p.Variant.Category.Initial,

                        //Variant
                        VariantId = p.VariantId,
                        VariantName = p.Variant.Initial,

                        Initial = p.Initial,
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description,
                        Stock = p.Stock,
                        Active = p.Active
                    }).ToList();
            }
            return result;
        }

        // Get By Id
        public static ProductViewModel ById(long id)
        {
            ProductViewModel result = new ProductViewModel();
            using (var db = new XPosContext())
            {
                result = db.Products
                    .Where(p => p.Id == id)
                    .Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        //Category
                        CategoryId = p.Variant.CategoryId,
                        CategoryName = p.Variant.Category.Initial,

                        //Variant
                        VariantId = p.VariantId,
                        VariantName = p.Variant.Initial,

                        Initial = p.Initial,
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description,
                        Stock = p.Stock,
                        Active = p.Active
                    }).FirstOrDefault();
            }
            return result;
        }

        // Get by search
        public static List<ProductViewModel> GetBySearch(string search)
        {
            List<ProductViewModel> result = new List<ProductViewModel>();
            using (var db = new XPosContext())
            {
                result = db.Products
                    .Where(p => p.Active == true && 
                    (p.Initial.Contains(search) || p.Name.Contains(search)))
                    .Take(10)
                    .Select(p => new ProductViewModel {
                        Id = p.Id,
                        CategoryId = p.Variant.CategoryId,
                        CategoryName = p.Variant.Category.Name,
                        VariantId = p.VariantId,
                        VariantName = p.Variant.Name,
                        Initial = p.Initial,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Stock = p.Stock,
                        Active = p.Active
                    })
                    .ToList();
            }
            return result;
        }

        // Update
        public static ResponseResult Update(ProductViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new XPosContext())
            {
                if (entity.Id == 0)
                {
                    Product product = new Product();
                    product.VariantId = entity.VariantId;
                    product.Initial = entity.Initial;
                    product.Name = entity.Name;
                    product.Description = entity.Description;
                    product.Price = entity.Price;
                    product.Stock = entity.Stock;
                    product.Active = entity.Active;

                    product.CreatedBy = "Atur";
                    product.CreatedDate = DateTime.Now;

                    db.Products.Add(product);
                    db.SaveChanges();

                    result.Entity = entity;
                }
                else
                {
                    Product product = db.Products
                        .Where(p => p.Id == entity.Id)
                        .FirstOrDefault();

                    if (product != null)
                    {
                        product.VariantId = entity.VariantId;
                        product.Initial = entity.Initial;
                        product.Name = entity.Name;
                        product.Description = entity.Description;
                        product.Price = entity.Price;
                        product.Stock = entity.Stock;
                        product.Active = entity.Active;

                        db.SaveChanges();
                        result.Entity = entity;
                    }
                }
            }
            return result;
        }
    }
}
