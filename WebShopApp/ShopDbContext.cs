using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopApp.Models;

namespace WebShopApp
{
    public class ShopDbContext : IdentityDbContext<User>
    {

        public DbSet<Phone> Phones { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Country> Countries { get; set; }
        //public DbSet<Order> Orders { get; set; }
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);

            modelBuilder.Entity<Country>().HasData(new Country[] {
                new Country{Id=1,Name="Ukraine"},
                new Country{Id=2,Name="USA"},
                new Country{Id=3,Name="Austria"},
                new Country{Id=4,Name="Germany"},
                new Country{Id=5,Name="France"},
                new Country{Id=6,Name="Switzerland"},
                new Country{Id=7,Name="Finland"},
                new Country{Id=8,Name="Netherlands"},
                new Country{Id=9,Name="China"},
                new Country{Id=10,Name="Japan"},
                new Country{Id=11,Name="Russia"},
                new Country{Id=12,Name="Poland"},
                new Country{Id=13,Name="Hungary"},


            });

        }
        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole[]
            {
                    new IdentityRole(){Id=Guid.NewGuid().ToString(),Name="Admin",NormalizedName = "Admin"},
                    new IdentityRole(){Id=Guid.NewGuid().ToString(),Name="User",NormalizedName = "User"}
            });
        }
    }
}
