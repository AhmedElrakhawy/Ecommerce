using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductsService productsService = new ProductsService();
        CategoriesService categoriesService = new CategoriesService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductTable(string Search)
        {
            var Products = productsService.GetProducts();
            if (!string.IsNullOrEmpty(Search))
            {
                Products = Products.Where(x =>
                x.Name != null && x.Name.ToUpper() == Search.ToUpper()).ToList();
            }
            return View(Products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var Categories = categoriesService.GetCategories();
            return PartialView(Categories);
        }
        [HttpPost]
        public ActionResult Create(NewCategoryViewModel Model)
        {
            Product NewProduct = new Product();
            NewProduct.Name = Model.Name;
            NewProduct.Description = Model.Description;
            NewProduct.Name = Model.Name;
            NewProduct.Price = Model.Price;
            NewProduct.Category = categoriesService.GetCategory(Model.CategoryID);
            productsService.Save(NewProduct);
            return RedirectToAction("ProductTable");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var Product = productsService.GetProduct(ID);
            return PartialView(Product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productsService.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }


        [HttpPost]
        public ActionResult Delete(int ID)
        {
            productsService.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }
    }
}