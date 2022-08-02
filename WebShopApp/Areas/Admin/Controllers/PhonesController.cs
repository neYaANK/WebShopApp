using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebShopApp.Models;
using WebShopApp.ViewModel;

namespace WebShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class PhonesController : Controller
    {



        ShopDbContext _db;
        IWebHostEnvironment _env;
        public PhonesController(ShopDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index(string query, int brandId = 0, int categoryId = 0, int countryId = 0, int page = 1)
        {
            PhonesViewModel model = new PhonesViewModel();

            IQueryable<Phone> items = _db.Phones;

            if (brandId > 0) items = items.Where(c => c.BrandId == brandId);
            if (categoryId > 0) items = items.Where(c => c.CategoryId == categoryId);
            if (countryId > 0) items = items.Where(c => c.CountryId == countryId);

            if (!String.IsNullOrWhiteSpace(query)) items = _db.Phones.Where(c => c.PhoneModel.Contains(query) || c.PhoneDescription.Contains(query));



            int psize = 5;
            model.Phones = items
                .Skip((page - 1) * psize)
                .Take(psize)
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.Country)
                .ToList();
            PageViewModel pvm = new PageViewModel(items.Count(), page, psize, query, brandId, categoryId, countryId);
            model.PageViewModel = pvm;

            ViewBag.Brands = new SelectList(_db.Brands.ToList(), "Id", "Name");
            ViewBag.Categories = new SelectList(_db.Categories.ToList(), "Id", "Name");
            ViewBag.Countries = new SelectList(_db.Countries.ToList(), "Id", "Name");


            return View(model);
        }
        public IActionResult AddNewPhone()
        {
            SelectList categories = new SelectList(_db.Categories.Select(c => c).ToList(), "Id", "Name");
            SelectList brands = new SelectList(_db.Brands.Select(c => c).ToList(), "Id", "Name");
            SelectList countries = new SelectList(_db.Countries.Select(c => c).ToList(), "Id", "Name");
            ViewBag.Categories = categories;
            ViewBag.Brands = brands;
            ViewBag.Countries = countries;

            //ViewBag.Countries = _db.Countries.Select(c => c).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddNewPhone(Phone phone)
        {
            if (ModelState.IsValid)
            {
                string path = "/Images/Phones/" + phone.PhoneImageFile.FileName;
                using (var fs = new FileStream(_env.WebRootPath + path, FileMode.Create))
                {
                    phone.PhoneImageFile.CopyTo(fs);
                }
                phone.PhoneImage = path;

                _db.Phones.Add(phone);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(phone);
        }


        public IActionResult EditPhone(int Id)
        {
            ViewBag.Categories = new SelectList(_db.Categories.Select(c => c).ToList(), "Id", "Name");
            SelectList brands = new SelectList(_db.Brands.Select(c => c).ToList(), "Id", "Name");
            SelectList countries = new SelectList(_db.Countries.Select(c => c).ToList(), "Id", "Name");
            ViewBag.Brands = brands;
            ViewBag.Countries = countries;
            return View(_db.Phones.Find(Id));
        }
        [HttpPost]
        public IActionResult EditPhone(Phone phone)
        {

            if (ModelState.IsValid)
            {
                var phoneOld = _db.Phones.Single(c => c.Id == phone.Id);

                System.IO.File.Delete(_env.WebRootPath + phoneOld.PhoneImage);

                string path = "/Images/Phones/" + phone.PhoneImageFile.FileName;
                using (var fs = new FileStream(_env.WebRootPath + path, FileMode.Create))
                {
                    phone.PhoneImageFile.CopyTo(fs);
                }

                _db.Phones.Update(phone);
                _db.SaveChanges();
                ViewBag.Categories = _db.Categories.Select(c => c).ToList();
                return RedirectToAction("Index");
            }
            return View(phone);
        }
        public IActionResult SinglePhone(int Id)
        {
            var phone = _db.Phones
                .Include(c => c.Country)
                .Include(c => c.Brand)
                .Include(c => c.Category)
                .SingleOrDefault(c => c.Id == Id);
            if (phone == null)
            {
                ViewBag.Success = false;
                return View();
            }
            else
            {
                ViewBag.Category = _db.Categories.Single(c => c.Id == phone.CategoryId);
                ViewBag.Success = true;
                return View(phone);
            }
        }
        public IActionResult DeletePhone(int Id)
        {
            var phone = _db.Phones.SingleOrDefault(c => c.Id == Id);
            if (phone == null) return RedirectToAction("Index");

            _db.Phones.Remove(phone);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult OnePhone()
        {
            return PartialView();
        }

        public JsonResult GetCountries(string query)
        {
            List<Country> countries = _db.Countries.Where(c => c.Name.ToLower().Contains(query.ToLower())).ToList();
            return Json(countries);
        }
    }
}
