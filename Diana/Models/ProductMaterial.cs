namespace Diana.Models
{
    public class ProductMaterial:BaseEntity
    {
        public int ProductId { get; set; }
        public int MaterialId { get; set; }
        public Product Product { get; set; }
        public Material Material { get; set; }
    }
}
