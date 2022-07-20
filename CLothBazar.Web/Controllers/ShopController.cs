using ClothBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Web.ViewModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using ClothBazar.Entities;

namespace ClothBazar.Web.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index(string SearchTerm, int? MinimumPrice ,int? MaximumPrice ,int? CategoryID, int? SortBy , int? PageNo)
        {
            var Model = new ShopViewModel();
            var PageSize = ConfigurationService.Instance.ShopPageSize();
            Model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();
            Model.MaximumPrice = ProductsService.Instance.GetMaximumPrice();
            PageNo = PageNo.HasValue ? PageNo.Value > 0 ? PageNo.Value : 1 : 1;
            Model.SortBy = SortBy;
            Model.CategoryID = CategoryID;

            int TotalProducts = ProductsService.Instance.FilterProductCount(SearchTerm, MinimumPrice, MaximumPrice, CategoryID, SortBy);
            Model.Products = ProductsService.Instance.FilterProduct(SearchTerm , MinimumPrice , MaximumPrice , CategoryID, SortBy , PageNo.Value , PageSize);
            Model.Pager = new Pager(TotalProducts, PageNo,PageSize);
            return View(Model);
        }

        public ActionResult FilterProducts(string SearchTerm, int? MinimumPrice, int? MaximumPrice, int? CategoryID, int? SortBy, int? PageNo)
        {
            var Model = new FilterProductsViewModel();
            var PageSize = ConfigurationService.Instance.ShopPageSize();
            PageNo = PageNo.HasValue ? PageNo.Value > 0 ? PageNo.Value : 1 : 1;
            Model.SortBy = SortBy;
            Model.CategoryID = CategoryID;
            Model.SearchTerm = SearchTerm;
            Model.Products = ProductsService.Instance.FilterProduct(SearchTerm, MinimumPrice, MaximumPrice, CategoryID, SortBy , PageNo.Value , PageSize);
            int TotalProducts = ProductsService.Instance.FilterProductCount(SearchTerm, MinimumPrice, MaximumPrice, CategoryID, SortBy);
            Model.Pager = new Pager(TotalProducts, PageNo , PageSize);
            return PartialView(Model);
        }

        public ActionResult Checkout()
        {
            var Model = new CheckOutViewModel();
            var CartProductsCookie = Request.Cookies["cartProduct"];
            if (CartProductsCookie != null && !string.IsNullOrEmpty(CartProductsCookie.Value))
            {
                Model.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();
                Model.CartProducts = ProductsService.Instance.GetProducts(Model.CartProductIDs);
                Model.User = UserManager.FindById(User.Identity.GetUserId());
            }
            return View(Model);
        }
        public JsonResult PlaceOrder(string ProductIDs)
        {
            var Result = new JsonResult();
            Result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if(!string.IsNullOrEmpty(ProductIDs))
            {
            var NewOrder = new Order();
            NewOrder.UserId = User.Identity.GetUserId();
            NewOrder.OrderedAt = DateTime.Now;
            NewOrder.Status = "Pending";

            var ProductsQuantity = ProductIDs.Split('-').Select(x => int.Parse(x)).ToList();
            var BoughtProducts = ProductsService.Instance.GetProducts(ProductsQuantity.Distinct().ToList());
            NewOrder.TotalAmount = BoughtProducts.Sum(x => x.Price * ProductsQuantity.Where(c => c == x.ID).Count());

            NewOrder.OrderItems = new List<OrderItem>();
            NewOrder.OrderItems.AddRange(BoughtProducts.Select(x => new OrderItem() { ProductID = x.ID , Quantity = ProductsQuantity.Where(c => c == x.ID).Count() }));

            var RowsEffected = ShopService.Instance.Save(NewOrder);
            Result.Data = new { Success = true , Rows = RowsEffected };
            }
            else
            {
                Result.Data = new { Success = false };
            }
            return Result;
        }
    }
}