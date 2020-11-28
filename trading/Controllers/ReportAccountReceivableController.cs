using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using trading.Models;

namespace trading.Controllers
{
    public class ReportAccountReceivableController : Controller
    {
        private readonly tradingContext _context;

        public ReportAccountReceivableController(tradingContext context)
        {
            _context = context;
        }

        // GET: ReportAccountReceivable
        public async Task<IActionResult> Index(int providerID, int providerTradingProfileID)
        {
            ViewBag.Provider = new SelectList(_context.Provider.ToList()
                      .OrderBy(m => m.ProviderName), "id", "ProviderName", providerID);

            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.ToList()
                      .OrderBy(m => m.ProviderTradingProfileName), "id", "ProviderTradingProfileName", providerTradingProfileID);
             
            var ReportAccountReceivable = _context.ReportAccountReceivable
                .Where(x => (providerID == 0 || x.ProviderID == providerID) &&
                      (providerTradingProfileID == 0 || x.ProviderTradingProfileID == providerTradingProfileID));

            if (HttpContext.Session.GetString("Role") == "S")
                return View(await ReportAccountReceivable.OrderByDescending(m => m.AmountReceivable).ToListAsync());

            return RedirectToAction("Logout", "Home");
        }

        // GET: ReportAccountReceivable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccountReceivable = await _context.ReportAccountReceivable
                .FirstOrDefaultAsync(m => m.ProviderTradingProfileID == id);
            if (reportAccountReceivable == null)
            {
                return NotFound();
            }

            return View(reportAccountReceivable);
        }

        // GET: ReportAccountReceivable/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReportAccountReceivable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProviderTradingProfileID,ProviderTradingProfileName,AmountReceivable,CurrencyName2,AmountReceivableUSD,Rate")] ReportAccountReceivable reportAccountReceivable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportAccountReceivable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportAccountReceivable);
        }

        // GET: ReportAccountReceivable/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccountReceivable = await _context.ReportAccountReceivable.FindAsync(id);
            if (reportAccountReceivable == null)
            {
                return NotFound();
            }
            return View(reportAccountReceivable);
        }

        // POST: ReportAccountReceivable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProviderTradingProfileID,ProviderTradingProfileName,AmountReceivable,CurrencyName2,AmountReceivableUSD,Rate")] ReportAccountReceivable reportAccountReceivable)
        {
            if (id != reportAccountReceivable.ProviderTradingProfileID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportAccountReceivable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportAccountReceivableExists(reportAccountReceivable.ProviderTradingProfileID))
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
            return View(reportAccountReceivable);
        }

        // GET: ReportAccountReceivable/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccountReceivable = await _context.ReportAccountReceivable
                .FirstOrDefaultAsync(m => m.ProviderTradingProfileID == id);
            if (reportAccountReceivable == null)
            {
                return NotFound();
            }

            return View(reportAccountReceivable);
        }

        // POST: ReportAccountReceivable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportAccountReceivable = await _context.ReportAccountReceivable.FindAsync(id);
            _context.ReportAccountReceivable.Remove(reportAccountReceivable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportAccountReceivableExists(int id)
        {
            return _context.ReportAccountReceivable.Any(e => e.ProviderTradingProfileID == id);
        }
    }
}
