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
using System.Reflection.Metadata;

namespace furni1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Staff")]
    public class MessagesController : Controller
    {
        #region Dependency injection

        private readonly AppDbContext _db;
        private readonly IEmailSender _emailSender;

        public MessagesController(AppDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
		{
			return View(await _db.Messages
                .OrderBy(x => x.IsReplied)
				.ToListAsync());
		}
		#endregion

		#region Details
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			Message? dbMessage = await _db.Messages.FirstOrDefaultAsync(x => x.Id == id);
			if (dbMessage == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(dbMessage);
		}
		#endregion

		#region SendEmail

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> SendEmail(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Error");
			}
			Message? dbMessage = await _db.Messages.FirstOrDefaultAsync(x => x.Id == id);
			if (dbMessage == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}
			return View(dbMessage);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]

		public async Task<IActionResult> SendEmail(int id, Message message1, string main, string email, string subject, string message)
		{
			if (id != message1.Id)
			{
				return RedirectToAction("Error");
			}
			Message? dbMessage = await _db.Messages.FirstOrDefaultAsync(x => x.Id == id);
			if (dbMessage == null)
			{
				return RedirectToAction("ErrorBadRequest");
			}

			main = "your-mail";
			email = Request.Form["email"];
			subject = Request.Form["subject"];
			message = Request.Form["text"];

			await _emailSender.SendEmailAsync(main, email, subject, message);

			dbMessage.IsReplied = true;
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
