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
    public class AccountAdjustController : Controller
    {
        private readonly tradingContext _context;

        public AccountAdjustController(tradingContext context)
        {
            _context = context;
        }

        // GET: AccountAdjust
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccountAdjustView.ToListAsync());
        }

        // GET: AccountAdjust/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountAdjust = await _context.AccountAdjustView
                .FirstOrDefaultAsync(m => m.id == id);
            if (accountAdjust == null)
            {
                return NotFound();
            }

            return View(accountAdjust);
        }

        // GET: AccountAdjust/Create
        public IActionResult Create()
        { 
            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(m => m.AccountName).ToList(), "id", "AccountName");

            AccountAdjust accountAdjust = new AccountAdjust();
            accountAdjust.ReferenceNo = "A" + Utility.GetLocalDateTime().ToString("yyyyMMddHHmmss");
            return View(accountAdjust);
        }

        // POST: AccountAdjust/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ReferenceNo,AccountBankAccountID,Amount,Reference,DateTimeModified,DateTimeAdded")] AccountAdjust accountAdjust)
        {
            if (ModelState.IsValid)
            {
                accountAdjust.DateTimeModified = Utility.GetLocalDateTime();// Utility.GetLocalDateTime();
                accountAdjust.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(accountAdjust);
                await _context.SaveChangesAsync();

                /////////////////////////////////////////////////////////////////////////////
                //update AccountTxn
                AccountTxn accountTxn = new AccountTxn();
                accountTxn.Type = "A";
                accountTxn.ReferenceID = accountAdjust.id;
                accountTxn.AccountBankAccountID = accountAdjust.AccountBankAccountID;
                if (accountAdjust.Amount >= 0)
                    accountTxn.AmountCredit = accountAdjust.Amount;
                else
                    accountTxn.AmountDebit = Math.Abs(accountAdjust.Amount);

                accountTxn.DateTimeModified = Utility.GetLocalDateTime();
                accountTxn.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(accountTxn);
                /////////////////////////////////////////////////////////////////////////////

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(m => m.AccountName).ToList(), "id", "AccountName");
            return View(accountAdjust);
        }

        // GET: AccountAdjust/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountAdjust = await _context.AccountAdjust.FindAsync(id);
            if (accountAdjust == null)
            {
                return NotFound();
            }

            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(m => m.AccountName).ToList(), "id", "AccountName");

            return View(accountAdjust);
        }

        // POST: AccountAdjust/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ReferenceNo,AccountBankAccountID,Amount,Reference,DateTimeModified,DateTimeAdded")] AccountAdjust accountAdjust)
        {
            if (id != accountAdjust.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    accountAdjust.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(accountAdjust);

                    /////////////////////////////////////////////////////////////////////////////
                    //update AccountTxn
                    var accountTxn = await _context.AccountTxn.AsNoTracking().SingleOrDefaultAsync(m => m.Type == "A" && m.ReferenceID == id);
                    if (accountTxn != null)
                    {
                        accountTxn.AccountBankAccountID = accountAdjust.AccountBankAccountID;
                        if (accountAdjust.Amount >= 0)
                        {
                            accountTxn.AmountCredit = accountAdjust.Amount;
                            accountTxn.AmountDebit = null;
                        }
                        else
                        {
                            accountTxn.AmountCredit = null;
                            accountTxn.AmountDebit = Math.Abs(accountAdjust.Amount);
                        }
                        accountTxn.DateTimeModified = Utility.GetLocalDateTime();
                        _context.Update(accountTxn);
                    }
                    /////////////////////////////////////////////////////////////////////////////
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountAdjustExists(accountAdjust.id))
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

            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(m => m.AccountName).ToList(), "id", "AccountName");

            return View(accountAdjust);
        }

        // GET: AccountAdjust/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountAdjust = await _context.AccountAdjustView
                .FirstOrDefaultAsync(m => m.id == id);
            if (accountAdjust == null)
            {
                return NotFound();
            }

            return View(accountAdjust);
        }

        // POST: AccountAdjust/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountAdjust = await _context.AccountAdjust.FindAsync(id);
            _context.AccountAdjust.Remove(accountAdjust);

            /////////////////////////////////////////////////////////////////////////////
            //update AccountTxn
            var accountTxn = await _context.AccountTxn.AsNoTracking().SingleOrDefaultAsync(m => m.Type == "A" &&  m.ReferenceID == id);
            if (accountTxn != null) _context.AccountTxn.Remove(accountTxn);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountAdjustExists(int id)
        {
            return _context.AccountAdjust.Any(e => e.id == id);
        }
    }
}
