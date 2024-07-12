using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using furni1.DataAccesLayer;
using furni1.Models;
using furni1.Helpers;
using Microsoft.AspNetCore.Authorization;
using furni1.ViewModels;

namespace furni1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Staff")]
    public class ProductsController : Controller
    {
        #region Dependency injection

        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ProductsController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        #endregion

        #region Index
        public async Task<IActionResult> Index()
		{
			List<Product> test = new();
			List<Product> products;

			if (User.IsInRole("Admin"))
			{
				products = await _db.Products
					.Include(x => x.ProductCategories)
					.ThenInclude(x => x.Category)
					.OrderByDescending(x => x.Id)
					.ToListAsync();
			}
			else
			{
				products = await _db.Products
					.Include(x => x.ProductCategories)
					.ThenInclude(x => x.Category)
					.Where(x => !x.IsDeactive)
					.OrderByDescending(x => x.Id)
					.ToListAsync();
			}

			return View(test);
		}
		#endregion

		#region Details
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			Product? dbProduct = await _db.Products.
				Include(x => x.ProductCategories).
				ThenInclude(x => x.Category).
				FirstOrDefaultAsync(x => x.Id == id);
			if (dbProduct == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(dbProduct);
		}
		#endregion

		#region Create
		public async Task<IActionResult> Create()
		{
			ViewBag.Categories = await _db.Categories.Where(x => !x.IsDeactive).ToListAsync();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(List<int> categoryIds, Product product)
		{
			ViewBag.Categories = await _db.Categories.Where(x => !x.IsDeactive).ToListAsync();

            #region Photo

            if (product.Photo == null)
			{
				ModelState.AddModelError("Photo", "You must choose an image");
			}
			if (!product.Photo.IsImage())
			{
				ModelState.AddModelError("Photo", "File type must be an image");
				return View();
			}
			if (product.Photo.IsMoreThan2Mb())
			{
				ModelState.AddModelError("Photo", "The size must be less than 2 mb");
				return View();
			}
			string folder = Path.Combine(_env.WebRootPath, "images");
			product.Image = await product.Photo.SaveFileAsync(folder);

            #endregion

            List<ProductCategory> productCategories = new List<ProductCategory>();
			foreach (int categoryId in categoryIds)
			{
				ProductCategory productCategory = new ProductCategory
				{
					CategoryId = categoryId,
				};
				productCategories.Add(productCategory);
			}
			product.ProductCategories = productCategories;

			await _db.Products.AddAsync(product);
			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		#endregion

		#region Edit
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}

			Product? dbProduct = await _db.Products
				.Include(x => x.ProductCategories)
				.ThenInclude(x => x.Category)
				.FirstOrDefaultAsync(x => x.Id == id);

			if (dbProduct == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			ViewBag.Categories = await _db.Categories.Where(x => !x.IsDeactive).ToListAsync();
			return View(dbProduct);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Edit(int? id, List<int> categoryIds, Product product)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			Product? dbProduct = await _db.Products.
				Include(x => x.ProductCategories).
				ThenInclude(x => x.Category).
				FirstOrDefaultAsync(x => x.Id == id);
			if (dbProduct == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			ViewBag.Categories = await _db.Categories.Where(x => !x.IsDeactive).ToListAsync();

			#region Photo

			if (product.Photo != null)
			{
				if (!product.Photo.IsImage())
				{
					ModelState.AddModelError("Photo", "File type must be an image");
					return View();
				}
				if (product.Photo.IsMoreThan2Mb())
				{
					ModelState.AddModelError("Photo", "The size must be less than 2 mb");
					return View();
				}
				string folder = Path.Combine(_env.WebRootPath, "images");
				if (!string.IsNullOrEmpty(product.Image))
				{
					string path = Path.Combine(folder, product.Image);
					if (System.IO.File.Exists(path))
					{
						System.IO.File.Delete(path);
					}
				}
				dbProduct.Image = await product.Photo.SaveFileAsync(folder);
			}
			#endregion

			dbProduct.Name = product.Name;
			dbProduct.Price = product.Price;
			dbProduct.Description = product.Description;

			List<ProductCategory> productCategories = new List<ProductCategory>();
			foreach (int categoryId in categoryIds)
			{
				ProductCategory productCategory = new ProductCategory
				{
					CategoryId = categoryId,
				};
				productCategories.Add(productCategory);
			}
			dbProduct.ProductCategories = productCategories;


			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		#endregion

		#region Activity
		public async Task<IActionResult> Activity(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Product? dbProduct = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
			if (dbProduct == null)
			{
				return BadRequest();
			}

			if (dbProduct.IsDeactive)
			{
				dbProduct.IsDeactive = false;
			}
			else
			{
				dbProduct.IsDeactive = true;
			}
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
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
