using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using trading.Class;
using trading.Models;

namespace trading.Controllers
{
    public class UserController : Controller
    {
        private readonly tradingContext _context;
        private readonly List<SelectListItem> Role = new List<SelectListItem>()
            {
                new SelectListItem { Text = "R", Value = "R" },
                new SelectListItem { Text = "A", Value = "A" },
                new SelectListItem { Text = "S", Value = "S" },
            }; 

        public UserController(tradingContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") == "S")
                return View(await _context.User.ToListAsync()); 

            return RedirectToAction("Logout", "Home");
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("Role") == "S")
                return View(user);

            return RedirectToAction("Logout", "Home");
        }

        // GET: User/Create
        public IActionResult Create()
        { 
            ViewBag.Role = Role;

            if (HttpContext.Session.GetString("Role") == "S")
                return View();

            return RedirectToAction("Logout", "Home"); 
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,UserName,Password,Role,DateTimeModified,DateTimeAdded")] User user)
        {
            if (ModelState.IsValid)
            {
                user.DateTimeModified = Utility.GetLocalDateTime();
                user.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Role = Role;  
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.Role = new SelectList(Role, "Value", "Text", user.Role);

            if (HttpContext.Session.GetString("Role") == "S")
                return View(user);

            return RedirectToAction("Logout", "Home"); 
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,UserName,Password,Role,DateTimeModified,DateTimeAdded")] User user)
        {
            if (id != user.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.id))
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
            ViewBag.Role = new SelectList(Role, "Value", "Text", user.Role);
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetString("Role") == "S")
                return View(user);

            return RedirectToAction("Logout", "Home"); 
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.id == id);
        }
    }
}
