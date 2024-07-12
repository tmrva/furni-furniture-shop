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
    public class ContentHomesController : Controller
    {
        #region Dependency injection

        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ContentHomesController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
		{
			return View(await _db.ContentHomes.ToListAsync());
		}
		#endregion

		#region Details
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			ContentHome? dbContentHome = await _db.ContentHomes.FirstOrDefaultAsync(x => x.Id == id);
			if (dbContentHome == null)
			{
				return RedirectToAction("ErrorBadRequest");
            }
			return View(dbContentHome);
		}
		#endregion

		#region Edit
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			ContentHome? dbContentHome = await _db.ContentHomes.FirstOrDefaultAsync(x => x.Id == id);
			if (dbContentHome == null)
			{
				return RedirectToAction("ErrorBadRequest");
            }
			return View(dbContentHome);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Edit(int id, ContentHome contentHome)
		{
			if (id != contentHome.Id)
			{
				return RedirectToAction("Error");
			}
			ContentHome? dbContentHome = await _db.ContentHomes.FirstOrDefaultAsync(x => x.Id == id);
			if (dbContentHome == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			#region Photo

			if (contentHome.Photo != null)
			{
				if (!contentHome.Photo.IsImage())
				{
					ModelState.AddModelError("Photo", "File type must be an image");
					return View();
				}
				if (contentHome.Photo.IsMoreThan2Mb())
				{
					ModelState.AddModelError("Photo", "The size must be less than 2 mb");
					return View();
				}
				string folder = Path.Combine(_env.WebRootPath, "images");
				string path = Path.Combine(folder, dbContentHome.Image);
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}

				dbContentHome.Image = await contentHome.Photo.SaveFileAsync(folder);
			}
			#endregion

			dbContentHome.Title = contentHome.Title;
			dbContentHome.Description = contentHome.Description;

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
