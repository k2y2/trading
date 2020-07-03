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
    public class ClientTradingProfileIntroducerMapController : Controller
    {
        private readonly tradingContext _context;

        public ClientTradingProfileIntroducerMapController(tradingContext context)
        {
            _context = context;
        }

        // GET: ClientTradingProfileIntroducerMap
        public async Task<IActionResult> Index(int clientTradingProfileID)
        {
            ViewBag.ClientTradingProfile = new SelectList(_context.ClientTradingProfileIntroducerMapView.ToList()
                      .GroupBy(m => m.ClientTradingProfileID).Select(m => m.First()).OrderBy(m => m.ClientTradingProfileName), "ClientTradingProfileID", "ClientTradingProfileName", clientTradingProfileID);

            var clientTradingProfileIntroducerMap = await _context.ClientTradingProfileIntroducerMapView
                .Where(m =>
                  (clientTradingProfileID == 0 || m.ClientTradingProfileID == clientTradingProfileID)
                ).OrderBy(m => m.ClientTradingProfileName).ThenBy(m => m.IntroducerName).ToListAsync();

            return View(clientTradingProfileIntroducerMap);
        }

        // GET: ClientTradingProfileIntroducerMap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientTradingProfileIntroducerMap = await _context.ClientTradingProfileIntroducerMapView
                .FirstOrDefaultAsync(m => m.id == id);
            if (clientTradingProfileIntroducerMap == null)
            {
                return NotFound();
            }

            return View(clientTradingProfileIntroducerMap);
        }

        [HttpGet]
        public JsonResult IsExist(int IntroducerID, int id, int ClientTradingProfileID)
        {
            return Json(!_context.ClientTradingProfileIntroducerMap
                .Any(m => m.IntroducerID == IntroducerID && m.ClientTradingProfileID == ClientTradingProfileID && m.id != id));
        }

        // GET: ClientTradingProfileIntroducerMap/Create
        public IActionResult Create()
        { 
            ViewBag.ClientTradingProfile = new SelectList(_context.ClientTradingProfile.OrderBy(m=>m.ClientTradingProfileName).ToList(), "id", "ClientTradingProfileName");
            ViewBag.Introducer = new SelectList(_context.Introducer.OrderBy(m => m.IntroducerName).ToList(), "id", "IntroducerName");
            ViewBag.IntroducerCommissionType = new SelectList(_context.IntroducerCommissionType.ToList(), "id", "IntroducerCommissionTypeName");
            
            return View();
        }

        // POST: ClientTradingProfileIntroducerMap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ClientTradingProfileID,IntroducerID,IntroducerCommissionTypeID,IntroducerCommissionRate,DateTimeModified,DateTimeAdded")] ClientTradingProfileIntroducerMap clientTradingProfileIntroducerMap)
        {
            if (ModelState.IsValid)
            {
                clientTradingProfileIntroducerMap.DateTimeModified = Utility.GetLocalDateTime();
                clientTradingProfileIntroducerMap.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(clientTradingProfileIntroducerMap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ClientTradingProfile = new SelectList(_context.ClientTradingProfile.OrderBy(m => m.ClientTradingProfileName).ToList(), "id", "ClientTradingProfileName");
            ViewBag.Introducer = new SelectList(_context.Introducer.OrderBy(m => m.IntroducerName).ToList(), "id", "IntroducerName");
            ViewBag.IntroducerCommissionType = new SelectList(_context.IntroducerCommissionType.ToList(), "id", "IntroducerCommissionTypeName");

            return View(clientTradingProfileIntroducerMap);
        }

        // GET: ClientTradingProfileIntroducerMap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientTradingProfileIntroducerMap = await _context.ClientTradingProfileIntroducerMap.FindAsync(id);
            if (clientTradingProfileIntroducerMap == null)
            {
                return NotFound();
            }
            ViewBag.ClientTradingProfile = new SelectList(_context.ClientTradingProfile.OrderBy(m => m.ClientTradingProfileName).ToList(), "id", "ClientTradingProfileName");
            ViewBag.Introducer = new SelectList(_context.Introducer.OrderBy(m => m.IntroducerName).ToList(), "id", "IntroducerName");
            ViewBag.IntroducerCommissionType = new SelectList(_context.IntroducerCommissionType.ToList(), "id", "IntroducerCommissionTypeName");

            return View(clientTradingProfileIntroducerMap);
        }

        // POST: ClientTradingProfileIntroducerMap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ClientTradingProfileID,IntroducerID,IntroducerCommissionTypeID,IntroducerCommissionRate,DateTimeModified,DateTimeAdded")] ClientTradingProfileIntroducerMap clientTradingProfileIntroducerMap)
        {
            if (id != clientTradingProfileIntroducerMap.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    clientTradingProfileIntroducerMap.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(clientTradingProfileIntroducerMap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientTradingProfileIntroducerMapExists(clientTradingProfileIntroducerMap.id))
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
            ViewBag.ClientTradingProfile = new SelectList(_context.ClientTradingProfile.OrderBy(m => m.ClientTradingProfileName).ToList(), "id", "ClientTradingProfileName");
            ViewBag.Introducer = new SelectList(_context.Introducer.OrderBy(m => m.IntroducerName).ToList(), "id", "IntroducerName");
            ViewBag.IntroducerCommissionType = new SelectList(_context.IntroducerCommissionType.ToList(), "id", "IntroducerCommissionTypeName");

            return View(clientTradingProfileIntroducerMap);
        }

        // GET: ClientTradingProfileIntroducerMap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientTradingProfileIntroducerMap = await _context.ClientTradingProfileIntroducerMapView
                .FirstOrDefaultAsync(m => m.id == id);
            if (clientTradingProfileIntroducerMap == null)
            {
                return NotFound();
            }

            return View(clientTradingProfileIntroducerMap);
        }

        // POST: ClientTradingProfileIntroducerMap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientTradingProfileIntroducerMap = await _context.ClientTradingProfileIntroducerMap.FindAsync(id);
            _context.ClientTradingProfileIntroducerMap.Remove(clientTradingProfileIntroducerMap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientTradingProfileIntroducerMapExists(int id)
        {
            return _context.ClientTradingProfileIntroducerMap.Any(e => e.id == id);
        }
    }
}
