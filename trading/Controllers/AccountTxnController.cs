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
    public class AccountTxnController : Controller
    {
        private readonly tradingContext _context;

        public AccountTxnController(tradingContext context)
        {
            _context = context;
        }

        // GET: AccountTxn
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") == "S")
                return View(await _context.AccountTxnView.OrderByDescending(m=>m.DateTimeAdded).ToListAsync());

            return RedirectToAction("Logout", "Home"); 
        }

        // GET: AccountTxn/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountTxn = await _context.AccountTxn
                .FirstOrDefaultAsync(m => m.id == id);
            if (accountTxn == null)
            {
                return NotFound();
            }

            return View(accountTxn);
        }

        // GET: AccountTxn/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountTxn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Type,ReferenceID,AccountBankAccountID,AmountCredit,AmountDebit,DateTimeModified,DateTimeAdded")] AccountTxn accountTxn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountTxn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountTxn);
        }

        // GET: AccountTxn/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountTxn = await _context.AccountTxn.FindAsync(id);
            if (accountTxn == null)
            {
                return NotFound();
            }
            return View(accountTxn);
        }

        // POST: AccountTxn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Type,ReferenceID,AccountBankAccountID,AmountCredit,AmountDebit,DateTimeModified,DateTimeAdded")] AccountTxn accountTxn)
        {
            if (id != accountTxn.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountTxn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountTxnExists(accountTxn.id))
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
            return View(accountTxn);
        }

        // GET: AccountTxn/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountTxn = await _context.AccountTxn
                .FirstOrDefaultAsync(m => m.id == id);
            if (accountTxn == null)
            {
                return NotFound();
            }

            return View(accountTxn);
        }

        // POST: AccountTxn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountTxn = await _context.AccountTxn.FindAsync(id);
            _context.AccountTxn.Remove(accountTxn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountTxnExists(int id)
        {
            return _context.AccountTxn.Any(e => e.id == id);
        }
    }
}
