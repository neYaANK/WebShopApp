using ClassLibraryDB;
using System.Collections.Generic;
using System.Linq;
using WebShopApp.Data.Interfaces;
using WebShopApp.Models;

namespace WebShopApp.Data
{
    public class CategoriesRepository : IRepository<Category> 
    {
        ShopDbContext _ctx;
        public CategoriesRepository(ShopDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Create(Category item)
        {
            _ctx.Categories.Add(item);
        }

        public void Delete(int id)
        {
            _ctx.Categories.Remove(_ctx.Categories.Find(id));
        }

        public Category Get(int id)
        {
            return _ctx.Categories.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _ctx.Categories.Select(c=>c).ToList();
        }

        public void Update(Category item)
        {
            
        }
    }
}
