using Diana.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diana.Controllers
{
    public class HomeController : Controller
    {
		AppDbContext _db;

		public HomeController(AppDbContext db)
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
    }
}
