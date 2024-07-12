using furni1.DataAccesLayer;
using furni1.Models;
using furni1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace furni1.Controllers
{
	public class ShopController : Controller
	{
        #region Dependency injection

        private readonly AppDbContext _db;
        public ShopController(AppDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
		{
			ProductVM productVM = new ProductVM
			{
				Products = await _db.Products.
					OrderByDescending(x => x.Id).
					Where(x => !x.IsDeactive).
					Take(8).
					ToListAsync(),
				Categories = await _db.Categories.
					OrderBy(x => x.Name).
					Where(x => !x.IsDeactive).
					ToListAsync(),
				ProductCount = await _db.Products.CountAsync(),
			};
			return View(productVM);
		}
		#endregion

		#region Detail
		public async Task<IActionResult> Detail(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			Product? product = await _db.Products.
					Include(x => x.ProductCategories).
					ThenInclude(x => x.Category).
					FirstOrDefaultAsync(x => x.Id == id);
			if (product == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			product.ProductCategories = product.ProductCategories
					.Where(x => !x.Category.IsDeactive)
					.ToList();
			return View(product);
		}
		#endregion

		#region Search
		public async Task<IActionResult> Search(string key)
		{
			List<Product> products = await _db.Products
				.Include(x => x.ProductCategories)
				.ThenInclude(x => x.Category)
				.Where(x => x.ProductCategories.Any(x => x.Category.Name == key))
				.OrderByDescending(x => x.Id)
				.Where(x => !x.IsDeactive)
				.ToListAsync();

			return PartialView("_ProductsLoadMorePartial", products);
		}
		#endregion

		#region LoadMore
		public async Task<IActionResult> LoadMore(int skip)
		{
			int productsCount = await _db.Products.CountAsync();
			if (skip > productsCount)
			{
				return Content("ok");
			}
			List<Product> products = await _db.Products
				.OrderByDescending(x => x.Id)
				.Skip(skip)
				.Take(8)
				.ToListAsync();
			return PartialView("_ProductsLoadMorePartial", products);
		}
		#endregion

		#region Error
		public IActionResult Error()
		{
			return View();
		}
		#endregion

		#region ErrorBadRequest
		public IActionResult ErrorBadRequest()
		{
			return View();
		}
		#endregion
	}
}
