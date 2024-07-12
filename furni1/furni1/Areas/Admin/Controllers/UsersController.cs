using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using furni1.DataAccesLayer;
using furni1.ViewModels;
using Microsoft.AspNetCore.Identity;
using furni1.Models;
using furni1.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace furni1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Staff")]
    public class UsersController : Controller
    {
        #region Dependency injection

        private readonly UserManager<AppUser> _userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
		{
			List<AppUser> users;

			if (User.IsInRole("Admin"))
			{
				users = await _userManager.Users.OrderBy(x => x.UserName).ToListAsync();
			}
			else
			{
				users = await _userManager.Users.Where(x => !x.IsDeactive).OrderBy(x => x.UserName).ToListAsync();
			}

			List<UserVM> userVMs = new List<UserVM>();

			foreach (AppUser user in users)
			{
				UserVM userVM = new UserVM
				{
					Id = user.Id,
					Name = user.Name,
					Surname = user.Surname,
					Username = user.UserName,
					Email = user.Email,
					IsDeactive = user.IsDeactive,
					Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
				};
				userVMs.Add(userVM);
			}
			return View(userVMs);
		}
		#endregion

		#region Create

		[Authorize(Roles = "Admin")]
		public IActionResult Create()
		{
			ViewBag.Roles = new List<string>
			{
				Roles.Admin,
				Roles.Member,
				Roles.Staff,
			};
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]

		public async Task<IActionResult> Create(RegisterVM registerVM, string role)
		{
			ViewBag.Roles = new List<string>
			{
				Roles.Admin,
				Roles.Member,
				Roles.Staff,
			};
			AppUser user = new AppUser
			{
				Name = registerVM.Name,
				Surname = registerVM.Surname,
				UserName = registerVM.Username,
				Email = registerVM.Email,
			};

			IdentityResult identityResult = await _userManager.CreateAsync(user, registerVM.Password);

			if (!identityResult.Succeeded)
			{
				foreach (IdentityError error in identityResult.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
				return View(registerVM);
			}
			await _userManager.AddToRoleAsync(user, role);
			return RedirectToAction("Index");
		}
		#endregion

		#region Edit

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			AppUser? user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			EditVM dbEditVM = new EditVM
			{
				Name = user.Name,
				Surname = user.Surname,
				Username = user.UserName,
				Email = user.Email,
				Role = (await _userManager.GetRolesAsync(user))[0],
			};

			ViewBag.Roles = new List<string>
			{
				Roles.Admin,
				Roles.Member,
				Roles.Staff,
			};

			return View(dbEditVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]

		public async Task<IActionResult> Edit(string id, EditVM editVM, string role)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			AppUser? user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			EditVM dbEditVM = new EditVM
			{
				Name = user.Name,
				Surname = user.Surname,
				Username = user.UserName,
				Email = user.Email,
				Role = (await _userManager.GetRolesAsync(user))[0],
			};

			ViewBag.Roles = new List<string>
			{
				Roles.Admin,
				Roles.Member,
				Roles.Staff,
			};

			user.Name = editVM.Name;
			user.Surname = editVM.Surname;
			user.UserName = editVM.Username;
			user.Email = editVM.Email;

			if (dbEditVM.Role != role)
			{
				IdentityResult addIdentityResult = await _userManager.AddToRoleAsync(user, role);
				if (!addIdentityResult.Succeeded)
				{
					foreach (IdentityError error in addIdentityResult.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
					return View(dbEditVM);
				}

				IdentityResult removeIdentityResult = await _userManager.RemoveFromRoleAsync(user, dbEditVM.Role);
				if (!removeIdentityResult.Succeeded)
				{
					foreach (IdentityError error in removeIdentityResult.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
					return View(dbEditVM);
				}
			}

			await _userManager.UpdateAsync(user);
			return RedirectToAction(nameof(Index));
		}
		#endregion

		#region ResetPassword

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> ResetPassword(string id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			AppUser? user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]

		public async Task<IActionResult> ResetPassword(string id, ResetPasswordVM resetPasswordVM)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			AppUser? user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			string token = await _userManager.GeneratePasswordResetTokenAsync(user);
			IdentityResult identityResult = await _userManager.ResetPasswordAsync(user, token, resetPasswordVM.Password);

			if (!identityResult.Succeeded)
			{
				foreach (IdentityError error in identityResult.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
				return View();
			}
			return RedirectToAction(nameof(Index));
		}
		#endregion

		#region Activity

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Activity(string id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			AppUser? user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			if (user.IsDeactive)
			{
				user.IsDeactive = false;
			}
			else
			{
				user.IsDeactive = true;
			}
			await _userManager.UpdateAsync(user);
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
