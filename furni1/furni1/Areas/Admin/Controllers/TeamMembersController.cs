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
    public class TeamMembersController : Controller
    {
        #region Dependency injection

        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public TeamMembersController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
		{
			List<TeamMember> teamMembers;

			if (User.IsInRole("Admin"))
			{
				teamMembers = await _db.TeamMembers.OrderByDescending(x => x.Id).ToListAsync();
			}
			else
			{
				teamMembers = await _db.TeamMembers.Where(x => !x.IsDeactive).OrderByDescending(x => x.Id).ToListAsync();
			}
			return View(teamMembers);
		}
		#endregion

		#region Details
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			TeamMember? dbTeamMember = await _db.TeamMembers.FirstOrDefaultAsync(x => x.Id == id);
			if (dbTeamMember == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(dbTeamMember);
		}
		#endregion

		#region Create

		[Authorize(Roles = "Admin")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]

		public async Task<IActionResult> Create(TeamMember teamMember)
		{
			#region Photo

			if (teamMember.Photo == null)
			{
				ModelState.AddModelError("Photo", "You must choose an image");
			}
			if (!teamMember.Photo.IsImage())
			{
				ModelState.AddModelError("Photo", "File type must be an image");
				return View();
			}
			if (teamMember.Photo.IsMoreThan2Mb())
			{
				ModelState.AddModelError("Photo", "The size must be less than 2 mb");
				return View();
			}
			string folder = Path.Combine(_env.WebRootPath, "images");
			teamMember.Image = await teamMember.Photo.SaveFileAsync(folder);

			#endregion

			await _db.TeamMembers.AddAsync(teamMember);
			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		#endregion

		#region Edit

		[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error");
            }
            TeamMember? dbTeamMember = await _db.TeamMembers.FirstOrDefaultAsync(x => x.Id == id);
            if (dbTeamMember == null)
            {
                return RedirectToAction("ErrorBadRequest");
            }
            return View(dbTeamMember);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id, TeamMember teamMember)
        {
            if (id != teamMember.Id)
            {
                return RedirectToAction("Error");
            }
            TeamMember? dbTeamMember = await _db.TeamMembers.FirstOrDefaultAsync(x => x.Id == id);
            if (dbTeamMember == null)
            {
                return RedirectToAction("ErrorBadRequest");
            }

			#region Photo

			if (teamMember.Photo != null)
			{
				if (!teamMember.Photo.IsImage())
				{
					ModelState.AddModelError("Photo", "File type must be an image");
					return View();
				}
				if (teamMember.Photo.IsMoreThan2Mb())
				{
					ModelState.AddModelError("Photo", "The size must be less than 2 mb");
					return View();
				}
				string folder = Path.Combine(_env.WebRootPath, "images");
				string path = Path.Combine(folder, dbTeamMember.Image);
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}

				dbTeamMember.Image = await teamMember.Photo.SaveFileAsync(folder);
			}
			#endregion

			dbTeamMember.Name = teamMember.Name;
            dbTeamMember.LastName = teamMember.LastName;
            dbTeamMember.Profession = teamMember.Profession;
            dbTeamMember.Description = teamMember.Description;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
		#endregion

		#region Activity

		[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error");
            }
            TeamMember? dbTeamMember = await _db.TeamMembers.FirstOrDefaultAsync(x => x.Id == id);
            if (dbTeamMember == null)
            {
                return RedirectToAction("ErrorBadRequest");
            }

            if (dbTeamMember.IsDeactive)
            {
                dbTeamMember.IsDeactive = false;
            }
            else
            {
                dbTeamMember.IsDeactive = true;
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
