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
    public class ProviderController : Controller
    {
        private readonly tradingContext _context;

        public ProviderController(tradingContext context)
        {
            _context = context;
        }

        // GET: Provider
        public async Task<IActionResult> Index(string providerName)
        {
            ViewBag.providerName = providerName;

            var provider = await _context.Provider
                .Where(m =>
                  (providerName == null || m.ProviderName.ToUpper().Contains(providerName.ToUpper()))

                ).OrderBy(m => m.ProviderName).ToListAsync();

            return View(provider);
        }

        // GET: Provider/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Provider 
                .FirstOrDefaultAsync(m => m.id == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        [HttpGet]
        public JsonResult IsExist(string ProviderName, int id)
        {
            return Json(!_context.Provider.Any(x => x.ProviderName == ProviderName && x.id != id));
        }

        // GET: Provider/Create
        public IActionResult Create()
        { 
            return View();
        }

        // POST: Provider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ProviderName,DateTimeModified,DateTimeAdded")] Provider provider)
        {
            if (ModelState.IsValid)
            {
                provider.DateTimeAdded = Utility.GetLocalDateTime();
                provider.DateTimeModified = Utility.GetLocalDateTime();
                _context.Add(provider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(provider);
        }

        // GET: Provider/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Provider.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }
            return View(provider);
        }

        // POST: Provider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ProviderName,DateTimeModified,DateTimeAdded")] Provider provider)
        {
            if (id != provider.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    provider.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(provider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderExists(provider.id))
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
            return View(provider);
        }

        // GET: Provider/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Provider
                .FirstOrDefaultAsync(m => m.id == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // POST: Provider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            //remove associated map 
            var providerTradingProfileIntroducerMap = await (from t in _context.ProviderTradingProfile
                                                             join m in _context.ProviderTradingProfileIntroducerMap on t.id equals m.ProviderTradingProfileID
                                                             where t.ProviderID == id
                                                           select m).ToListAsync();

            _context.ProviderTradingProfileIntroducerMap.RemoveRange(providerTradingProfileIntroducerMap);

            //remove associated TP
            var providerTradingProfile = await _context.ProviderTradingProfile
                .Where(m => m.ProviderID == id).ToListAsync();

            _context.ProviderTradingProfile.RemoveRange(providerTradingProfile);

            //remove associated bank
            var providerBankAccount = await _context.ProviderBankAccount
                .Where(m => m.ProviderID == id).ToListAsync();

            _context.ProviderBankAccount.RemoveRange(providerBankAccount);
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            
            var provider = await _context.Provider.FindAsync(id);
            _context.Provider.Remove(provider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderExists(int id)
        {
            return _context.Provider.Any(e => e.id == id);
        }
    }
}
