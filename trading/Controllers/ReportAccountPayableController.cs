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
    public class ReportAccountPayableController : Controller
    {
        private readonly tradingContext _context;

        public ReportAccountPayableController(tradingContext context)
        {
            _context = context;
        }

        // GET: ReportAccountPayable
        public async Task<IActionResult> Index(int clientTradingProfileID, int currencyID, DateTime dateFilterStart, DateTime dateFilterEnd)
        {
            ViewBag.ClientTradingProfile = new SelectList(_context.ClientTradingProfile.ToList()
                      .OrderBy(m => m.ClientTradingProfileName), "id", "ClientTradingProfileName", clientTradingProfileID);

            ViewBag.Currency = new SelectList(_context.Currency.ToList()
                      .OrderBy(m => m.CurrencyName), "id", "CurrencyName", currencyID);

            //date range
            if (dateFilterStart == DateTime.MinValue) dateFilterStart = Utility.GetLocalDateTime().Date.AddMonths(-3); 
            if (dateFilterEnd == DateTime.MinValue) dateFilterEnd = Utility.GetLocalDateTime().Date;

            ViewBag.dateFilterStart = dateFilterStart.ToString("yyyy-MM-dd");
            ViewBag.dateFilterEnd = dateFilterEnd.ToString("yyyy-MM-dd");


            //var clientTradingProfileID = 0;
            //var clientCurrencyIDOut = 0;
            //var tradeDateFrom = "2020-05-02";
            //var tradeDateTo = "2020-10-02";

            var reportAccountPayable = await _context.ReportAccountPayable
                .FromSqlRaw("EXECUTE dbo.sp_ReportAccountPayable {0},{1},{2},{3}", clientTradingProfileID, currencyID, dateFilterStart, dateFilterEnd)
                .ToListAsync();

            ViewBag.TotalInUSD = String.Format("{0:0,0.00}", reportAccountPayable.Sum(x => x.BalanceInUSD)); 
            ViewBag.TotalOut = currencyID>0 ? String.Format("{0:0,0.00}", reportAccountPayable.Sum(x => x.ClientAmountUncompleted)) : "";
            ViewBag.TotalOutUSD = String.Format("{0:0,0.00}", reportAccountPayable.Sum(x => x.ClientAmountUncompletedUSD));
            
            if (HttpContext.Session.GetString("Role") == "S")
                return View(reportAccountPayable);

            return RedirectToAction("Logout", "Home");
            
            //return View(await _context.ReportAccountPayable.ToListAsync());
        }

        // GET: ReportAccountPayable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccountPayable = await _context.ReportAccountPayable
                .FirstOrDefaultAsync(m => m.ClientTradingProfileID == id);
            if (reportAccountPayable == null)
            {
                return NotFound();
            }

            return View(reportAccountPayable);
        }

        // GET: ReportAccountPayable/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReportAccountPayable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientTradingProfileID,ClientTradingProfileName,ClientCurrencyNameOut,TradeDate,ClientAmountUncompleted,ClientAmountUncompletedUSD,Rate")] ReportAccountPayable reportAccountPayable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportAccountPayable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportAccountPayable);
        }

        // GET: ReportAccountPayable/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccountPayable = await _context.ReportAccountPayable.FindAsync(id);
            if (reportAccountPayable == null)
            {
                return NotFound();
            }
            return View(reportAccountPayable);
        }

        // POST: ReportAccountPayable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientTradingProfileID,ClientTradingProfileName,ClientCurrencyNameOut,TradeDate,ClientAmountUncompleted,ClientAmountUncompletedUSD,Rate")] ReportAccountPayable reportAccountPayable)
        {
            if (id != reportAccountPayable.ClientTradingProfileID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportAccountPayable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportAccountPayableExists(reportAccountPayable.ClientTradingProfileID))
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
            return View(reportAccountPayable);
        }

        // GET: ReportAccountPayable/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccountPayable = await _context.ReportAccountPayable
                .FirstOrDefaultAsync(m => m.ClientTradingProfileID == id);
            if (reportAccountPayable == null)
            {
                return NotFound();
            }

            return View(reportAccountPayable);
        }

        // POST: ReportAccountPayable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportAccountPayable = await _context.ReportAccountPayable.FindAsync(id);
            _context.ReportAccountPayable.Remove(reportAccountPayable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportAccountPayableExists(int id)
        {
            return _context.ReportAccountPayable.Any(e => e.ClientTradingProfileID == id);
        }
    }
}
