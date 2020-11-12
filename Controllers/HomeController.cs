﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MrDriverCore.Data;
using MrDriverCore.Models;

namespace MrDriverCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDBContext _context;
        private readonly IConfiguration _config;
        private readonly IDataProtector _protector;

        public HomeController(ILogger<HomeController> logger,
            MyDBContext context,
            IConfiguration config,
            IDataProtectionProvider provider)
        {
            _logger = logger;
            _context = context;
            _config = config;
            _protector = provider.CreateProtector(_config["CryptoKey"]);
        }

        private void showViewBagMessage()
        {
            string FlashMessage = HttpContext.Session.GetString("FlashMessage");
            if (FlashMessage != null)
            {
                ViewBag.FlashMessage = FlashMessage;
                HttpContext.Session.Remove("FlashMessage");
            }
        }

        async public Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Redirect("/Home/Login");

            ViewBag.User = await _context.User
                .Include(u => u.Locations)
                .FirstOrDefaultAsync(u => u.Id == userId);

            foreach (Locations item in ViewBag.User.Locations)
            {
                try
                {
                    item.Name = _protector.Unprotect(item.Name);
                    item.Street = _protector.Unprotect(item.Street);
                }
                catch (Exception e) { Console.WriteLine(e); };
            }

            showViewBagMessage();
            return View();
        }

        [HttpGet]
        public IActionResult CreateLocation()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Redirect("/Home/Login");
            showViewBagMessage();
            return View();
        }

        [HttpPost]
        public IActionResult CreateLocation(Locations locations)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Redirect("/Home/Login");

            locations.Driver = (int)userId;
            locations.Name = _protector.Protect(locations.Name);
            locations.Street = _protector.Protect(locations.Street);

            _context.Locations.Add(locations);
            _context.SaveChanges();

            ViewBag.Message = "Location created";
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            showViewBagMessage();
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            user.Password = CryptographyService.Hash(user.Password);
            //user.Username = _protector.Protect(user.Username);
            //user.Username = _protector.Protect(user.Username);
            _context.User.Add(user);
            _context.SaveChanges();

            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Login()
        {
            showViewBagMessage();
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == null || password == null)
            {
                ViewBag.Message = "Please fill out both forms";
                return View();
            }

            //username = _protector.Protect(username);
            User user = _context.User.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                ViewBag.Message = "No such user exists (or wrong password)";
                return View();
            }
            else
            {
                if (CryptographyService.Verify(password, user.Password))
                {
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    Console.WriteLine($"User {user.Username} logged in");
                    return Redirect("/");
                }
                else
                {
                    ViewBag.Message = "Wrong password (or unknown user)";
                    return View();
                }
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Home/Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}