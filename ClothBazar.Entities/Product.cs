namespace ClothBazar.Entities
{
    public class Product : BaseEntity
    {
        public decimal Price { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public string ImageUrl { get; set; }
    }
}
