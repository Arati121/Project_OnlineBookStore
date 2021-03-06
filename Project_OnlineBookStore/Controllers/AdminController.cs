using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_OnlineBookStore.Data;
using Project_OnlineBookStore.Models;

namespace Project_OnlineBookStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Book.ToListAsync());
        }
        public IActionResult AllBooksList()
        {
            var books = _context.Book.ToList();
            var cat = _context.Categories.ToList();
            foreach(Book item in books)
            {
                foreach(Category c in cat)
                {
                    if(c.CatId==item.CatId)
                    {
                        item.CatName = c.CatName;
                    }
                }
            }
            return View(books);
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.BId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            var cat = _context.Categories.ToList();
            ViewBag.Category = new SelectList(cat, "CatId", "CatName");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*  public async Task<IActionResult> Create([Bind("BId,BName,BInfo,BPrice,BAuthor,image,CatId")] Book book)
          {
              if (ModelState.IsValid)
              {

                  _context.Add(book);
                  await _context.SaveChangesAsync();
                  return RedirectToAction(nameof(Index));
              }
              return View(book);
          }
        */
        public async Task<IActionResult> Create(IFormFile file, [Bind("BId,BName,BInfo,BPrice,BAuthor,image,CatId")] Book book)
        {
            if (file != null)
            {
                string filename = file.FileName;
                //  string  ext = Path.GetExtension(file.FileName);
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                book.image = filename;
            }

            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
         
        //catalog

        public async Task<IActionResult> Catalogue()
        {
            return View(await _context.Book.ToListAsync());
        }
        // GET: Admin/Edit/5


        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        /* var book = _context.Book.Where(b => b.BId == b.BId).SingleOrDefault();
          var cat = _context.Categories.ToList();
          ViewBag.Category = new SelectList(cat, "CatId", "CatName");
         */

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BId,BName,BInfo,BPrice,BAuthor,image,CatId")] Book book)
        {
            if (id != book.BId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                  //  await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
       
     /*   [HttpGet]
        public IActionResult Edit(int BId)
        {
            var prod = _context.Book.Where(p => p.BId == BId).SingleOrDefault();
            var cat = _context.Categories.ToList();

            //  var cate = context.Category.Where(c => c.CategoryId == prod.CategoryId);
            ViewBag.Category = new SelectList(cat, "CategoryId", "CategoryName");
            return View(prod);
        }
       
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            var prod = _context.Book.Where(p => p.BId == book.BId).SingleOrDefault();
            if (prod != null)
            {
                prod.BName = book.BName;
                prod.BPrice = book.BPrice;
                prod.CatId = book.CatId;

                _context.Update(prod);
                int res = _context.SaveChanges();
                if (res == 1)
                {
                    ViewBag.UpdateMessage = "< script > alert('Updated!') </ script >";
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.UpdateMessage = "< script > alert('Failed!') </ script >";
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                ViewBag.UpdateMessage = "< script > alert('Not FOund!') </ script >";
                return RedirectToAction("Index", "Admin");
            }
        }
     */

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var book = await _context.Book
        .FirstOrDefaultAsync(m => m.BId == id);
    if (book == null)
    {
        return NotFound();
    }

    return View(book);
}

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var book = await _context.Book.FindAsync(id);
    _context.Book.Remove(book);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}

private bool BookExists(int id)
{
    return _context.Book.Any(e => e.BId == id);
}
}
}
