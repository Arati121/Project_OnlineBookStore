using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project_OnlineBookStore.Data;
using Project_OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project_OnlineBookStore.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Users user)
        {
            using (_db)
            {
                var user1 = _db.Users.Single(u => u.EmailId == user.EmailId && u.Password == user.Password);
                if (user1 != null)
                {
                    if (user1.RoleId == 1)
                    {
                       
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("UserId", user.UId);
                        return RedirectToAction("Index","Customer");
                    }
                }
            }
            return View();
        }

        public IActionResult Register()
        {
            //    ViewData["url"] = url;
            return View();
        }
        [HttpPost]
      
        public IActionResult Register(Users user)
        {
            if (ModelState.IsValid)
            {
                using (_db)
                {
                     user.RoleId = 2;
                    _db.Users.Add(user);
                    _db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = user.UserName + " " + " Registered";
            }
            return View();
        }

        public IActionResult UserDashbord()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
   



