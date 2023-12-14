using System.Reflection.Metadata.Ecma335;

namespace Diana.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
