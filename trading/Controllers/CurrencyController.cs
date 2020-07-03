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
    public class CurrencyController : Controller
    {
        private readonly tradingContext _context;

        public CurrencyController(tradingContext context)
        {
            _context = context;
        }

        // GET: Currency
        public async Task<IActionResult> Index()
        {
            return View(await _context.Currency.OrderBy(m => m.CurrencyName).ToListAsync());
        }

        // GET: Currency/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currency
                .FirstOrDefaultAsync(m => m.id == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        [HttpGet]
        public JsonResult IsExist(string CurrencyName, byte id)
        {
            return Json(!_context.Currency.Any(x => x.CurrencyName == CurrencyName && x.id != id));
        }

        // GET: Currency/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Currency/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,CurrencyName,DateTimeModified,DateTimeAdded")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                currency.DateTimeModified = Utility.GetLocalDateTime();
                currency.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(currency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        // GET: Currency/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currency.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

        // POST: Currency/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("id,CurrencyName,DateTimeModified,DateTimeAdded")] Currency currency)
        {
            if (id != currency.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    currency.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(currency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyExists(currency.id))
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
            return View(currency);
        }

        // GET: Currency/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currency
                .FirstOrDefaultAsync(m => m.id == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // POST: Currency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var currency = await _context.Currency.FindAsync(id);
            _context.Currency.Remove(currency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurrencyExists(byte id)
        {
            return _context.Currency.Any(e => e.id == id);
        }
    }
}
