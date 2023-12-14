﻿namespace Diana.Areas.Manage.ViewModels
{
    public class UpdateProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int ?CategoryId { get; set; }
        public IFormFile? formFile { get; set; }
        public List<int>? SizeIds { get; set; }
        public List<int>? ColorIds { get; set; }
        public List<int>? MaterialIds { get; set; }
    }
}
