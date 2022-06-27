using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class CategorySearchViewModel
    {
        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
        public List<Category> Categories { get; set; }
    }
}