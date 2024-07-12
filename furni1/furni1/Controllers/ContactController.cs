using furni1.DataAccesLayer;
using furni1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace furni1.Controllers
{
	public class ContactController : Controller
	{
        #region Dependency injection

        private readonly AppDbContext _db;
        public ContactController(AppDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
		{
			return View(await _db.Messages.FirstOrDefaultAsync());
		}
		#endregion

		#region ThankYou
		public IActionResult ThankYou()
		{
			return View();
		}
		#endregion

		#region Create
		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Create(Message message)
		{
			if (string.IsNullOrEmpty(message.Email) || string.IsNullOrEmpty(message.Subject) || string.IsNullOrEmpty(message.Text))
			{
				return BadRequest("All fields are required.");
			}
			_db.Messages.Add(message);
			await _db.SaveChangesAsync();
			return RedirectToAction("ThankYou");
		}
		#endregion

		#region ContactUs(comment)
		//public async Task<IActionResult> ContactUs(string main, string email, string subject, string message)
		//{
		//	main = "unknowngirl621n@gmail.com";
		//	email = Request.Form["email"];
		//	subject = Request.Form["subject"];
		//	message = Request.Form["message"];

		//	if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
		//	{
		//		return BadRequest("All fields are required.");
		//	}

		//	await _emailSender.SendEmailAsync(main, email, subject, message);

		//	return RedirectToAction("Index");
		//}
		#endregion
	}
}
