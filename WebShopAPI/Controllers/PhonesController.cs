using ClassLibraryDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly ShopDbContext _context;
        public PhonesController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: api/<PhonesController>
        [HttpGet]
        public IEnumerable<Phone> Get()
        {
            return _context.Phones.ToList();
        }

        // GET api/<PhonesController>/5
        [HttpGet("{id}")]
        public Phone Get(int id)
        {
            return _context.Phones.Find(id);
        }

        // POST api/<PhonesController>
        [HttpPost]
        public void Post(Phone value)
        {
            _context.Phones.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<PhoneseController>/5
        [HttpPut("{id}")]
        public void Put(int id, Phone value)
        {
            var phone = _context.Phones.AsNoTracking().SingleOrDefault(c => c.Id == id);
            if (phone == null) return;

            phone = value;
            phone.Id = id;

            _context.Entry(phone).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var phone = _context.Phones.Find(id);
            if (phone == null) return;
            _context.Entry(phone).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
