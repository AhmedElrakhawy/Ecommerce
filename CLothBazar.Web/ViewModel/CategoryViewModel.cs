using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class CategoryViewModel
    {
        public string SearchTerm { get; set; }
        public List<Category> Categories { get; set; }
    }
}