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
    public class CostRateController : Controller
    {
        private readonly tradingContext _context;

        public CostRateController(tradingContext context)
        {
            _context = context;
        }

        // GET: CostRate
        public async Task<IActionResult> Index(DateTime dateFilter)
        {
            if (dateFilter == DateTime.MinValue) dateFilter = Utility.GetLocalDateTime().Date;
            ViewBag.dateFilter = dateFilter.ToString("yyyy-MM-dd");

            //var costRateView = (from c in _context.CostRate
            //               join p in _context.Provider on c.ProviderID equals p.id
            //               where c.TradeDate == dateFilter

            //               select new CostRateView
            //               {
            //                   id = c.id,
            //                   TradeDate = c.TradeDate,
            //                   ProviderID = c.ProviderID,
            //                   Rate = c.Rate,
            //                   DateTimeModified = c.DateTimeModified,
            //                   DateTimeAdded = c.DateTimeAdded,
            //                   ProviderName = p.ProviderName
            //               }
            //    ); 

            //return View(await costRateView.ToListAsync());
            //return View(await _context.CostRate.ToListAsync());
            return View(await _context.CostRateView.Where(m => m.TradeDate == dateFilter).OrderBy(m => m.ProviderTradingProfileName).ToListAsync());
        }

        // GET: CostRate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costRateView = await _context.CostRateView
                .FirstOrDefaultAsync(m => m.id == id);

            //var costRateView = await (from c in _context.CostRate
            //                          join p in _context.Provider on c.ProviderID equals p.id
            //                          where c.id == id

            //                          select new CostRateView
            //                          {
            //                              id = c.id,
            //                              TradeDate = c.TradeDate,
            //                              ProviderID = c.ProviderID,
            //                              Rate = c.Rate,
            //                              DateTimeModified = c.DateTimeModified,
            //                              DateTimeAdded = c.DateTimeAdded,
            //                              ProviderName = p.ProviderName
            //                          }
            //    ).FirstOrDefaultAsync();

            if (costRateView == null)
            {
                return NotFound();
            }

            return View(costRateView);
        }

        [HttpGet]
        public JsonResult IsExist(int ProviderTradingProfileID, int id, DateTime TradeDate)
        {
            return Json(!_context.CostRate.Any(x => x.ProviderTradingProfileID == ProviderTradingProfileID && x.TradeDate == TradeDate && x.id != id));
        }

        // GET: CostRate/Create
        public IActionResult Create()
        {
            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.OrderBy(x => x.ProviderTradingProfileName).ToList(), "id", "ProviderTradingProfileName");
             
            CostRate costRate = new CostRate();
            costRate.TradeDate = Utility.GetLocalDateTime().Date;

            return View("Create", costRate);
        }

        // POST: CostRate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,TradeDate,ProviderTradingProfileID,Rate,DateTimeModified,DateTimeAdded")] CostRate costRate)
        {
            if (ModelState.IsValid)
            {
                costRate.DateTimeModified = Utility.GetLocalDateTime();
                costRate.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(costRate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.OrderBy(x => x.ProviderTradingProfileName).ToList(), "id", "ProviderTradingProfileName");

            return View(costRate);
        }

        // GET: CostRate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costRate = await _context.CostRate.FindAsync(id);
            if (costRate == null)
            {
                return NotFound();
            }
            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.OrderBy(x => x.ProviderTradingProfileName).ToList(), "id", "ProviderTradingProfileName");
 
            return View(costRate);
        }

        // POST: CostRate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,TradeDate,ProviderTradingProfileID,Rate,DateTimeModified,DateTimeAdded")] CostRate costRate)
        {
            if (id != costRate.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    costRate.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(costRate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostRateExists(costRate.id))
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
            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.OrderBy(x => x.ProviderTradingProfileName).ToList(), "id", "ProviderTradingProfileName");

            return View(costRate);
        }

        // GET: CostRate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costRateView = await _context.CostRateView
                .FirstOrDefaultAsync(m => m.id == id);

            //var costRateView = await (from c in _context.CostRate
            //                    join p in _context.Provider on c.ProviderID equals p.id
            //                    where c.id == id

            //                    select new CostRateView
            //                    {
            //                        id = c.id,
            //                        TradeDate = c.TradeDate,
            //                        ProviderID = c.ProviderID,
            //                        Rate = c.Rate,
            //                        DateTimeModified = c.DateTimeModified,
            //                        DateTimeAdded = c.DateTimeAdded,
            //                        ProviderName = p.ProviderName
            //                    }
            //    ).FirstOrDefaultAsync();  

            if (costRateView == null)
            {
                return NotFound();
            }

            return View(costRateView);
        }

        // POST: CostRate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var costRate = await _context.CostRate.FindAsync(id);
            _context.CostRate.Remove(costRate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostRateExists(int id)
        {
            return _context.CostRate.Any(e => e.id == id);
        }
    }
}
