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
    public class CurrencyPairController : Controller
    {
        private readonly tradingContext _context;

        public CurrencyPairController(tradingContext context)
        {
            _context = context;
        }

        // GET: CurrencyPair
        public async Task<IActionResult> Index()
        {
            return View(await _context.CurrencyPair.OrderBy(m => m.CurrencyPairName).ToListAsync());
        }

        // GET: CurrencyPair/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currencyPair = await _context.CurrencyPair
                .FirstOrDefaultAsync(m => m.id == id);
            if (currencyPair == null)
            {
                return NotFound();
            }

            return View(currencyPair);
        }

        [HttpGet]
        public JsonResult IsExist(string CurrencyPairName, short id)
        {
            return Json(!_context.CurrencyPair.Any(x => x.CurrencyPairName == CurrencyPairName && x.id != id));
        }

        // GET: CurrencyPair/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CurrencyPair/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,CurrencyPairName,DateTimeModified,DateTimeAdded")] CurrencyPair currencyPair)
        {
            if (ModelState.IsValid)
            {
                currencyPair.DateTimeModified = Utility.GetLocalDateTime();
                currencyPair.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(currencyPair);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(currencyPair);
        }

        // GET: CurrencyPair/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currencyPair = await _context.CurrencyPair.FindAsync(id);
            if (currencyPair == null)
            {
                return NotFound();
            }
            return View(currencyPair);
        }

        // POST: CurrencyPair/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("id,CurrencyPairName,DateTimeModified,DateTimeAdded")] CurrencyPair currencyPair)
        {
            if (id != currencyPair.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    currencyPair.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(currencyPair);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyPairExists(currencyPair.id))
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
            return View(currencyPair);
        }

        // GET: CurrencyPair/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currencyPair = await _context.CurrencyPair
                .FirstOrDefaultAsync(m => m.id == id);
            if (currencyPair == null)
            {
                return NotFound();
            }

            return View(currencyPair);
        }

        // POST: CurrencyPair/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var currencyPair = await _context.CurrencyPair.FindAsync(id);
            _context.CurrencyPair.Remove(currencyPair);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurrencyPairExists(short id)
        {
            return _context.CurrencyPair.Any(e => e.id == id);
        }
    }
}
