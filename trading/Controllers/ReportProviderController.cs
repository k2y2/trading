using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using trading.Models;

namespace trading.Controllers
{
    public class ReportProviderController : Controller
    {
        private readonly tradingContext _context;

        public ReportProviderController(tradingContext context)
        {
            _context = context;
        }

        // GET: ReportProvider
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReportProvider
                .OrderByDescending(m=>m.MissingSlipAmount).ThenBy(m => m.ProviderTradingProfileName).ToListAsync());
        }

        // GET: ReportProvider/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportProvider = await _context.ReportProvider
                .FirstOrDefaultAsync(m => m.ProviderTradingProfileID == id);
            if (reportProvider == null)
            {
                return NotFound();
            }

            return View(reportProvider);
        }

        // GET: ReportProvider/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReportProvider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProviderName,TotalProviderExpectedPayInAmount,TotalSlipAmount,MissingSlipAmount")] ReportProvider reportProvider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportProvider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportProvider);
        }

        // GET: ReportProvider/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportProvider = await _context.ReportProvider.FindAsync(id);
            if (reportProvider == null)
            {
                return NotFound();
            }
            return View(reportProvider);
        }

        // POST: ReportProvider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProviderName,TotalProviderExpectedPayInAmount,TotalSlipAmount,MissingSlipAmount")] ReportProvider reportProvider)
        {
            if (id != reportProvider.ProviderTradingProfileID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportProvider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportProviderExists(reportProvider.ProviderTradingProfileID))
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
            return View(reportProvider);
        }

        // GET: ReportProvider/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportProvider = await _context.ReportProvider
                .FirstOrDefaultAsync(m => m.ProviderTradingProfileID == id);
            if (reportProvider == null)
            {
                return NotFound();
            }

            return View(reportProvider);
        }

        // POST: ReportProvider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportProvider = await _context.ReportProvider.FindAsync(id);
            _context.ReportProvider.Remove(reportProvider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportProviderExists(int id)
        {
            return _context.ReportProvider.Any(e => e.ProviderTradingProfileID == id);
        }
    }
}
