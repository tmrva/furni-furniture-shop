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
    public class TestimonialsController : Controller
    {
        #region Dependency injection

        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public TestimonialsController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
		{
			List<Testimonial> testimonials;

			if (User.IsInRole("Admin"))
			{
				testimonials = await _db.Testimonials.OrderByDescending(x => x.Id).ToListAsync();
			}
			else
			{
				testimonials = await _db.Testimonials.Where(x => !x.IsDeactive).OrderByDescending(x => x.Id).ToListAsync();
			}
			return View(testimonials);
		}
		#endregion

		#region Details
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			Testimonial? dbTestimonial = await _db.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
			if (dbTestimonial == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(dbTestimonial);
		}
		#endregion

		#region Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Create(Testimonial testimonial)
		{
			#region Photo

			if (testimonial.Photo == null)
			{
				ModelState.AddModelError("Photo", "You must choose an image");
			}
			if (!testimonial.Photo.IsImage())
			{
				ModelState.AddModelError("Photo", "File type must be an image");
				return View();
			}
			if (testimonial.Photo.IsMoreThan2Mb())
			{
				ModelState.AddModelError("Photo", "The size must be less than 2 mb");
				return View();
			}
			string folder = Path.Combine(_env.WebRootPath, "images");
			testimonial.Image = await testimonial.Photo.SaveFileAsync(folder);

			#endregion

			await _db.Testimonials.AddAsync(testimonial);
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
			Testimonial? dbTestimonial = await _db.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
			if (dbTestimonial == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(dbTestimonial);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Edit(int id, Testimonial testimonial)
		{
			if (id != testimonial.Id)
			{
				return RedirectToAction("Error");
			}
			Testimonial? dbTestimonial = await _db.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
			if (dbTestimonial == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			#region Photo

			if (testimonial.Photo != null)
			{
				if (!testimonial.Photo.IsImage())
				{
					ModelState.AddModelError("Photo", "File type must be an image");
					return View();
				}
				if (testimonial.Photo.IsMoreThan2Mb())
				{
					ModelState.AddModelError("Photo", "The size must be less than 2 mb");
					return View();
				}
				string folder = Path.Combine(_env.WebRootPath, "images");
				string path = Path.Combine(folder, dbTestimonial.Image);
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}

				dbTestimonial.Image = await testimonial.Photo.SaveFileAsync(folder);
			}
			#endregion

			dbTestimonial.Description = testimonial.Description;
			dbTestimonial.Name = testimonial.Name;
			dbTestimonial.Profession = testimonial.Profession;

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
			Testimonial? dbTestimonial = await _db.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
			if (dbTestimonial == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			if (dbTestimonial.IsDeactive)
			{
				dbTestimonial.IsDeactive = false;
			}
			else
			{
				dbTestimonial.IsDeactive = true;
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
