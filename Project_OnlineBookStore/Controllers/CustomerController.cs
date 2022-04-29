using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_OnlineBookStore.Data;
using Project_OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_OnlineBookStore.Controllers
{ 
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext context;
        public static Orders od;
        List<Orders> li = new List<Orders>();
        public CustomerController(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        public IActionResult Index()
        {
            var books =context.Book.ToList();
            //ViewBag.books = books;
            return View(books);
        }
        public IActionResult Order()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Order(int BID)
        {
            var b = context.Book.Where(b => b.BId == BID).SingleOrDefault();
            return View(b);
        }
        [HttpPost]
        public IActionResult Order(int qty, int id)
        {

            var prod = context.Book.Where(b => b.BId == id).SingleOrDefault();
            Orders od = new Orders();
            if (prod != null)
            {

                od.BName = prod.BName;
                od.BId = prod.BId;
                od.Quantity = qty;
                od.BPrice = prod.BPrice;
                od.TotalBill = od.Quantity * od.BPrice;
                // ViewBag.Order=od;
                HttpContext.Session.SetString("order", JsonConvert.SerializeObject(od));

                return RedirectToAction("Cart");
            }
            //return RedirectToAction("Cart");
            return View();
        }
        [HttpGet]
        public IActionResult Cart()
        {
            var data = HttpContext.Session.GetString("order");
            Orders o = JsonConvert.DeserializeObject<Orders>(data);
            ViewBag.obj = o;
            return View(o);
        }
        [HttpPost]
        public IActionResult Cart(Orders ordered)
        {
            ordered.UId = (int)HttpContext.Session.GetInt32("UserId");
         //   ordered.OrderDate = Convert.ToDateTime.(ToShortDateString;
            context.Orders.Add(ordered);
            int r = context.SaveChanges();

            if (r == 1)
            {
                ViewBag.OrderPlaced = "<script> alert('Order Placed!') </script>";
                return RedirectToAction("ConfirmOrder");
            }
            else
            {
                ViewBag.OrderPlaced = "<script> alert('Failed to placed!') </script>";
                return View();
            }

        }
        public IActionResult ConfirmOrder()
        {
            int id = (int)HttpContext.Session.GetInt32("UserId");
            var result = context.Orders.Where(x => x.UId == id).OrderByDescending(y=>y.OId).Take(1);
            return View(result);
        }


        [HttpPost]
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }


    }
}
  