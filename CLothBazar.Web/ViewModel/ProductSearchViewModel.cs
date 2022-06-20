using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class ProductSearchViewModel
    {
        public int PageNo { get; set; }
        public string SearchTerm { get; set; }
        public List<Product> Products { get; set; }
    }
}