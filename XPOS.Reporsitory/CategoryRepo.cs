using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPOS.DataModel;
using XPOS.ViewModel;

namespace XPOS.Reporsitory
{
    public class CategoryRepo
    {
        // Get All
        public static List<CategoryViewModel> All()
        {
            List<CategoryViewModel> result = new List<CategoryViewModel>();
            using (var db = new XPosContext())
            {
                result = (
                    from c in db.Categories
                    select new CategoryViewModel
                    {
                        Id = c.Id,
                        Initial = c.Initial,
                        Name = c.Name,
                        Active = c.Active
                    }).ToList();
            }
            return result != null ? result : new List<CategoryViewModel>();
        }

        // Get by id
        public static CategoryViewModel ById(int id)
        {
            CategoryViewModel result = new CategoryViewModel();
            using (var db = new XPosContext())
            {
                result = db.Categories
                    .Where(o => o.Id == id)
                    .Select(c => new CategoryViewModel
                    {
                        Id = c.Id,
                        Initial = c.Initial,
                        Name = c.Name,
                        Active = c.Active
                    }).FirstOrDefault();
            }
            return result;
        }

        // Update
        public static ResponseResult Update(CategoryViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XPosContext())
                {
                    //Create
                    if (entity.Id == 0)
                    {
                        Category cat = new Category();
                        cat.Initial = entity.Initial;
                        cat.Name = entity.Name;
                        cat.Active = entity.Active;

                        cat.CreatedBy = "Atur";
                        cat.CreatedDate = DateTime.Now;

                        db.Categories.Add(cat);
                        db.SaveChanges();

                        result.Entity = entity;
                    }
                    else
                    // Edit
                    {
                        Category cat = db.Categories
                            .Where(o => o.Id == entity.Id)
                            .FirstOrDefault();

                        if (cat != null)
                        {
                            cat.Initial = entity.Initial;
                            cat.Name = entity.Name;
                            cat.Active = entity.Active;

                            cat.ModifiedBy = "Atur";
                            cat.ModifiedDate = DateTime.Now;

                            db.SaveChanges();

                            result.Entity = entity;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        // Delete
        public static ResponseResult Delete(long id)
        {
            ResponseResult result = new ResponseResult();
            using (var db = new XPosContext())
            {
                Category cat = db.Categories
                    .Where(o => o.Id == id)
                    .FirstOrDefault();
                if (cat != null)
                {
                    db.Categories.Remove(cat);
                    db.SaveChanges();
                }
                else {
                    result.Success = false;
                    result.Message = "Category not found!";
                }
            }
            return result;
        }

    }
}
