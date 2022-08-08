using ClassLibraryDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ShopDbContext _context;
        public CategoriesController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _context.Categories.ToList();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return _context.Categories.Find(id);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public void Post(Category value)
        {
            _context.Categories.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, Category value)
        {
            var category = _context.Categories.AsNoTracking().SingleOrDefault(c => c.Id == id);
            if (category == null) return;
            
            category = value;
            category.Id = id;

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var category =  _context.Categories.Find(id);
            if (category == null) return;
            _context.Entry(category).State = EntityState.Deleted;
            _context.SaveChanges();
        }

    }
}
