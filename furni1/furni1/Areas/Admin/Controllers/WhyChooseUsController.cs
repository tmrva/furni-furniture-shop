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
	public class WhyChooseUsController : Controller
    {
        #region Dependency injection

        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public WhyChooseUsController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
		{
			List<WhyChooseUs> whyChooseUs = await _db.WhyChooseUs.OrderByDescending(x => x.Id).ToListAsync();

			return View(whyChooseUs);
		}
		#endregion

		#region Details
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			WhyChooseUs? dbWhyChooseUs = await _db.WhyChooseUs.FirstOrDefaultAsync(x => x.Id == id);
			if (dbWhyChooseUs == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(dbWhyChooseUs);
		}
		#endregion

		#region Edit
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			WhyChooseUs? dbWhyChooseUs = await _db.WhyChooseUs.FirstOrDefaultAsync(x => x.Id == id);
			if (dbWhyChooseUs == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(dbWhyChooseUs);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Edit(int id, WhyChooseUs whyChooseUs)
		{
			if (id != whyChooseUs.Id)
			{
				return RedirectToAction("Error");
			}
			WhyChooseUs? dbWhyChooseUs = await _db.WhyChooseUs.FirstOrDefaultAsync(x => x.Id == id);
			if (dbWhyChooseUs == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			#region Photo1

			if (whyChooseUs.Photo1 != null)
			{
				if (!whyChooseUs.Photo1.IsImage())
				{
					ModelState.AddModelError("Photo", "File type must be an image");
					return View();
				}
				if (whyChooseUs.Photo1.IsMoreThan2Mb())
				{
					ModelState.AddModelError("Photo", "The size must be less than 2 mb");
					return View();
				}
				string folder = Path.Combine(_env.WebRootPath, "images");
				string path = Path.Combine(folder, dbWhyChooseUs.Image);
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}

				dbWhyChooseUs.Image = await whyChooseUs.Photo1.SaveFileAsync(folder);
			}
			#endregion

			#region Photo2

			if (whyChooseUs.Photo2 != null)
			{
				if (!whyChooseUs.Photo2.IsImage())
				{
					ModelState.AddModelError("Photo", "File type must be an image");
					return View();
				}
				if (whyChooseUs.Photo2.IsMoreThan2Mb())
				{
					ModelState.AddModelError("Photo", "The size must be less than 2 mb");
					return View();
				}
				string folder = Path.Combine(_env.WebRootPath, "images");
				string path = Path.Combine(folder, dbWhyChooseUs.Image);
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}

				dbWhyChooseUs.Image = await whyChooseUs.Photo2.SaveFileAsync(folder);
			}
			#endregion

			#region Photo3

			if (whyChooseUs.Photo3 != null)
			{
				if (!whyChooseUs.Photo3.IsImage())
				{
					ModelState.AddModelError("Photo", "File type must be an image");
					return View();
				}
				if (whyChooseUs.Photo3.IsMoreThan2Mb())
				{
					ModelState.AddModelError("Photo", "The size must be less than 2 mb");
					return View();
				}
				string folder = Path.Combine(_env.WebRootPath, "images");
				string path = Path.Combine(folder, dbWhyChooseUs.Image);
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}

				dbWhyChooseUs.Image = await whyChooseUs.Photo3.SaveFileAsync(folder);
			}
			#endregion

			#region Photo4

			if (whyChooseUs.Photo4 != null)
			{
				if (!whyChooseUs.Photo4.IsImage())
				{
					ModelState.AddModelError("Photo", "File type must be an image");
					return View();
				}
				if (whyChooseUs.Photo4.IsMoreThan2Mb())
				{
					ModelState.AddModelError("Photo", "The size must be less than 2 mb");
					return View();
				}
				string folder = Path.Combine(_env.WebRootPath, "images");
				string path = Path.Combine(folder, dbWhyChooseUs.Image);
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}

				dbWhyChooseUs.Image = await whyChooseUs.Photo4.SaveFileAsync(folder);
			}
			#endregion

			dbWhyChooseUs.Title = whyChooseUs.Title;

			dbWhyChooseUs.MiniTitle1 = whyChooseUs.MiniTitle1;
			dbWhyChooseUs.MiniTitle2 = whyChooseUs.MiniTitle2;
			dbWhyChooseUs.MiniTitle3 = whyChooseUs.MiniTitle3;
			dbWhyChooseUs.MiniTitle4 = whyChooseUs.MiniTitle4;

			dbWhyChooseUs.Description = whyChooseUs.Description;

			dbWhyChooseUs.MiniDescription1 = whyChooseUs.MiniDescription1;
			dbWhyChooseUs.MiniDescription2 = whyChooseUs.MiniDescription2;
			dbWhyChooseUs.MiniDescription3 = whyChooseUs.MiniDescription3;
			dbWhyChooseUs.MiniDescription4 = whyChooseUs.MiniDescription4;

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
