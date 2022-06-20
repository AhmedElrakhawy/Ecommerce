using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        //Index
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CategoryTable(string Search)
        {
            var Categories = CategoriesService.Instance.GetCategories();
            if(!string.IsNullOrEmpty(Search))
            {
                Categories = Categories.Where(X => X.Name != null && X.Name == Search).ToList();
            }
            var Model = new CategoryViewModel()
            {
                Categories = Categories
            };
            return View(Model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView(new Category());
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            CategoriesService.Instance.Save(category);
            return RedirectToAction("CategoryTable");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var Category = CategoriesService.Instance.GetCategory(ID);
            return PartialView(Category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            CategoriesService.Instance.UpdateCategory(category);
            return PartialView("CategoryTable");
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            CategoriesService.Instance.DeleteCategory(ID);
            return RedirectToAction("CategoryTable");
        }

        //[HttpPost]
        //public ActionResult Delete(Category category)
        //{
        //    categoryService.DeleteCategory(category.ID);
        //    return RedirectToAction("CategoryTable");
        //}
    }
}