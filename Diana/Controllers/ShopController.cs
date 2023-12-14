using Diana.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diana.Controllers
{
	public class ShopController : Controller
	{
		AppDbContext _db;

		public ShopController(AppDbContext db)
		{
			_db = db;
		}

		public IActionResult Detail(int id)
		{
			Product detail = _db.Products.Include(p => p.Images).
				 Include(p => p.ProductColors).
				 ThenInclude(p => p.Color).
				 Include(p => p.ProductMaterials).
				 ThenInclude(p => p.Material).
				 Include(p => p.ProductSizes).
				 ThenInclude(p => p.Size).
				 Include(p => p.ProductColors).
				 Include(p => p.Category).Where(p => p.Id == id).FirstOrDefault();
			return View(detail);
		}
	}
}
