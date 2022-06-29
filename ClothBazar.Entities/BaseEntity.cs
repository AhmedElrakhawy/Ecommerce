﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        [Required]
        [MinLength(3),MaxLength(50)]
        public String Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
