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

namespace furni1.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin, Staff")]
	public class CategoriesController : Controller
	{
        #region Dependency injection

        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public CategoriesController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
		{
			List<Category> categories;

			if (User.IsInRole("Admin"))
			{
				categories = await _db.Categories.OrderBy(x => x.Name).ToListAsync();
			}
			else
			{
				categories = await _db.Categories.Where(x => !x.IsDeactive).OrderBy(x => x.Name).ToListAsync();
			}
			return View(categories);
		}
		#endregion

		#region Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Category category)
		{
			await _db.Categories.AddAsync(category);
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
			Category? dbCategory = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
			if (dbCategory == null)
			{
				return RedirectToAction("ErrorBadRequest");
            }
			return View(dbCategory);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Edit(int id, Category category)
		{
			if (id != category.Id)
			{
				return RedirectToAction("Error");
			}
			Category? dbCategory = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
			if (dbCategory == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			dbCategory.Name = category.Name;

			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		#endregion

		#region Activity
		public async Task<IActionResult> Activity(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			Category? dbCategory = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
			if (dbCategory == null)
			{
				return RedirectToAction("ErrorBadRequest");
            }

			if (dbCategory.IsDeactive)
			{
				dbCategory.IsDeactive = false;
			}
			else
			{
				dbCategory.IsDeactive = true;
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
