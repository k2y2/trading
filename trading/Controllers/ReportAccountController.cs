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
    public class ReportAccountController : Controller
    {
        private readonly tradingContext _context;

        public ReportAccountController(tradingContext context)
        {
            _context = context;
        }

        // GET: ReportAccount
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReportAccount.OrderBy(m=>m.AccountName).ToListAsync());
        }

        // GET: ReportAccount/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccount = await _context.ReportAccount
                .FirstOrDefaultAsync(m => m.id == id);
            if (reportAccount == null)
            {
                return NotFound();
            }

            return View(reportAccount);
        }

        // GET: ReportAccount/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReportAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountName,TotalClientAmountOut")] ReportAccount reportAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportAccount);
        }

        // GET: ReportAccount/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccount = await _context.ReportAccount.FindAsync(id);
            if (reportAccount == null)
            {
                return NotFound();
            }
            return View(reportAccount);
        }

        // POST: ReportAccount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountName,TotalClientAmountOut")] ReportAccount reportAccount)
        {
            if (id != reportAccount.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportAccountExists(reportAccount.id))
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
            return View(reportAccount);
        }

        // GET: ReportAccount/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccount = await _context.ReportAccount
                .FirstOrDefaultAsync(m => m.id == id);
            if (reportAccount == null)
            {
                return NotFound();
            }

            return View(reportAccount);
        }

        // POST: ReportAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportAccount = await _context.ReportAccount.FindAsync(id);
            _context.ReportAccount.Remove(reportAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportAccountExists(int id)
        {
            return _context.ReportAccount.Any(e => e.id == id);
        }
    }
}
