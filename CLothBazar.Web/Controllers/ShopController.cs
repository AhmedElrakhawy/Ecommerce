using ClothBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Web.ViewModel;

namespace ClothBazar.Web.Controllers
{
    public class ShopController : Controller
    {
        ProductsService productsService = new ProductsService();
        public ActionResult Checkout()
        {
            var Model = new CheckOutViewModel();
            var CartProductsCookie = Request.Cookies["cartProduct"];
            if (CartProductsCookie != null)
            {
                Model.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();
                Model.CartProducts = productsService.GetProducts(Model.CartProductIDs);
            }
            return View(Model);
        }
    }
}