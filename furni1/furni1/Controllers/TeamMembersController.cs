using furni1.DataAccesLayer;
using furni1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace furni1.Controllers
{
    public class TeamMembersController : Controller
    {
        #region Dependency injection

        private readonly AppDbContext _db;
        public TeamMembersController(AppDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
		{
			List<TeamMember> teamMembers = await _db.TeamMembers.
				OrderByDescending(x => x.Id).
				Where(x => !x.IsDeactive).
				ToListAsync();
			return View(teamMembers);
		}
		#endregion

		#region Detail
		public async Task<IActionResult> Detail(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			TeamMember? teamMember = await _db.TeamMembers.FirstOrDefaultAsync(x => x.Id == id);
			if (teamMember == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(teamMember);
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
