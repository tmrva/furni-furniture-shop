using furni1.DataAccesLayer;
using furni1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace furni1.Controllers
{
    [Authorize(Roles = "Admin, Staff, Member")]
    public class CartController : Controller
    {
        #region Dependency injection

        private readonly AppDbContext _db;

        public CartController(AppDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var model = new CartViewModel
            {
                Products = cartItems.Select(c => _db.Products.Find(c.ProductId)).ToList(),
                Quantities = cartItems.ToDictionary(c => c.ProductId, c => c.Quantity),
                CartTotal = cartItems.Sum(c => c.Price * c.Quantity)
            };

            return View(model);
        }
        #endregion

        #region AddToCart

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var product = _db.Products.Find(productId);
            var cartItem = cartItems.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem == null)
            {
                cartItems.Add(new CartItem { ProductId = productId, Price = product.Price, Quantity = 1 });
            }
            else
            {
                cartItem.Quantity++;
            }

            // Update session
            HttpContext.Session.SetObjectAsJson("Cart", cartItems);
            HttpContext.Session.SetObjectAsJson("CartProductIds", cartItems.Select(c => c.ProductId).ToList());
            HttpContext.Session.SetObjectAsJson("CartQuantities", cartItems.ToDictionary(c => c.ProductId, c => c.Quantity));

            return RedirectToAction("Index");
        }
        #endregion

        #region UpdateCart

        [HttpPost]
        public IActionResult UpdateCart(int productId, int quantity)
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var cartItem = cartItems.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
            }

            // Update session
            HttpContext.Session.SetObjectAsJson("Cart", cartItems);
            HttpContext.Session.SetObjectAsJson("CartProductIds", cartItems.Select(c => c.ProductId).ToList());
            HttpContext.Session.SetObjectAsJson("CartQuantities", cartItems.ToDictionary(c => c.ProductId, c => c.Quantity));

            var cartTotal = cartItems.Sum(c => c.Price * c.Quantity);

            return Json(new { success = true, cartTotal });
        }
        #endregion

        #region RemoveFromCart

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var cartItem = cartItems.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem != null)
            {
                cartItems.Remove(cartItem);
            }

            // Update session
            HttpContext.Session.SetObjectAsJson("Cart", cartItems);
            HttpContext.Session.SetObjectAsJson("CartProductIds", cartItems.Select(c => c.ProductId).ToList());
            HttpContext.Session.SetObjectAsJson("CartQuantities", cartItems.ToDictionary(c => c.ProductId, c => c.Quantity));

            var cartTotal = cartItems.Sum(c => c.Price * c.Quantity);

            return Json(new { success = true, cartTotal });
        }
        #endregion
    }
    public class CartItem
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
