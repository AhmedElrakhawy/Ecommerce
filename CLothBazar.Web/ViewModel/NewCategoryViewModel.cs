﻿using System.ComponentModel.DataAnnotations;

namespace ClothBazar.Web.ViewModel
{
    public class NewCategoryViewModel
    {
        [Required]
        [MinLength(5), MaxLength(70)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public bool IsFeatsured { get; set; }
        public string ImageUrl { get; set; }
    }
}