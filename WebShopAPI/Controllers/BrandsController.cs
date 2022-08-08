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
    public class BrandsController : ControllerBase
    {
        private readonly ShopDbContext _context;
        public BrandsController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: api/<BrandsController>
        [HttpGet]
        public IEnumerable<Brand> Get()
        {
            return _context.Brands.ToList();
        }

        // GET api/<BrandsController>/5
        [HttpGet("{id}")]
        public async Task<Brand> Get(int id)
        {
            return await _context.Brands.FindAsync(id);
        }

        // POST api/<BrandsController>
        [HttpPost]
        public void Post(Brand value)
        {
            _context.Brands.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<BrandsController>/5
        [HttpPut("{id}")]
        public void Put(int id, Brand value)
        {
            var brand = _context.Brands.AsNoTracking().SingleOrDefault(c => c.Id == id);
            if (brand == null) return;

            brand = value;
            brand.Id = id;

            _context.Entry(brand).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var brand =  _context.Brands.Find(id);
            if (brand == null) return;
            _context.Entry(brand).State = EntityState.Deleted;
            _context.SaveChanges();
        }

    }
}
