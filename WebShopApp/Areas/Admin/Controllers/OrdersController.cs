using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebShopApp.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly ShopDbContext _context;
        public OrdersController(ShopDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }
        public IActionResult Details(int id)
        {
            var order = _context.Orders.Single(c=>c.Id==id);
            _context.Entry(order).Collection(c => c.OrderItems).Load();
            if (order == null) return NotFound();
            return View(order);
        }
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.SingleOrDefault(c => c.Id == id);
            if (order == null) return NotFound();
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
