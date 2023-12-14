namespace Diana.Models
{
    public class Image:BaseEntity
    {
        public string ImgUrl { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
