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
    public class ClientTradingProfileController : Controller
    {
        private readonly tradingContext _context;

        public ClientTradingProfileController(tradingContext context)
        {
            _context = context;
        }

        // GET: ClientTradingProfile
        public async Task<IActionResult> Index(int clientID)
        {
            ViewBag.Client = new SelectList(_context.ClientTradingProfileView.ToList()
                   .GroupBy(m => m.ClientID).Select(m => m.First()).OrderBy(m => m.ClientName), "ClientID", "ClientName", clientID);

            var clientTradingProfile = await _context.ClientTradingProfileView
                .Where(m =>
                  (clientID == 0 || m.ClientID == clientID)
                ).OrderBy(m => m.ClientName).ThenBy(m => m.ClientTradingProfileName).ToListAsync();

            return View(clientTradingProfile);
        }

        // GET: ClientTradingProfile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientTradingProfile = await _context.ClientTradingProfileView
                .FirstOrDefaultAsync(m => m.id == id);
            if (clientTradingProfile == null)
            {
                return NotFound();
            }

            return View(clientTradingProfile);
        }

        [HttpGet]
        public JsonResult IsExist(string ClientTradingProfileName, int id, int ClientID)
        {
            return Json(!_context.ClientTradingProfile.Any(m => m.ClientTradingProfileName == ClientTradingProfileName && m.ClientID == ClientID && m.id != id));
        }

        // GET: ClientTradingProfile/Create
        public IActionResult Create()
        {
            ViewBag.Client = new SelectList(_context.Client.OrderBy(m=>m.ClientName).ToList(), "id", "ClientName");
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(m => m.CurrencyPairName).ToList(), "id", "CurrencyPairName");

            return View();
        }

        // POST: ClientTradingProfile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ClientID,ClientTradingProfileName,CurrencyPairID,Price,DateTimeModified,DateTimeAdded")] ClientTradingProfile clientTradingProfile)
        {
            if (ModelState.IsValid)
            {
                clientTradingProfile.DateTimeModified = Utility.GetLocalDateTime();
                clientTradingProfile.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(clientTradingProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Client = new SelectList(_context.Client.OrderBy(m => m.ClientName).ToList(), "id", "ClientName");
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(m => m.CurrencyPairName).ToList(), "id", "CurrencyPairName");

            return View(clientTradingProfile);
        }

        // GET: ClientTradingProfile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientTradingProfile = await _context.ClientTradingProfile.FindAsync(id);
            if (clientTradingProfile == null)
            {
                return NotFound();
            }
            ViewBag.Client = new SelectList(_context.Client.OrderBy(m => m.ClientName).ToList(), "id", "ClientName");
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(m => m.CurrencyPairName).ToList(), "id", "CurrencyPairName");

            return View(clientTradingProfile);
        }

        // POST: ClientTradingProfile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ClientID,ClientTradingProfileName,CurrencyPairID,Price,DateTimeModified,DateTimeAdded")] ClientTradingProfile clientTradingProfile)
        {
            if (id != clientTradingProfile.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    clientTradingProfile.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(clientTradingProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientTradingProfileExists(clientTradingProfile.id))
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
            ViewBag.Client = new SelectList(_context.Client.OrderBy(m => m.ClientName).ToList(), "id", "ClientName");
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(m => m.CurrencyPairName).ToList(), "id", "CurrencyPairName");

            return View(clientTradingProfile);
        }

        // GET: ClientTradingProfile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientTradingProfile = await _context.ClientTradingProfileView
                .FirstOrDefaultAsync(m => m.id == id);
            if (clientTradingProfile == null)
            {
                return NotFound();
            }

            return View(clientTradingProfile);
        }

        // POST: ClientTradingProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //remove associated map
            var clientTradingProfileIntroducerMap = await _context.ClientTradingProfileIntroducerMap
                .Where(m => m.ClientTradingProfileID == id).ToListAsync(); 
            _context.ClientTradingProfileIntroducerMap.RemoveRange(clientTradingProfileIntroducerMap);

            var clientTradingProfile = await _context.ClientTradingProfile.FindAsync(id);
            _context.ClientTradingProfile.Remove(clientTradingProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientTradingProfileExists(int id)
        {
            return _context.ClientTradingProfile.Any(e => e.id == id);
        }
    }
}
