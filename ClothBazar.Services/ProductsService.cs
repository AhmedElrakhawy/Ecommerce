using ClothBazar.database;
using ClothBazar.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClothBazar.Services
{
    public class ProductsService
    {
        public static ProductsService Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProductsService();
                return instance;
            }
        }
        private static ProductsService instance { set; get; }

        private ProductsService()
        {

        }
        public int GetProductsCount(string Search)
        {
            using (var Context = new CBContext())
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return Context.Products
                        .Where(X => X.Name != null && X.Name.ToUpper()
                        .Contains(Search.ToUpper()))
                        .Count();
                }
                else
                {
                    return Context.Products.Count();
                }
            }
        }
        public List<Product> GetProducts(string Search,int PageNo)
        {
            int PageSize = 6;
            using (var Context = new CBContext())
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return Context.Products.OrderBy(x => x.ID)
                        .Skip((PageNo - 1) * PageSize)
                        .Take(PageSize)
                        .Include(x => x.Category)
                        .Where(X => X.Name != null && X.Name.ToUpper()
                        .Contains(Search.ToUpper()))
                        .ToList();
                }
                else
                {
                    return Context.Products
                        .OrderBy(x => x.ID)
                        .Skip((PageNo - 1) * PageSize)
                        .Take(PageSize)
                        .Include(x => x.Category)
                        .ToList();
                }
            }
        }
        public List<Product> GetProducts( int PageNo , int PageSize)
        {
            using (var Context = new CBContext())
            {
                    return Context.Products.OrderByDescending(x => x.ID)
                        .Skip((PageNo - 1) * PageSize)
                        .Take(PageSize)
                        .Include(x => x.Category)
                        .ToList();
            }
        }
        public List<Product> GetLatestProducts(int NumOfProducts)
        {
            using (var Context = new CBContext())
            {
                    return Context.Products.OrderByDescending(x => x.ID)
                        .Take(NumOfProducts)
                        .Include(x => x.Category)
                        .ToList();
            }
        }
        public List<Product> GetProductsByCategoryID(int CategoryID , int PageSize)
        {
            using (var Context = new CBContext())
            {
                return Context.Products.Where(x => x.CategoryID == CategoryID)
                        .OrderByDescending(x => x.ID)
                        .Take(PageSize)
                        .Include(x => x.Category)
                        .ToList();
            }
        }
        public List<Product> GetProducts()
        {
            using (var Context = new CBContext())
            {
                //return Context.Products.OrderBy(x => x.ID)
                //    .Skip((PageNo -1) * PageSize).Take(PageSize).Include(X => X.Category).ToList();
                return Context.Products.Include(X => X.Category).ToList();
            }
        }
        public Product GetProduct(int ID)
        {
            using (var Context = new CBContext())
            {
                return Context.Products.Include(x => x.Category).FirstOrDefault(x=> x.ID == ID);
            }
        }
        public List<Product> GetProducts(List<int> IDs)
        {
            using (var Context = new CBContext())
            {
                return Context.Products.Where(product => IDs.Contains(product.ID)).ToList();
            }
        }
        public void Save(Product product)
        {
            using (var Context = new CBContext())
            {
                Context.Entry(product.Category).State = EntityState.Unchanged;
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
