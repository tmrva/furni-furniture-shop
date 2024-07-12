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

namespace furni1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Staff")]

    public class OrderDetailsController : Controller
    {
        #region Dependency injection

        private readonly AppDbContext _db;
        private readonly IEmailSender _emailSender;

        public OrderDetailsController(AppDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Order> orders = await _db.Orders.OrderByDescending(x => x.OrderDate).ToListAsync();
            return View(orders);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error");
            }
            Order? dbOrder = await _db.Orders
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (dbOrder == null)
            {
                return RedirectToAction("ErrorBadRequest");
            }
            return View(dbOrder);
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
