﻿using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
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
            return PartialView(Model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var Model = new ProductViewModel();
            Model.Categories=  CategoriesService.Instance.GetCategories();
            return PartialView(Model);
        }
        [HttpPost]
        public ActionResult Create(ProductViewModel Model)
        {
            Product NewProduct = new Product();
            NewProduct.Name = Model.Name;
            NewProduct.Description = Model.Description;
            NewProduct.Price = Model.Price;
            NewProduct.ImageUrl = Model.ImageUrl;
            NewProduct.Category = CategoriesService.Instance.GetCategory(Model.CategoryID);
            ProductsService.Instance.Save(NewProduct);
            return RedirectToAction("ProductTable");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var Product = ProductsService.Instance.GetProduct(ID);
            var Model = new ProductViewModel();
            Model.ID = Product.ID;
            Model.Name = Product.Name;
            Model.Description = Product.Description;
            Model.Price = Product.Price;
            Model.CategoryID = Product.Category != null ? Product.CategoryID : 0;
            Model.ImageUrl = Product.ImageUrl;
            Model.Categories = CategoriesService.Instance.GetCategories();
            return PartialView(Model);
        }
        [HttpPost]
        public ActionResult Edit(ProductViewModel Model)
        {
            var ExcistingProduct = ProductsService.Instance.GetProduct(Model.ID);
            ExcistingProduct.Name = Model.Name;
            ExcistingProduct.Description = Model.Description;
            ExcistingProduct.Price = Model.Price;

            ExcistingProduct.ImageUrl = Model.ImageUrl;
            ExcistingProduct.Category = null;
            ExcistingProduct.CategoryID = Model.CategoryID;
            ProductsService.Instance.UpdateProduct(ExcistingProduct);
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