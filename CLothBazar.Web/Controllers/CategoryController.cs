using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class CategoryController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CategoryTable(string Search)
        {
            var Categories = CategoriesService.Instance.GetCategories();
            if(!string.IsNullOrEmpty(Search))
            {
                Categories = Categories.Where(X => X.Name != null && X.Name.ToUpper() == Search.ToUpper()).ToList();
            }
            var Model = new CategoryViewModel()
            {
                SearchTerm = Search,
                Categories = Categories
            };
            return PartialView(Model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var Model = new Category();
            return PartialView(Model);
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            CategoriesService.Instance.Save(category);
            CategoryViewModel Model = new CategoryViewModel();
            Model.Categories = CategoriesService.Instance.GetCategories();
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
            CategoryViewModel Model = new CategoryViewModel();
            Model.Categories = CategoriesService.Instance.GetCategories();
            return PartialView("CategoryTable", Model);
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            CategoriesService.Instance.DeleteCategory(ID);
            return RedirectToAction("CategoryTable");
        }
    }
}