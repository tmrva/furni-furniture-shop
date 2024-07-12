using furni1.DataAccesLayer;
using furni1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Linq;

namespace furni1.Controllers
{
    [Authorize(Roles = "Admin, Staff, Member")]
    public class CheckoutController : Controller
    {
        #region Dependency injection

        private readonly AppDbContext _db;

        public CheckoutController(AppDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            var cartProducts = GetCartProducts();
            var quantities = GetCartQuantities();

            if (cartProducts == null || quantities == null || !cartProducts.Any() || !quantities.Any())
            {
                // Optionally handle the empty cart case here
                return RedirectToAction("Index", "Cart"); // Redirect to cart page if the cart is empty
            }

            var model = new OrderViewModel
            {
                Products = cartProducts,
                Quantities = quantities,
                CartTotal = cartProducts.Sum(p => p.Price * quantities[p.Id])
            };

            return View(model);
        }
        #endregion

        #region PlaceOrder

        [HttpPost]
        public IActionResult PlaceOrder([FromBody] OrderViewModel model)
        {
            var order = new Order
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                StateCountry = model.StateCountry,
                PostalZip = model.PostalZip,
                OrderNotes = model.OrderNotes,
                TotalAmount = model.CartTotal,
                OrderDate = DateTime.Now,
                OrderDetails = new List<OrderDetail>()
            };

            foreach (var product in model.Products)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = product.Id,
                    Quantity = model.Quantities[product.Id],
                    UnitPrice = product.Price
                };
                order.OrderDetails.Add(orderDetail);
            }

            _db.Orders.Add(order);
            _db.SaveChanges();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = Url.Action("ThankYou", "Checkout", null, Request.Scheme),
                CancelUrl = Url.Action("Error", "Checkout", null, Request.Scheme)
            };

            foreach (var product in model.Products)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(product.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name
                        }
                    },
                    Quantity = model.Quantities[product.Id]
                });
            }

            var service = new SessionService();
            Session session = service.Create(options);

            return Json(new { id = session.Id });
        }
        #endregion

        #region ThankYou
        public IActionResult ThankYou()
        {
            return View();
        }
        #endregion

        #region Error
        public IActionResult Error()
        {
            return View();
        }
        #endregion

        #region GetCartProducts
        private List<furni1.Models.Product> GetCartProducts()
        {
            var productIds = HttpContext.Session.GetObjectFromJson<List<int>>("CartProductIds");
            if (productIds == null)
            {
                return new List<furni1.Models.Product>();
            }

            return _db.Products.Where(p => productIds.Contains(p.Id)).ToList();
        }
        #endregion

        #region GetCartQuantities
        private Dictionary<int, int> GetCartQuantities()
        {
            var quantities = HttpContext.Session.GetObjectFromJson<Dictionary<int, int>>("CartQuantities");
            if (quantities == null)
            {
                return new Dictionary<int, int>();
            }

            return quantities;
        }
        #endregion
    }
}
