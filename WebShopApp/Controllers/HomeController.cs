using ClassLibraryDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebShopApp.Models;
using WebShopApp.ViewModel;

namespace WebShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ILogger<HomeController> logger, ShopDbContext context, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = accessor;
        }
        [Authorize(Roles = "User")]
        public IActionResult Index(string query, int brandId = 0, int categoryId = 0, int countryId = 0, int page = 1)
        {
            int psize = 1;
            //FileStream fileStream = new FileStream("appsettings.json", FileMode.Open);
            //var result = "";
            //using (StreamReader rdr = new StreamReader(fileStream))
            //{
            //    result = rdr.ReadToEnd();
            //}
            //dynamic res = JsonConvert.DeserializeObject(result);
            //ViewData["appSettings"] = res["Logging"]["LogLevel"]["Microsoft"];



            string psizeCookie = _httpContextAccessor.HttpContext.Request.Cookies["pageSize"];
            if (string.IsNullOrWhiteSpace(psizeCookie)||psizeCookie=="undefined") _httpContextAccessor.HttpContext.Response.Cookies.Append("pageSize", psize.ToString());
            else psize = int.Parse(psizeCookie);


            PhonesViewModel model = new PhonesViewModel();

            IQueryable<Phone> items = _context.Phones;

            if (brandId > 0) items = items.Where(c => c.BrandId == brandId);
            if (categoryId > 0) items = items.Where(c => c.CategoryId == categoryId);
            if (countryId > 0) items = items.Where(c => c.CountryId == countryId);

            if (!String.IsNullOrWhiteSpace(query)) items = _context.Phones.Where(c => c.PhoneModel.Contains(query) || c.PhoneDescription.Contains(query));



            model.Phones = items
                .Skip((page - 1) * psize)
                .Take(psize)
                .Include(c => c.Category)
                .Include(c => c.Brand)
                .Include(c => c.Country)
                .ToList();
            PageViewModel pvm = new PageViewModel(items.Count(), page, psize, query, brandId, categoryId, countryId);
            model.PageViewModel = pvm;

            ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name");
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            ViewBag.Countries = new SelectList(_context.Countries.ToList(), "Id", "Name");


            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Contacts()
        {
            return View();
        }

    }
}
