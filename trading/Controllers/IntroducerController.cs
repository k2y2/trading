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
    public class IntroducerController : Controller
    {
        private readonly tradingContext _context;

        public IntroducerController(tradingContext context)
        {
            _context = context;
        }

        // GET: Introducer
        public async Task<IActionResult> Index(string introducerName, string introducerLegalName)
        {
            ViewBag.introducerName = introducerName;
            ViewBag.introducerLegalName = introducerLegalName;

            var introducer = await _context.IntroducerView
                .Where(m =>
                  (introducerName == null || m.IntroducerName.ToUpper().Contains(introducerName.ToUpper())) &&
                  (introducerLegalName == null || m.IntroducerLegalName.ToUpper().Contains(introducerLegalName.ToUpper()))  
                ).OrderBy(m => m.IntroducerName).ToListAsync();

            return View(introducer);
        }

        // GET: Introducer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var introducer = await _context.IntroducerView
                .FirstOrDefaultAsync(m => m.id == id);
            if (introducer == null)
            {
                return NotFound();
            }

            return View(introducer);
        }

        [HttpGet]
        public JsonResult IsExist(string IntroducerName, int id)
        {
            return Json(!_context.Introducer.Any(m => m.IntroducerName == IntroducerName && m.id != id));
        }

        // GET: Introducer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Introducer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,IntroducerName,IntroducerLegalName,DateTimeModified,DateTimeAdded")] Introducer introducer)
        {
            if (ModelState.IsValid)
            {
                introducer.DateTimeModified = Utility.GetLocalDateTime();
                introducer.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(introducer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(introducer);
        }

        // GET: Introducer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var introducer = await _context.Introducer.FindAsync(id);
            if (introducer == null)
            {
                return NotFound();
            }
            return View(introducer);
        }

        // POST: Introducer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,IntroducerName,IntroducerLegalName,DateTimeModified,DateTimeAdded")] Introducer introducer)
        {
            if (id != introducer.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    introducer.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(introducer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntroducerExists(introducer.id))
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
            return View(introducer);
        }

        // GET: Introducer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var introducer = await _context.IntroducerView
                .FirstOrDefaultAsync(m => m.id == id);
            if (introducer == null)
            {
                return NotFound();
            }

            return View(introducer);
        }

        // POST: Introducer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var introducer = await _context.Introducer.FindAsync(id);
            _context.Introducer.Remove(introducer);

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            //remove associated map - Client
            var clientTradingProfileIntroducerMap = await _context.ClientTradingProfileIntroducerMap
                .Where(m => m.IntroducerID == id).ToListAsync();

            _context.ClientTradingProfileIntroducerMap.RemoveRange(clientTradingProfileIntroducerMap);

            //remove associated map - Provider
            var providerTradingProfileIntroducerMap = await _context.ProviderTradingProfileIntroducerMap
                .Where(m => m.IntroducerID == id).ToListAsync();

            _context.ProviderTradingProfileIntroducerMap.RemoveRange(providerTradingProfileIntroducerMap);

            //remove associated bank
            var introducerBankAccount = await _context.IntroducerBankAccount
                .Where(m => m.IntroducerID == id).ToListAsync();

            _context.IntroducerBankAccount.RemoveRange(introducerBankAccount);
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntroducerExists(int id)
        {
            return _context.Introducer.Any(e => e.id == id);
        }
    }
}
