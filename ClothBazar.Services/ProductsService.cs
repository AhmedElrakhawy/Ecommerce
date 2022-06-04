using ClothBazar.database;
using ClothBazar.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClothBazar.Services
{
    public class ProductsService
    {
        public List<Product> GetProducts()
        {
            using (var Context = new CBContext())
            {
                return Context.Products.ToList();
            }
        }
        public Product GetProduct(int ID)
        {
            using (var Context = new CBContext())
            {
                return Context.Products.Find(ID);
            }
        }
        public void Save(Product product)
        {
            using (var Context = new CBContext())
            {
                Context.Products.Add(product);
                Context.SaveChanges();
            }
        }
        public void UpdateProduct(Product product)
        {
            using (var Context = new CBContext())
            {
                Context.Entry(product).State = EntityState.Modified;
                Context.SaveChanges();
            }
        }
        public void DeleteProduct(int ID)
        {
            using (var Context = new CBContext())
            {
                var product = GetProduct(ID);
                Context.Entry(product).State = EntityState.Deleted;
                //Context.Products.Remove(product);
                Context.SaveChanges();
            }
        }
    }
}
