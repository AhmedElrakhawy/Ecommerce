using ClothBazar.Entities;
using ClothBazar.Services;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class CategoryController : Controller
    {
        CategoriesService categoryService = new CategoriesService();
        // GET: Category
        [HttpGet]
        public ActionResult Index()
        {
            var Categories = categoryService.GetCategories();
            return View(Categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            categoryService.Save(category);
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var Category = categoryService.GetCategory(ID);
            return View(Category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            categoryService.UpdateCategory(category);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var Category = categoryService.GetCategory(ID);
            return View(Category);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            categoryService.DeleteCategory(category.ID);
            return RedirectToAction("Index");
        }
    }
}