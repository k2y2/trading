using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using trading.Class;
using trading.Models;

namespace trading.Controllers
{
    public class AccountBankAccountController : Controller
    {
        private readonly tradingContext _context;

        public AccountBankAccountController(tradingContext context)
        {
            _context = context;
        }

        // GET: AccountBankAccount
        public async Task<IActionResult> Index(int accountID)
        {
            ViewBag.Account = new SelectList(_context.AccountBankAccountView.ToList()
                   .GroupBy(m => m.AccountID).Select(m => m.First()).OrderBy(m => m.MasterAccountName), "AccountID", "MasterAccountName", accountID);

            var accountBankAccount = await _context.AccountBankAccountView
                .Where(m =>
                  (accountID == 0 || m.AccountID == accountID)
                ).OrderBy(m => m.MasterAccountName).ThenBy(m => m.AccountName).ToListAsync();

            return View(accountBankAccount);
        }

        // GET: AccountBankAccount/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountBankAccount = await _context.AccountBankAccountView
                .FirstOrDefaultAsync(m => m.id == id);
            if (accountBankAccount == null)
            {
                return NotFound();
            }

            return View(accountBankAccount);
        }

        [HttpGet]
        public async Task<JsonResult> ValidateIBAN(string iban)
        {
            var client = new HttpClient();
            string url = "https://iban.codes/validate/" + iban;

            var rresponse = await client.GetAsync(url);
            var result = await client.GetStringAsync(url);

            var message = "";
            if (result.Contains("This is a valid IBAN"))
                message = "Valid";
            else if (result.Contains("this is an invalid IBAN") || result.Contains("This IBAN is incorrect"))
                message = "Invalid";
            else
                message = "Error: please visit https://iban.codes/";

            return Json(message);
        }

        [HttpGet]
        public JsonResult IsExist(string AccountName, int id)
        {
            return Json(!_context.AccountBankAccount.Any(m => m.AccountName == AccountName && m.id != id));
        }

        // GET: AccountBankAccount/Create
        public IActionResult Create()
        {
            ViewBag.Account = new SelectList(_context.Account.OrderBy(m => m.AccountName).ToList(), "id", "AccountName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");

            return View();
        }

        // POST: AccountBankAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,AccountID,AccountName,CurrencyID,AccountTypeID,AccountHolderName,AccountHolderAddress,AccountNo,IBAN,BSB,BankCode,BankName,SWIFT,CountryID,BranchAddress,Reference,DateTimeModified,DateTimeAdded")] AccountBankAccount accountBankAccount)
        {
            if (ModelState.IsValid)
            {
                accountBankAccount.DateTimeModified = Utility.GetLocalDateTime();
                accountBankAccount.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(accountBankAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Account = new SelectList(_context.Account.OrderBy(m => m.AccountName).ToList(), "id", "AccountName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");

            return View(accountBankAccount);
        }

        // GET: AccountBankAccount/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountBankAccount = await _context.AccountBankAccount.FindAsync(id);
            if (accountBankAccount == null)
            {
                return NotFound();
            }
            ViewBag.Account = new SelectList(_context.Account.OrderBy(m => m.AccountName).ToList(), "id", "AccountName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");

            return View(accountBankAccount);
        }

        // POST: AccountBankAccount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,AccountID,AccountName,CurrencyID,AccountTypeID,AccountHolderName,AccountHolderAddress,AccountNo,IBAN,BSB,BankCode,BankName,SWIFT,CountryID,BranchAddress,Reference,DateTimeModified,DateTimeAdded")] AccountBankAccount accountBankAccount)
        {
            if (id != accountBankAccount.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    accountBankAccount.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(accountBankAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountBankAccountExists(accountBankAccount.id))
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
            ViewBag.Account = new SelectList(_context.Account.OrderBy(m => m.AccountName).ToList(), "id", "AccountName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");

            return View(accountBankAccount);
        }

        // GET: AccountBankAccount/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountBankAccount = await _context.AccountBankAccountView
                .FirstOrDefaultAsync(m => m.id == id);
            if (accountBankAccount == null)
            {
                return NotFound();
            }

            return View(accountBankAccount);
        }

        // POST: AccountBankAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountBankAccount = await _context.AccountBankAccount.FindAsync(id);
            _context.AccountBankAccount.Remove(accountBankAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountBankAccountExists(int id)
        {
            return _context.AccountBankAccount.Any(e => e.id == id);
        }
    }
}
