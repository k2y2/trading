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
    public class ReportAccountBalanceController : Controller
    {
        private readonly tradingContext _context;

        public ReportAccountBalanceController(tradingContext context)
        {
            _context = context;
        }

        // GET: ReportAccountBalance
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") == "S")
                return View(await _context.ReportAccountBalance.ToListAsync());

            return RedirectToAction("Logout", "Home"); 
        }

        // GET: ReportAccountBalance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccountBalance = await _context.ReportAccountBalance
                .FirstOrDefaultAsync(m => m.id == id);
            if (reportAccountBalance == null)
            {
                return NotFound();
            }

            return View(reportAccountBalance);
        }

        // GET: ReportAccountBalance/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReportAccountBalance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,AccountName,CurrencyName,Balance,BalanceUSD,Rate")] ReportAccountBalance reportAccountBalance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportAccountBalance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportAccountBalance);
        }

        // GET: ReportAccountBalance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccountBalance = await _context.ReportAccountBalance.FindAsync(id);
            if (reportAccountBalance == null)
            {
                return NotFound();
            }
            return View(reportAccountBalance);
        }

        // POST: ReportAccountBalance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,AccountName,CurrencyName,Balance,BalanceUSD,Rate")] ReportAccountBalance reportAccountBalance)
        {
            if (id != reportAccountBalance.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportAccountBalance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportAccountBalanceExists(reportAccountBalance.id))
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
            return View(reportAccountBalance);
        }

        // GET: ReportAccountBalance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccountBalance = await _context.ReportAccountBalance
                .FirstOrDefaultAsync(m => m.id == id);
            if (reportAccountBalance == null)
            {
                return NotFound();
            }

            return View(reportAccountBalance);
        }

        // POST: ReportAccountBalance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportAccountBalance = await _context.ReportAccountBalance.FindAsync(id);
            _context.ReportAccountBalance.Remove(reportAccountBalance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportAccountBalanceExists(int id)
        {
            return _context.ReportAccountBalance.Any(e => e.id == id);
        }
    }
}
