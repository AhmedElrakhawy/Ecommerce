using ClothBazar.database;
using ClothBazar.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClothBazar.Services
{
    public class CategoriesService
    {
        public static CategoriesService Instance
        {
            get
            {
                if (instance == null)
                    instance = new CategoriesService();
                return instance;
            }
        }
        private static CategoriesService instance { set; get; }

        private CategoriesService()
        {

        }
        public List<Category> GetCategories()
        {
            using (var Context = new CBContext())
            {
                return Context.Categories.Include(x => x.Products).ToList();
            }
        }
        public List<Category> GetFeaturedCategories()
        {
            using (var Context = new CBContext())
            {
                return Context.Categories.Where(X => X.IsFeatured && X.ImageUrl != null).ToList();
            }
        }
        public Category GetCategory(int ID)
        {
            using (var Context = new CBContext())
            {
                return Context.Categories.Find(ID);
            }
        }
        public void Save(Category category)
        {
            using (var Context = new CBContext())
            {
                Context.Categories.Add(category);
                Context.SaveChanges();
            }
        }
        public void UpdateCategory(Category category)
        {
            using (var Context = new CBContext())
            {
                Context.Entry(category).State = EntityState.Modified;
                Context.SaveChanges();
            }
        }
        public void DeleteCategory(int ID)
        {
            using (var Context = new CBContext())
            {
                var category = GetCategory(ID);
                Context.Entry(category).State = EntityState.Deleted;
                //Context.Categories.Remove(category);
                Context.SaveChanges();
            }
        }
    }
}
