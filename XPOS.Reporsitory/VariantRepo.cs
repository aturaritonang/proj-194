using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPOS.DataModel;
using XPOS.ViewModel;

namespace XPOS.Reporsitory
{
    public class VariantRepo
    {
        // Get all
        public static List<VariantViewModel> All()
        {
            List<VariantViewModel> result = new List<VariantViewModel>();
            using (var db = new XPosContext())
            {
                result = db.Variants
                    .Select(v => new VariantViewModel
                    {
                        Id = v.Id,
                        CategoryId = v.CategoryId,
                        CategoryName = v.Category.Name,
                        Initial = v.Initial,
                        Name = v.Name,
                        Active = v.Active
                    }).ToList();
            }
            return result;
        }

        // Get By Id
        public static VariantViewModel ById(long id)
        {
            VariantViewModel result = new VariantViewModel();
            using (var db = new XPosContext())
            {
                result = db.Variants
                    .Where(v => v.Id == id)
                    .Select(v => new VariantViewModel
                    {
                        Id = v.Id,
                        CategoryId = v.CategoryId,
                        CategoryName = v.Category.Name,
                        Initial = v.Initial,
                        Name = v.Name,
                        Active = v.Active
                    })
                    .FirstOrDefault();
            }
            return result;
        }

        // Get by Category Id
        public static List<VariantViewModel> ByCatId(long id)
        {
            // id => Category Id
            List<VariantViewModel> result = new List<VariantViewModel>();
            using (var db = new XPosContext())
            {
                // gunakan ini jika 0 adalah semua (id == 0 ? v.CategoryId : id)
                result = db.Variants
                    .Where(v => v.CategoryId == id)
                    .Select(v => new VariantViewModel
                    {
                        Id = v.Id,
                        CategoryId = v.CategoryId,
                        CategoryName = v.Category.Name,
                        Initial = v.Initial,
                        Name = v.Name,
                        Active = v.Active
                    }).ToList();
            }
            return result;
        }

        // Update
        public static ResponseResult Update(VariantViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new XPosContext())
            {
                // Create
                if (entity.Id == 0)
                {
                    Variant variant = new Variant();
                    variant.CategoryId = entity.CategoryId;
                    variant.Initial = entity.Initial;
                    variant.Name = entity.Name;
                    variant.Active = entity.Active;

                    variant.CreatedBy = "Atur";
                    variant.CreatedDate = DateTime.Now;

                    db.Variants.Add(variant);
                    db.SaveChanges();

                    result.Entity = entity;
                }
                else
                // Edit
                {
                    Variant variant = db.Variants
                        .Where(v => v.Id == entity.Id)
                        .FirstOrDefault();

                    if (variant != null)
                    {
                        variant.CategoryId = entity.CategoryId;
                        variant.Initial = entity.Initial;
                        variant.Name = entity.Name;
                        variant.Active = entity.Active;

                        variant.ModifiedBy = "Atur";
                        variant.ModifiedDate = DateTime.Now;

                        db.SaveChanges();

                        result.Entity = entity;
                    }
                }
            }
            return result;
        }

        // Delete
        public static ResponseResult Delete(long id)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new XPosContext())
            {
                Variant variant = db.Variants
                    .Where(v => v.Id == id)
                    .FirstOrDefault();
                if (variant != null)
                {
                    db.Variants.Remove(variant);
                    db.SaveChanges();
                }
            }
            return result;
        }
    }
}
