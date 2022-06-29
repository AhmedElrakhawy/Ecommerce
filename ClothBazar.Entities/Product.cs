using System.ComponentModel.DataAnnotations;

namespace ClothBazar.Entities
{
    public class Product : BaseEntity
    {
        [Range(500,200000)]
        public decimal Price { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public string ImageUrl { get; set; }
    }
}
