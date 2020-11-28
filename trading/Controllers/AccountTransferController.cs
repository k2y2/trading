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
    public class AccountTransferController : Controller
    {
        private readonly tradingContext _context;

        public AccountTransferController(tradingContext context)
        {
            _context = context;
        }

        // GET: AccountTransfer
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccountTransferView.ToListAsync());
        }

        // GET: AccountTransfer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountTransfer = await _context.AccountTransferView
                .FirstOrDefaultAsync(m => m.id == id);
            if (accountTransfer == null)
            {
                return NotFound();
            }

            return View(accountTransfer);
        }

        // GET: AccountTransfer/Create
        public IActionResult Create()
        {
            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(m => m.AccountName).ToList(), "id", "AccountName");

            AccountTransfer accountTransfer = new AccountTransfer();
            accountTransfer.ReferenceNo = "T" + Utility.GetLocalDateTime().ToString("yyyyMMddHHmmss");
            return View(accountTransfer);
        }

        // POST: AccountTransfer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ReferenceNo,AccountBankAccountIDFrom,AccountBankAccountIDTo,Amount,Rate,ActualDate,Reference,DateTimeModified,DateTimeAdded")] AccountTransfer accountTransfer)
        {
            if (ModelState.IsValid)
            {
                accountTransfer.DateTimeModified = Utility.GetLocalDateTime();
                accountTransfer.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(accountTransfer);
                await _context.SaveChangesAsync();

                /////////////////////////////////////////////////////////////////////////////
                //update AccountTxn
                AccountTxn accountTxnFrom = new AccountTxn();
                accountTxnFrom.Type = "T";
                accountTxnFrom.ReferenceID = accountTransfer.id;
                accountTxnFrom.AccountBankAccountID = accountTransfer.AccountBankAccountIDFrom;
                accountTxnFrom.AmountDebit = accountTransfer.Amount;
                accountTxnFrom.DateTimeModified = Utility.GetLocalDateTime();
                accountTxnFrom.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(accountTxnFrom);

                AccountTxn accountTxnTo = new AccountTxn();
                accountTxnTo.Type = "T";
                accountTxnTo.ReferenceID = accountTransfer.id;
                accountTxnTo.AccountBankAccountID = accountTransfer.AccountBankAccountIDTo;
                accountTxnTo.AmountCredit = accountTransfer.Amount * accountTransfer.Rate;
                accountTxnTo.DateTimeModified = Utility.GetLocalDateTime();
                accountTxnTo.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(accountTxnTo); 
                /////////////////////////////////////////////////////////////////////////////

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(m => m.AccountName).ToList(), "id", "AccountName");

            return View(accountTransfer);
        }

        // GET: AccountTransfer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountTransfer = await _context.AccountTransfer.FindAsync(id);
            if (accountTransfer == null)
            {
                return NotFound();
            }

            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(m => m.AccountName).ToList(), "id", "AccountName");

            return View(accountTransfer);
        }

        // POST: AccountTransfer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ReferenceNo,AccountBankAccountIDFrom,AccountBankAccountIDTo,Amount,Rate,ActualDate,Reference,DateTimeModified,DateTimeAdded")] AccountTransfer accountTransfer)
        {
            if (id != accountTransfer.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    accountTransfer.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(accountTransfer);

                    /////////////////////////////////////////////////////////////////////////////
                    //update AccountTxn
                    var accountTxn = await _context.AccountTxn.Where(m => m.Type == "T" && m.ReferenceID == id).ToListAsync();
                    if (accountTxn != null) _context.AccountTxn.RemoveRange(accountTxn);

                    AccountTxn accountTxnFrom = new AccountTxn();
                    accountTxnFrom.Type = "T";
                    accountTxnFrom.ReferenceID = id;
                    accountTxnFrom.AccountBankAccountID = accountTransfer.AccountBankAccountIDFrom;
                    accountTxnFrom.AmountDebit = accountTransfer.Amount;
                    accountTxnFrom.DateTimeModified = Utility.GetLocalDateTime();
                    accountTxnFrom.DateTimeAdded = Utility.GetLocalDateTime();
                    _context.Add(accountTxnFrom);

                    AccountTxn accountTxnTo = new AccountTxn();
                    accountTxnTo.Type = "T";
                    accountTxnTo.ReferenceID = id;
                    accountTxnTo.AccountBankAccountID = accountTransfer.AccountBankAccountIDTo;
                    accountTxnTo.AmountCredit = accountTransfer.Amount * accountTransfer.Rate;
                    accountTxnTo.DateTimeModified = Utility.GetLocalDateTime();
                    accountTxnTo.DateTimeAdded = Utility.GetLocalDateTime();
                    _context.Add(accountTxnTo);
                    /////////////////////////////////////////////////////////////////////////////

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountTransferExists(accountTransfer.id))
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

            return View(accountTransfer);
        }

        // GET: AccountTransfer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountTransfer = await _context.AccountTransferView
                .FirstOrDefaultAsync(m => m.id == id);
            if (accountTransfer == null)
            {
                return NotFound();
            }

            return View(accountTransfer);
        }

        // POST: AccountTransfer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountTransfer = await _context.AccountTransfer.FindAsync(id);
            _context.AccountTransfer.Remove(accountTransfer);

            /////////////////////////////////////////////////////////////////////////////
            //update AccountTxn
            var accountTxn = await _context.AccountTxn.Where(m => m.Type == "T" && m.ReferenceID == id).ToListAsync();
            if (accountTxn != null) _context.AccountTxn.RemoveRange(accountTxn);
            /////////////////////////////////////////////////////////////////////////////

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountTransferExists(int id)
        {
            return _context.AccountTransfer.Any(e => e.id == id);
        }
    }
}
