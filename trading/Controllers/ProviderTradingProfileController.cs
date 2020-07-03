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
    public class ProviderTradingProfileController : Controller
    {
        private readonly tradingContext _context;

        public ProviderTradingProfileController(tradingContext context)
        {
            _context = context;
        }

        // GET: ProviderTradingProfile
        public async Task<IActionResult> Index(int providerID)
        {
            ViewBag.Provider = new SelectList(_context.ProviderTradingProfileView.ToList()
                   .GroupBy(m => m.ProviderID).Select(m => m.First()).OrderBy(m => m.ProviderName), "ProviderID", "ProviderName", providerID);

            var providerTradingProfile = await _context.ProviderTradingProfileView
                .Where(m =>
                  (providerID == 0 || m.ProviderID == providerID)
                ).OrderBy(m => m.ProviderName).ThenBy(m => m.ProviderTradingProfileName).ToListAsync();

            return View(providerTradingProfile);
        }

        // GET: ProviderTradingProfile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerTradingProfile = await _context.ProviderTradingProfileView
                .FirstOrDefaultAsync(m => m.id == id);
            if (providerTradingProfile == null)
            {
                return NotFound();
            }

            return View(providerTradingProfile);
        }
         
        [HttpGet]
        public JsonResult IsExist(string ProviderTradingProfileName, int id)
        {
            return Json(!_context.ProviderTradingProfile.Any(m => m.ProviderTradingProfileName == ProviderTradingProfileName && m.id != id));
        }

        // GET: ProviderTradingProfile/Create
        public IActionResult Create()
        {
            ViewBag.Provider = new SelectList(_context.Provider.OrderBy(m => m.ProviderName).ToList(), "id", "ProviderName");
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(m => m.CurrencyPairName).ToList(), "id", "CurrencyPairName");

            return View();
        }

        // POST: ProviderTradingProfile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ProviderID,ProviderTradingProfileName,CurrencyPairID,DateTimeModified,DateTimeAdded")] ProviderTradingProfile providerTradingProfile)
        {
            if (ModelState.IsValid)
            {
                providerTradingProfile.DateTimeModified = Utility.GetLocalDateTime();
                providerTradingProfile.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(providerTradingProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Provider = new SelectList(_context.Provider.OrderBy(m => m.ProviderName).ToList(), "id", "ProviderName");
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(m => m.CurrencyPairName).ToList(), "id", "CurrencyPairName");

            return View(providerTradingProfile);
        }

        // GET: ProviderTradingProfile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerTradingProfile = await _context.ProviderTradingProfile.FindAsync(id);
            if (providerTradingProfile == null)
            {
                return NotFound();
            }
            ViewBag.Provider = new SelectList(_context.Provider.OrderBy(m => m.ProviderName).ToList(), "id", "ProviderName");
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(m => m.CurrencyPairName).ToList(), "id", "CurrencyPairName");

            return View(providerTradingProfile);
        }

        // POST: ProviderTradingProfile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ProviderID,ProviderTradingProfileName,CurrencyPairID,DateTimeModified,DateTimeAdded")] ProviderTradingProfile providerTradingProfile)
        {
            if (id != providerTradingProfile.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    providerTradingProfile.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(providerTradingProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderTradingProfileExists(providerTradingProfile.id))
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
            ViewBag.Provider = new SelectList(_context.Provider.OrderBy(m => m.ProviderName).ToList(), "id", "ProviderName");
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(m => m.CurrencyPairName).ToList(), "id", "CurrencyPairName");

            return View(providerTradingProfile);
        }

        // GET: ProviderTradingProfile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerTradingProfile = await _context.ProviderTradingProfileView
                .FirstOrDefaultAsync(m => m.id == id);
            if (providerTradingProfile == null)
            {
                return NotFound();
            }

            return View(providerTradingProfile);
        }

        // POST: ProviderTradingProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //remove associated map
            var providerTradingProfileIntroducerMap = await _context.ProviderTradingProfileIntroducerMap
                .Where(m => m.ProviderTradingProfileID == id).ToListAsync();
            _context.ProviderTradingProfileIntroducerMap.RemoveRange(providerTradingProfileIntroducerMap);

            var providerTradingProfile = await _context.ProviderTradingProfile.FindAsync(id);
            _context.ProviderTradingProfile.Remove(providerTradingProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderTradingProfileExists(int id)
        {
            return _context.ProviderTradingProfile.Any(e => e.id == id);
        }
    }
}
