using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopApp.Models;
using WebShopApp.Utils;
using WebShopApp.ViewModel;

namespace WebShopApp.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly UserManager<User> _userManager;
        public CartController(ShopDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<Cart>("cart");
            if (cart != null)
            {
                ViewBag.Cart = cart;
                ViewBag.Total = cart.CartItems.Sum(item => item.Phone.Price * item.Quantity);
            }
            return View();

        }
        public IActionResult Buy(int id, int quantity = 1)
        {
            Phone phone = _context.Phones
                .Where(c => c.Id == id)
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.Country)
                .Single();
            if (HttpContext.Session.Get<Cart>("cart") == null)
            {
                Cart cart = new Cart();

                cart.CartItems.Add(new CartItem() { Phone = phone, Quantity = quantity });
                HttpContext.Session.Set("cart", cart);
            }
            else
            {
                Cart cart = HttpContext.Session.Get<Cart>("cart");
                int index = DoesExist(id, cart);
                if (index != -1)
                {
                    cart.CartItems[index].Quantity++;
                }
                else
                {
                    cart.CartItems.Add(new CartItem() { Phone = phone, Quantity = quantity });
                }
                HttpContext.Session.Set("cart", cart);
            }
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public IActionResult Remove(int id)
        {
            if (HttpContext.Session.Get<Cart>("cart") == null)
            {
                return RedirectToAction("Index");
            }
            Cart cart = HttpContext.Session.Get<Cart>("cart");
            int index = DoesExist(id, cart);
            cart.CartItems.RemoveAt(index);
            HttpContext.Session.Set("cart", cart);
            return RedirectToAction("Index");

        }
        private int DoesExist(int id, Cart cart)
        {
            for (int i = 0; i < cart.CartItems.Count; i++)
            {
                if (cart.CartItems[i].Phone.Id == id)
                {
                    return i;
                }
            }
            return -1;
        }
        public IActionResult ContactData()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactData(ContactDataViewModel contactData)
        {
            var isAllValid = ModelState.IsValid || HttpContext.Session.Get<Cart>("cart") != null;

            if (!isAllValid) return BadRequest();

            var user = await _userManager.GetUserAsync(User);
            Cart cart = HttpContext.Session.Get<Cart>("cart");
            var order = new Order() { UserId = user.Id,Address = contactData.Address,City = contactData.City, RecieverName = contactData.Name, RecieverSurname = contactData.LastName};
            order.OrderItems = new List<OrderItem>();

            foreach(var item in cart.CartItems)
            {
                order.OrderItems.Add(new OrderItem() { PhoneId = item.Phone.Id, Quantity = item.Quantity });
            }

            HttpContext.Session.Remove("cart");
            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
