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
    public class ProviderTradingProfileIntroducerMapController : Controller
    {
        private readonly tradingContext _context;

        public ProviderTradingProfileIntroducerMapController(tradingContext context)
        {
            _context = context;
        }

        // GET: ProviderTradingProfileIntroducerMap
        public async Task<IActionResult> Index(int providerTradingProfileID)
        {
            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfileIntroducerMapView.ToList()
                      .GroupBy(m => m.ProviderTradingProfileID).Select(m => m.First()).OrderBy(m => m.ProviderTradingProfileName), "ProviderTradingProfileID", "ProviderTradingProfileName", providerTradingProfileID);

            var providerTradingProfileIntroducerMap = await _context.ProviderTradingProfileIntroducerMapView
                .Where(m =>
                  (providerTradingProfileID == 0 || m.ProviderTradingProfileID == providerTradingProfileID)
                ).OrderBy(m => m.ProviderTradingProfileName).ThenBy(m => m.IntroducerName).ToListAsync();

            return View(providerTradingProfileIntroducerMap);
        }

        // GET: ProviderTradingProfileIntroducerMap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerTradingProfileIntroducerMap = await _context.ProviderTradingProfileIntroducerMapView
                .FirstOrDefaultAsync(m => m.id == id);
            if (providerTradingProfileIntroducerMap == null)
            {
                return NotFound();
            }

            return View(providerTradingProfileIntroducerMap);
        }

        [HttpGet]
        public JsonResult IsExist(int IntroducerID, int id, int ProviderTradingProfileID)
        {
            return Json(!_context.ProviderTradingProfileIntroducerMap
                .Any(m => m.IntroducerID == IntroducerID && m.ProviderTradingProfileID == ProviderTradingProfileID && m.id != id));
        }

        // GET: ProviderTradingProfileIntroducerMap/Create
        public IActionResult Create()
        {
            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.OrderBy(m => m.ProviderTradingProfileName).ToList(), "id", "ProviderTradingProfileName");
            ViewBag.Introducer = new SelectList(_context.Introducer.OrderBy(m => m.IntroducerName).ToList(), "id", "IntroducerName");
            ViewBag.IntroducerCommissionType = new SelectList(_context.IntroducerCommissionType.ToList(), "id", "IntroducerCommissionTypeName");

            return View();
        }

        // POST: ProviderTradingProfileIntroducerMap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ProviderTradingProfileID,IntroducerID,IntroducerCommissionTypeID,IntroducerCommissionRate,DateTimeModified,DateTimeAdded")] ProviderTradingProfileIntroducerMap providerTradingProfileIntroducerMap)
        {
            if (ModelState.IsValid)
            {
                providerTradingProfileIntroducerMap.DateTimeModified = Utility.GetLocalDateTime();
                providerTradingProfileIntroducerMap.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(providerTradingProfileIntroducerMap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.OrderBy(m => m.ProviderTradingProfileName).ToList(), "id", "ProviderTradingProfileName");
            ViewBag.Introducer = new SelectList(_context.Introducer.OrderBy(m => m.IntroducerName).ToList(), "id", "IntroducerName");
            ViewBag.IntroducerCommissionType = new SelectList(_context.IntroducerCommissionType.ToList(), "id", "IntroducerCommissionTypeName");

            return View(providerTradingProfileIntroducerMap);
        }

        // GET: ProviderTradingProfileIntroducerMap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerTradingProfileIntroducerMap = await _context.ProviderTradingProfileIntroducerMap.FindAsync(id);
            if (providerTradingProfileIntroducerMap == null)
            {
                return NotFound();
            }
            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.OrderBy(m => m.ProviderTradingProfileName).ToList(), "id", "ProviderTradingProfileName");
            ViewBag.Introducer = new SelectList(_context.Introducer.OrderBy(m => m.IntroducerName).ToList(), "id", "IntroducerName");
            ViewBag.IntroducerCommissionType = new SelectList(_context.IntroducerCommissionType.ToList(), "id", "IntroducerCommissionTypeName");

            return View(providerTradingProfileIntroducerMap);
        }

        // POST: ProviderTradingProfileIntroducerMap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ProviderTradingProfileID,IntroducerID,IntroducerCommissionTypeID,IntroducerCommissionRate,DateTimeModified,DateTimeAdded")] ProviderTradingProfileIntroducerMap providerTradingProfileIntroducerMap)
        {
            if (id != providerTradingProfileIntroducerMap.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    providerTradingProfileIntroducerMap.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(providerTradingProfileIntroducerMap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderTradingProfileIntroducerMapExists(providerTradingProfileIntroducerMap.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.OrderBy(m => m.ProviderTradingProfileName).ToList(), "id", "ProviderTradingProfileName");
                ViewBag.Introducer = new SelectList(_context.Introducer.OrderBy(m => m.IntroducerName).ToList(), "id", "IntroducerName");
                ViewBag.IntroducerCommissionType = new SelectList(_context.IntroducerCommissionType.ToList(), "id", "IntroducerCommissionTypeName");

                return RedirectToAction(nameof(Index));
            }
            return View(providerTradingProfileIntroducerMap);
        }

        // GET: ProviderTradingProfileIntroducerMap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerTradingProfileIntroducerMap = await _context.ProviderTradingProfileIntroducerMapView
                .FirstOrDefaultAsync(m => m.id == id);
            if (providerTradingProfileIntroducerMap == null)
            {
                return NotFound();
            }

            return View(providerTradingProfileIntroducerMap);
        }

        // POST: ProviderTradingProfileIntroducerMap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var providerTradingProfileIntroducerMap = await _context.ProviderTradingProfileIntroducerMap.FindAsync(id);
            _context.ProviderTradingProfileIntroducerMap.Remove(providerTradingProfileIntroducerMap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderTradingProfileIntroducerMapExists(int id)
        {
            return _context.ProviderTradingProfileIntroducerMap.Any(e => e.id == id);
        }
    }
}
