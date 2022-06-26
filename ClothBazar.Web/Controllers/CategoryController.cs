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
        public ActionResult CategoryTable(string Search,int? PageNo)
        {
            var Model = new CategorySearchViewModel();
            Model.SearchTerm = Search;
            PageNo = PageNo.HasValue ? PageNo.Value > 0 ? PageNo.Value : 1 : 1;
            var TotalRecords = CategoriesService.Instance.GetCategoriesCount(Search);
            Model.Categories = CategoriesService.Instance.GetCategories(Search,PageNo.Value);
            if(Model.Categories != null)
            {
                Model.Pager = new Pager(TotalRecords,PageNo,3);
                return PartialView("CategoryTable",Model);
            }
            else
            {
                return HttpNotFound();
            }
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
            Model.Categories = CategoriesService.Instance.GetAllCategories();
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
            Model.Categories = CategoriesService.Instance.GetAllCategories();
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