using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using furni1.Models;
using furni1.ViewModels;
using System.Linq;
using furni1.DataAccesLayer;

namespace furni1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Staff")]
    public class DashboardController : Controller
    {
        #region Dependency Injection

        private readonly AppDbContext _db;
        public DashboardController(AppDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            var totalUsers = _db.Users.Count();
            var activeProducts = _db.Products.Count(p => !p.IsDeactive);
            var totalOrdersAmount = _db.Orders.Sum(o => o.TotalAmount);
            var totalOrdersCount = _db.Orders.Count();
            var unreadMessages = _db.Messages.Count(p => !p.IsReplied);

            var dashboardVM = new DashboardVM
            {
                TotalUsers = totalUsers,
                ActiveProducts = activeProducts,
                TotalOrdersAmount = totalOrdersAmount,
                TotalOrdersCount = totalOrdersCount,
                UnreadMessages = unreadMessages
            };

            return View(dashboardVM);
        }
        #endregion
    }
}
