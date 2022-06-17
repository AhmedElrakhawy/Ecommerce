using ClothBazar.Entities;
using System.Collections.Generic;

namespace ClothBazar.Web.ViewModel
{
    public class HomeViewModel
    {
        public List<Category> FeaturedCategories { get; set; }
        public List<Product> FeaturedProducts { get; set; }
    }
}