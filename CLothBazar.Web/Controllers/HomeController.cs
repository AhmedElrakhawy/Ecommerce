using ClothBazar.Services;
using ClothBazar.Web.ViewModel;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class HomeController : Controller
    {
        //ProductsService Products = new ProductsService();
        public ActionResult Index()
        {
            var Model = new HomeViewModel()
            {
                FeaturedCategories = CategoriesService.Instance.GetAllCategories()
            };
            return View(Model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}