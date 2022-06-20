using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        //ProductsService productsService = new ProductsService();
        //CategoriesService categoriesService = new CategoriesService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductTable(string Search)
        {
            var Model = new ProductSearchViewModel();
            //Model.PageNo = PageNo.HasValue ? PageNo.Value > 0 ? PageNo.Value : 1 : 1;
            Model.Products = ProductsService.Instance.GetProducts(Model.PageNo);
            if (!string.IsNullOrEmpty(Search))
            {
                Model.SearchTerm = Search;
                Model.Products = Model.Products.Where(x =>
                x.Name != null && x.Name.ToUpper() == Search.ToUpper()).ToList();
            }
            return View(Model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var Categories = CategoriesService.Instance.GetCategories();
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
            NewProduct.Category = CategoriesService.Instance.GetCategory(Model.CategoryID);
            ProductsService.Instance.Save(NewProduct);
            return RedirectToAction("ProductTable");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var Product = ProductsService.Instance.GetProduct(ID);
            return PartialView(Product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            ProductsService.Instance.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }


        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductsService.Instance.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }
    }
}