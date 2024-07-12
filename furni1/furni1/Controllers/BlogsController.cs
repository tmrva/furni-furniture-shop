using furni1.DataAccesLayer;
using furni1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace furni1.Controllers
{
	public class BlogsController : Controller
	{
        #region Dependency injection

        private readonly AppDbContext _db;
        public BlogsController(AppDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index(int page = 1)
		{
			int take = 6;
			ViewBag.PagesCount = Math.Ceiling((decimal)await _db.Blogs.CountAsync() / take);
			if (page <= 0 || page > ViewBag.PagesCount)
			{
				return RedirectToAction("Error");
			}
			List<Blog> blogs = await _db.Blogs
				.OrderByDescending(x => x.Id)
                .Where(x => !x.IsDeactive)
                .Skip((page - 1) * take)
				.Take(take)
				.ToListAsync();
			ViewBag.CurrentPage = page;
			return View(blogs);
		}
		#endregion

		#region Detail
		public async Task<IActionResult> Detail(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			Blog? blog = await _db.Blogs.FirstOrDefaultAsync(x => x.Id == id);
			if (blog == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(blog);
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
