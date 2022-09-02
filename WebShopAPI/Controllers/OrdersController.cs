using ClassLibraryDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ShopDbContext _context;
        public OrdersController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _context.Orders.ToList();            
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            return _context.Orders.Find(id);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public void Post(Order value)
        {
            _context.Orders.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, Order value)
        {
            var order = _context.Orders.AsNoTracking().SingleOrDefault(c => c.Id == id);
            if (order == null) return;

            order = value;
            order.Id = id;

            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return;
            _context.Entry(order).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
