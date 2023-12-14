﻿namespace Diana.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string ?Description { get; set; }
        public int ?CategoryId { get; set; }
        public Category ?Category { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductMaterial> ProductMaterials { get; set; }
        public List<Image> Images { get; set; }
        //public IFormFile formFile { get; set; }


    }
}
