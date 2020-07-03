using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using trading.Class;
using trading.Models;

namespace trading.Controllers
{
    public class SenderController : Controller
    {
        private readonly tradingContext _context;

        public SenderController(tradingContext context)
        {
            _context = context;
        }

        // GET: Sender
        public async Task<IActionResult> Index(int providerID)
        {
            ViewBag.Provider = new SelectList(_context.SenderView.ToList()
                .GroupBy(m => m.ProviderID).Select(m => m.First()).OrderBy(m => m.ProviderName), "ProviderID", "ProviderName", providerID);

            var sender = await _context.SenderView
                .Where(m =>
                  (providerID == 0 || m.ProviderID == providerID)
                ).OrderBy(m => m.ProviderName).ThenBy(m => m.SenderName).ToListAsync();

            return View(sender);
        }

        // GET: Sender/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var senderView = await _context.SenderView
                .FirstOrDefaultAsync(m => m.id == id);

            //var senderView = await (from s in _context.Sender
            //                  join p in _context.Provider on s.ProviderID equals p.id
            //                  where s.id == id

            //                  select new SenderView
            //                  {
            //                      id = s.id,
            //                      SenderName = s.SenderName,
            //                      ProviderID = s.ProviderID,
            //                      Unit = s.Unit,
            //                      AddressLine1 = s.AddressLine1,
            //                      AddressLine2 = s.AddressLine2,
            //                      City = s.City,
            //                      Country = s.Country,
            //                      PostalCode = s.PostalCode,
            //                      DirectorFirstName = s.DirectorFirstName,
            //                      DirectorLastName = s.DirectorLastName,
            //                      DateTimeModified = s.DateTimeModified,
            //                      DateTimeAdded = s.DateTimeModified,
            //                      ProviderName = p.ProviderName
            //                  }
            //       ).FirstOrDefaultAsync();

            if (senderView == null)
            {
                return NotFound();
            }

            return View(senderView);
        }

        [HttpGet]
        public JsonResult IsExist(string SenderName, int id, int ProviderID)
        {
            return Json(!_context.Sender.Any(x => x.SenderName == SenderName && x.ProviderID == ProviderID && x.id != id));
        }

        // GET: Sender/Create
        public IActionResult Create()
        {
            ViewBag.Provider = new SelectList(_context.Provider.OrderBy(m => m.ProviderName).ToList(), "id", "ProviderName");
            return View();
        }

        // POST: Sender/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,SenderName,ProviderID,Unit,AddressLine1,AddressLine2,City,Country,PostalCode,DirectorFirstName,DirectorLastName,DateTimeModified,DateTimeAdded")] Sender sender)
        {
            if (ModelState.IsValid)
            {
                sender.DateTimeModified = Utility.GetLocalDateTime();
                sender.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(sender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Provider = new SelectList(_context.Provider.OrderBy(m => m.ProviderName).ToList(), "id", "ProviderName");
            return View(sender);
        }

        // GET: Sender/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sender = await _context.Sender.FindAsync(id);
            if (sender == null)
            {
                return NotFound();
            }

            ViewBag.Provider = new SelectList(_context.Provider.OrderBy(m => m.ProviderName).ToList(), "id", "ProviderName");
            return View(sender);
        }

        // POST: Sender/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,SenderName,ProviderID,Unit,AddressLine1,AddressLine2,City,Country,PostalCode,DirectorFirstName,DirectorLastName,DateTimeModified,DateTimeAdded")] Sender sender)
        {
            if (id != sender.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sender.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(sender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SenderExists(sender.id))
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
            ViewBag.Provider = new SelectList(_context.Provider.OrderBy(m=>m.ProviderName).ToList(), "id", "ProviderName");
            return View(sender);
        }

        // GET: Sender/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var senderView = await _context.SenderView
                .FirstOrDefaultAsync(m => m.id == id);

            //var senderView = await (from s in _context.Sender
            //                        join p in _context.Provider on s.ProviderID equals p.id
            //                        where s.id == id

            //                        select new SenderView
            //                        {
            //                            id = s.id,
            //                            SenderName = s.SenderName,
            //                            ProviderID = s.ProviderID,
            //                            Unit = s.Unit,
            //                            AddressLine1 = s.AddressLine1,
            //                            AddressLine2 = s.AddressLine2,
            //                            City = s.City,
            //                            Country = s.Country,
            //                            PostalCode = s.PostalCode,
            //                            DirectorFirstName = s.DirectorFirstName,
            //                            DirectorLastName = s.DirectorLastName,
            //                            DateTimeModified = s.DateTimeModified,
            //                            DateTimeAdded = s.DateTimeModified,
            //                            ProviderName = p.ProviderName
            //                        }
            //       ).FirstOrDefaultAsync();

            if (senderView == null)
            {
                return NotFound();
            }

            return View(senderView);
        }

        // POST: Sender/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sender = await _context.Sender.FindAsync(id);
            _context.Sender.Remove(sender);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SenderExists(int id)
        {
            return _context.Sender.Any(e => e.id == id);
        }
    }
}
