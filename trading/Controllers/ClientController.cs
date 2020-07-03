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
    public class ClientController : Controller
    {
        private readonly tradingContext _context;

        public ClientController(tradingContext context)
        {
            _context = context;
        }

        // GET: Client
        public async Task<IActionResult> Index(string clientName)
        {
            ViewBag.clientName = clientName;

            var client = await _context.ClientView
                .Where(m =>
                   clientName == null || m.ClientName.ToUpper().Contains(clientName.ToUpper())   
                ).OrderBy(m => m.ClientName).ToListAsync();

            return View(client);
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.ClientView
                .FirstOrDefaultAsync(m => m.id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpGet]
        public JsonResult IsExist(string ClientName, int id)
        {
            return Json(!_context.Client.Any(x => x.ClientName == ClientName && x.id != id));
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            ViewBag.ClientType = new SelectList(_context.ClientType.ToList(), "id", "ClientTypeName");
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ClientName,ClientLegalName,ClientTypeID,DateTimeModified,DateTimeAdded")] Client client)
        {
            if (ModelState.IsValid)
            {
                client.DateTimeModified = Utility.GetLocalDateTime();
                client.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ClientType = new SelectList(_context.ClientType.ToList(), "id", "ClientTypeName");
            return View(client);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewBag.ClientType = new SelectList(_context.ClientType.ToList(), "id", "ClientTypeName");
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ClientName,ClientLegalName,ClientTypeID,DateTimeModified,DateTimeAdded")] Client client)
        {
            if (id != client.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    client.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.id))
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
            ViewBag.ClientType = new SelectList(_context.ClientType.ToList(), "id", "ClientTypeName");
            return View(client);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.ClientView
                .FirstOrDefaultAsync(m => m.id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        { 
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            //remove associated map 
            var clientTradingProfileIntroducerMap = await (from t in _context.ClientTradingProfile
                                                           join m in _context.ClientTradingProfileIntroducerMap on t.id equals m.ClientTradingProfileID
                                                           where t.ClientID == id
                                                           select m).ToListAsync();
             
            _context.ClientTradingProfileIntroducerMap.RemoveRange(clientTradingProfileIntroducerMap);
            
            //remove associated TP
            var clientTradingProfile = await _context.ClientTradingProfile
                .Where(m => m.ClientID == id).ToListAsync();

            _context.ClientTradingProfile.RemoveRange(clientTradingProfile);
             
            //remove associated bank
            var clientBankAccount = await _context.ClientBankAccount
                .Where(m => m.ClientID == id).ToListAsync();

            _context.ClientBankAccount.RemoveRange(clientBankAccount);
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.id == id);
        }
    }
}
