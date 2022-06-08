using ClothBazar.Entities;
using System.Collections.Generic;

namespace ClothBazar.Web.ViewModel
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}