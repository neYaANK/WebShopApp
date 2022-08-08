using ClassLibraryDB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebShopAPI
{
    public class ShopDbContext : IdentityDbContext<User>
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Country> Countries { get; set; }
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);
            SeedCategory(modelBuilder);
            SeedBrand(modelBuilder);
            SeedCountries(modelBuilder);
            SeedPhone(modelBuilder);
        }
        private void SeedCountries(ModelBuilder modelBuilder)
        {
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
                new Country{Id=14,Name="Korea"}
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
        private void SeedCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                    new Category(){Id = 1, Name="iOS",Description = "iOS"},
                    new Category(){Id = 2, Name="Android",Description = "Android"}
            });
        }
        private void SeedBrand(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasData(new Brand[]
            {
                    new Brand(){Id = 1, Name="Apple",Description = "Apple"},
                    new Brand(){Id = 2, Name="Samsung",Description = "Samsung"},
                    new Brand(){Id = 3, Name="Xiaomi",Description = "Xiaomi"}
            });
        }
        private void SeedPhone(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>().HasData(new Phone[]
            {
                    new Phone(){Id = 1, PhoneModel="iPhone 10",PhoneDescription = "iPhone 10",CountPhones=5,Price=1000,PhoneImage="/Images/Phones/iPhone10.jpg",BrandId=1,CategoryId=1,CountryId=2},
                    new Phone(){Id = 2, PhoneModel="Samsung A72",PhoneDescription = "Samsung A72",CountPhones=6,Price=200,PhoneImage="/Images/Phones/SamsungA72.jpg",BrandId=2,CategoryId=2,CountryId=14},
                    new Phone(){Id = 3, PhoneModel="Samsung A9",PhoneDescription = "Samsung A9",CountPhones=10,Price=180,PhoneImage="/Images/Phones/SamsungA9.jpg",BrandId=2,CategoryId=2,CountryId=14},
                    new Phone(){Id = 4, PhoneModel="Samsung S22",PhoneDescription = "Samsung S22",CountPhones=5,Price=1000,PhoneImage="/Images/Phones/SamsungS22.jpg",BrandId=2,CategoryId=2,CountryId=14},
                    new Phone(){Id = 5, PhoneModel="Mi 11",PhoneDescription = "Mi 11",CountPhones=10,Price=150,PhoneImage="/Images/Phones/Mi11.jpg",BrandId=3,CategoryId=2,CountryId=9}
            });
        }
    }
}
