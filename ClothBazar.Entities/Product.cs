namespace ClothBazar.Entities
{
    public class Product : BaseEntity
    {
        public decimal Price { get; set; }

        public virtual Category Category { get; set; }
    }
}
