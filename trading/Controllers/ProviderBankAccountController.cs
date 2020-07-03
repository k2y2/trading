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
    public class ProviderBankAccountController : Controller
    {
        private readonly tradingContext _context;

        public ProviderBankAccountController(tradingContext context)
        {
            _context = context;
        }

        // GET: ProviderBankAccount
        public async Task<IActionResult> Index(int providerID)
        {
            ViewBag.Provider = new SelectList(_context.ProviderBankAccountView.ToList()
                      .GroupBy(m => m.ProviderID).Select(m => m.First()).OrderBy(m => m.ProviderName), "ProviderID", "ProviderName", providerID);

            var providerBankAccount = await _context.ProviderBankAccountView
                .Where(m =>
                  (providerID == 0 || m.ProviderID == providerID)
                ).OrderBy(m => m.ProviderName).ThenBy(m => m.AccountName).ToListAsync();

            return View(providerBankAccount);
        }

        // GET: ProviderBankAccount/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerBankAccount = await _context.ProviderBankAccountView
                .FirstOrDefaultAsync(m => m.id == id);
            if (providerBankAccount == null)
            {
                return NotFound();
            }

            return View(providerBankAccount);
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
            return Json(!_context.ProviderBankAccount.Any(m => m.AccountName == AccountName && m.id != id));
        }

        // GET: ProviderBankAccount/Create
        public IActionResult Create()
        {
            ViewBag.Provider = new SelectList(_context.Provider.OrderBy(m => m.ProviderName).ToList(), "id", "ProviderName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");

            return View();
        }

        // POST: ProviderBankAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ProviderID,AccountName,CurrencyID,AccountTypeID,AccountHolderName,AccountHolderAddress,AccountNo,IBAN,BSB,BankCode,BankName,SWIFT,CountryID,BranchAddress,Reference,DateTimeModified,DateTimeAdded")] ProviderBankAccount providerBankAccount)
        {
            if (ModelState.IsValid)
            {
                providerBankAccount.DateTimeModified = Utility.GetLocalDateTime();
                providerBankAccount.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(providerBankAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Provider = new SelectList(_context.Provider.OrderBy(m => m.ProviderName).ToList(), "id", "ProviderName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");

            return View(providerBankAccount);
        }

        // GET: ProviderBankAccount/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerBankAccount = await _context.ProviderBankAccount.FindAsync(id);
            if (providerBankAccount == null)
            {
                return NotFound();
            }
            ViewBag.Provider = new SelectList(_context.Provider.OrderBy(m => m.ProviderName).ToList(), "id", "ProviderName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");

            return View(providerBankAccount);
        }

        // POST: ProviderBankAccount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ProviderID,AccountName,CurrencyID,AccountTypeID,AccountHolderName,AccountHolderAddress,AccountNo,IBAN,BSB,BankCode,BankName,SWIFT,CountryID,BranchAddress,Reference,DateTimeModified,DateTimeAdded")] ProviderBankAccount providerBankAccount)
        {
            if (id != providerBankAccount.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    providerBankAccount.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(providerBankAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderBankAccountExists(providerBankAccount.id))
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
            ViewBag.Provider = new SelectList(_context.Provider.OrderBy(m => m.ProviderName).ToList(), "id", "ProviderName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");

            return View(providerBankAccount);
        }

        // GET: ProviderBankAccount/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerBankAccount = await _context.ProviderBankAccountView
                .FirstOrDefaultAsync(m => m.id == id);
            if (providerBankAccount == null)
            {
                return NotFound();
            }

            return View(providerBankAccount);
        }

        // POST: ProviderBankAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var providerBankAccount = await _context.ProviderBankAccount.FindAsync(id);
            _context.ProviderBankAccount.Remove(providerBankAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderBankAccountExists(int id)
        {
            return _context.ProviderBankAccount.Any(e => e.id == id);
        }
    }
}
