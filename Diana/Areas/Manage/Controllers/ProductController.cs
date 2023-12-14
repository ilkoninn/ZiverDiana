using Diana.Areas.Manage.ViewModels;
using Diana.DAL;
using Diana.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diana.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {


        AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Product> products = _db.Products.Include(p => p.Images).
                Include(p => p.ProductColors).
                ThenInclude(p => p.Color).
                Include(p => p.ProductMaterials).
                ThenInclude(p => p.Material).
                Include(p => p.ProductSizes).
                ThenInclude(p => p.Size).
                 Include(p => p.ProductColors).
                Include(p => p.Category).
                ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories.ToList();
            ViewBag.Materials = _db.Materials.ToList();
            ViewBag.Colors = _db.Colors.ToList();
            ViewBag.Sizes = _db.Sizes.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateProductVm productVm)
        {
            ViewBag.Categories = _db.Categories.ToList();
            ViewBag.Materials = _db.Materials.ToList();
            ViewBag.Colors = _db.Colors.ToList();
            ViewBag.Sizes = _db.Sizes.ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            Product product = new Product()
            {
                Name = productVm.Name,
                Price = productVm.Price,
                Description = productVm.Description,
                CategoryId = productVm.CategoryId,
                ProductSizes = new List<ProductSize>()
            };

            foreach (var item in productVm.SizeIds)
            {
                ProductSize productSize = new ProductSize()
                {
                    Product=product,
                    SizeId = item
                };
                _db.ProductSizes.Add(productSize);
            }

            foreach (var item in productVm.ColorIds)
            {
                ProductColor productColor = new ProductColor()
                {
                    Product = product,
                    ColorId = item
                };
                _db.ProductColors.Add(productColor);
            }

            foreach (var item in productVm.MaterialIds)
            {
                ProductMaterial  productMaterial = new ProductMaterial()
                {
                    Product = product,
                    MaterialId = item
                };
                _db.ProductMaterials.Add(productMaterial);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Product updated = _db.Products.Include(p => p.Images).
                  Include(p => p.ProductColors).
                  ThenInclude(p => p.Color).
                  Include(p => p.ProductMaterials).
                  ThenInclude(p => p.Material).
                  Include(p => p.ProductSizes).
                  ThenInclude(p => p.Size).
                  Include(p => p.ProductColors).
                  Include(p => p.Category).Where(p => p.Id == id).FirstOrDefault();

            ViewBag.Categories = _db.Categories.ToList();
            ViewBag.Materials = _db.Materials.ToList();
            ViewBag.Colors = _db.Colors.ToList();
            ViewBag.Sizes = _db.Sizes.ToList();

            UpdateProductVm productVm = new UpdateProductVm()
            {
                Name=updated.Name,
                Price=updated.Price,
                Description=updated.Description,
                SizeIds= new List<int>(),
                ColorIds= new List<int>(),
                MaterialIds= new List<int>(),
                CategoryId=updated.CategoryId
            };

            foreach (var item in updated.ProductSizes)
            {
                productVm.SizeIds.Add(item.SizeId);
            }
            foreach (var item in updated.ProductMaterials)
            {
                productVm.MaterialIds.Add(item.MaterialId);
            }
            foreach (var item in updated.ProductColors)
            {
                productVm.ColorIds.Add(item.ColorId);
            }
            return View(productVm);
        }

        [HttpPost]
        public IActionResult Update(UpdateProductVm productVm)
        {
            ViewBag.Categories = _db.Categories.ToList();
            ViewBag.Materials = _db.Materials.ToList();
            ViewBag.Colors = _db.Colors.ToList();
            ViewBag.Sizes = _db.Sizes.ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            Product old = _db.Products.Include(p => p.Images).
                Include(p => p.ProductColors).
                ThenInclude(p => p.Color).
                Include(p => p.ProductMaterials).
                ThenInclude(p => p.Material).
                Include(p => p.ProductSizes).
                ThenInclude(p => p.Size).
                Include(p => p.ProductColors).
                Include(p => p.Category).Where(p => p.Id == productVm.Id).FirstOrDefault();

            old.Name= productVm.Name;
            old.Description= productVm.Description;
            old.Price = productVm.Price;
            old.CategoryId= productVm.CategoryId;
            old.ProductColors = null;  
            foreach (var item in productVm.ColorIds)
            {
                ProductColor productColor = new ProductColor()
                {
                    Product=old,
                    ColorId=item
                };
                _db.ProductColors.Add(productColor);
            }
            old.ProductMaterials = null;
            foreach (var item in productVm.MaterialIds)
            {
                ProductMaterial productMaterial = new ProductMaterial()
                {
                    Product = old,
                    MaterialId = item
                };
                _db.ProductMaterials.Add(productMaterial);
            }
            old.ProductSizes = null;
            foreach (var item in productVm.SizeIds)
            {
                ProductSize productSize = new ProductSize()
                {
                    Product = old,
                    SizeId = item
                };
                _db.ProductSizes.Add(productSize);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Product product = _db.Products.FirstOrDefault(x => x.Id == id);
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
