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
        public IActionResult Login(Users userlist)
        {
            using (_db)
            {
                var user = _db.Userss.Single(u => u.UserName == userlist.UserName && u.Password == userlist.Password);
                if (user != null)
                {
                    /*ISession session;
                    session["UserName"] = user.uname.ToString();*/
                    return RedirectToAction("UserDashbord");

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
      
        public IActionResult Register(Users ulist)
        {
            if (ModelState.IsValid)
            {
                using (_db)
                {
                    _db.Userss.Add(ulist);
                    _db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = ulist.UserName + " " + " Registered";
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
   



