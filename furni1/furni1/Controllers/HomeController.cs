using furni1.DataAccesLayer;
using furni1.Models;
using furni1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace furni1.Controllers
{
    public class HomeController : Controller
    {
        #region Dependency injection

        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
		{
			HomeVM homeVM = new HomeVM
			{
				ContentHome = await _db.ContentHomes.FirstOrDefaultAsync(),
				ContentProduct = await _db.ContentProducts.FirstOrDefaultAsync(),
				WhyChooseUs = await _db.WhyChooseUs.FirstOrDefaultAsync(),
				WeHelpYou = await _db.WeHelpYous.FirstOrDefaultAsync(),
				Products = await _db.Products.OrderByDescending(x => x.Id).Where(x => !x.IsDeactive).Take(3).ToListAsync(),
				TeamMembers = await _db.TeamMembers.Where(x => !x.IsDeactive).ToListAsync(),
				Blogs = await _db.Blogs.OrderByDescending(x => x.Id).Where(x => !x.IsDeactive).Take(3).ToListAsync(),
				Testimonials = await _db.Testimonials.OrderByDescending(x => x.Id).Where(x => !x.IsDeactive).ToListAsync(),
			};
			return View(homeVM);
		}
		#endregion

		#region Error
		public IActionResult Error()
		{
			return View();
		}
		#endregion
	}
}
