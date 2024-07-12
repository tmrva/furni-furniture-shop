using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using furni1.DataAccesLayer;
using furni1.Models;
using Microsoft.AspNetCore.Authorization;
using furni1.Helpers;

namespace furni1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Staff")]
    public class ContentProductsController : Controller
    {
        #region Dependency injection

        private readonly AppDbContext _db;

        public ContentProductsController(AppDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
		{
			return View(await _db.ContentProducts.ToListAsync());
		}
		#endregion

		#region Edit
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			ContentProduct? dbContentProduct = await _db.ContentProducts.FirstOrDefaultAsync(x => x.Id == id);
			if (dbContentProduct == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(dbContentProduct);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Edit(int id, ContentProduct contentProduct)
		{
			if (id != contentProduct.Id)
			{
				return RedirectToAction("Error");
			}
			ContentProduct? dbContentProduct = await _db.ContentProducts.FirstOrDefaultAsync(x => x.Id == id);
			if (dbContentProduct == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			dbContentProduct.Title = contentProduct.Title;
			dbContentProduct.Description = contentProduct.Description;

			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
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
