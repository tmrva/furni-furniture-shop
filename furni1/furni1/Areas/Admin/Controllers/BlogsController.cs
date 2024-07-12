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
    public class BlogsController : Controller
    {
        #region Dependency injection

        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public BlogsController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
		{
			List<Blog> blogs;

			if (User.IsInRole("Admin"))
			{
				blogs = await _db.Blogs.OrderByDescending(x => x.Id).ToListAsync();
			}
			else
			{
				blogs = await _db.Blogs.Where(x => !x.IsDeactive).OrderByDescending(x => x.Id).ToListAsync();
			}
			return View(blogs);
		}
		#endregion

		#region Details
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			Blog? dbBlog = await _db.Blogs.FirstOrDefaultAsync(x => x.Id == id);
			if (dbBlog == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(dbBlog);
		}
		#endregion

		#region Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Create(Blog blog)
		{
			#region Photo

			if (blog.Photo == null)
			{
				ModelState.AddModelError("Photo", "You must choose an image");
			}
			if (!blog.Photo.IsImage())
			{
				ModelState.AddModelError("Photo", "File type must be an image");
				return View();
			}
			if (blog.Photo.IsMoreThan2Mb())
			{
				ModelState.AddModelError("Photo", "The size must be less than 2 mb");
				return View();
			}
			string folder = Path.Combine(_env.WebRootPath, "images");
			blog.Image = await blog.Photo.SaveFileAsync(folder);

			#endregion

			await _db.Blogs.AddAsync(blog);
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
			Blog? dbBlog = await _db.Blogs.FirstOrDefaultAsync(x => x.Id == id);
			if (dbBlog == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(dbBlog);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Blog blog)
		{
			if (id != blog.Id)
			{
				return RedirectToAction("Error");
			}
			Blog? dbBlog = await _db.Blogs.FirstOrDefaultAsync(x => x.Id == id);
			if (dbBlog == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			#region Photo

			if (blog.Photo != null)
			{
				if (!blog.Photo.IsImage())
				{
					ModelState.AddModelError("Photo", "File type must be an image");
					return View();
				}
				if (blog.Photo.IsMoreThan2Mb())
				{
					ModelState.AddModelError("Photo", "The size must be less than 2 mb");
					return View();
				}
				string folder = Path.Combine(_env.WebRootPath, "images");
				string path = Path.Combine(folder, dbBlog.Image);
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}

				dbBlog.Image = await blog.Photo.SaveFileAsync(folder);
			}
			#endregion

			dbBlog.Title = blog.Title;
			dbBlog.By = blog.By;
			dbBlog.Description = blog.Description;

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
			Blog? dbBlog = await _db.Blogs.FirstOrDefaultAsync(x => x.Id == id);
			if (dbBlog == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			if (dbBlog.IsDeactive)
			{
				dbBlog.IsDeactive = false;
			}
			else
			{
				dbBlog.IsDeactive = true;
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
