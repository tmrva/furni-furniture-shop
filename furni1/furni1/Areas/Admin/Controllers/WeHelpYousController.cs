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
    public class WeHelpYousController : Controller
    {
        #region Dependency injection

        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public WeHelpYousController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
		#endregion

		#region Index
		public async Task<IActionResult> Index()
		{
			List<WeHelpYou> weHelpYous = await _db.WeHelpYous.OrderByDescending(x => x.Id).ToListAsync();

			return View(weHelpYous);
		}
		#endregion

		#region Details
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			WeHelpYou? dbWeHelpYou = await _db.WeHelpYous.FirstOrDefaultAsync(x => x.Id == id);
			if (dbWeHelpYou == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(dbWeHelpYou);
		}
		#endregion

		#region Edit
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			WeHelpYou? dbWeHelpYou = await _db.WeHelpYous.FirstOrDefaultAsync(x => x.Id == id);
			if (dbWeHelpYou == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(dbWeHelpYou);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Edit(int id, WeHelpYou weHelpYou)
		{
			if (id != weHelpYou.Id)
			{
				return RedirectToAction("Error");
			}
			WeHelpYou? dbWeHelpYou = await _db.WeHelpYous.FirstOrDefaultAsync(x => x.Id == id);
			if (dbWeHelpYou == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			#region Photo1

			if (weHelpYou.Photo1 != null)
			{
				if (!weHelpYou.Photo1.IsImage())
				{
					ModelState.AddModelError("Photo", "File type must be an image");
					return View();
				}
				if (weHelpYou.Photo1.IsMoreThan2Mb())
				{
					ModelState.AddModelError("Photo", "The size must be less than 2 mb");
					return View();
				}
				string folder = Path.Combine(_env.WebRootPath, "images");
				string path = Path.Combine(folder, dbWeHelpYou.Image1);
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}

				dbWeHelpYou.Image1 = await weHelpYou.Photo1.SaveFileAsync(folder);
			}
			#endregion

			#region Photo2

			if (weHelpYou.Photo2 != null)
			{
				if (!weHelpYou.Photo2.IsImage())
				{
					ModelState.AddModelError("Photo", "File type must be an image");
					return View();
				}
				if (weHelpYou.Photo2.IsMoreThan2Mb())
				{
					ModelState.AddModelError("Photo", "The size must be less than 2 mb");
					return View();
				}
				string folder = Path.Combine(_env.WebRootPath, "images");
				string path = Path.Combine(folder, dbWeHelpYou.Image2);
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}

				dbWeHelpYou.Image2 = await weHelpYou.Photo2.SaveFileAsync(folder);
			}
			#endregion

			#region Photo3

			if (weHelpYou.Photo3 != null)
			{
				if (!weHelpYou.Photo3.IsImage())
				{
					ModelState.AddModelError("Photo", "File type must be an image");
					return View();
				}
				if (weHelpYou.Photo3.IsMoreThan2Mb())
				{
					ModelState.AddModelError("Photo", "The size must be less than 2 mb");
					return View();
				}
				string folder = Path.Combine(_env.WebRootPath, "images");
				string path = Path.Combine(folder, dbWeHelpYou.Image3);
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}

				dbWeHelpYou.Image3 = await weHelpYou.Photo3.SaveFileAsync(folder);
			}
			#endregion

			dbWeHelpYou.Title = weHelpYou.Title;

			dbWeHelpYou.Description = weHelpYou.Description;
			dbWeHelpYou.DescriptionLeft = weHelpYou.DescriptionLeft;
			dbWeHelpYou.DescriptionRigth = weHelpYou.DescriptionRigth;

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
