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
    public class IntroducerBankAccountController : Controller
    {
        private readonly tradingContext _context;

        public IntroducerBankAccountController(tradingContext context)
        {
            _context = context;
        }

        // GET: IntroducerBankAccount
        public async Task<IActionResult> Index(int introducerID)
        {
            ViewBag.Introducer = new SelectList(_context.IntroducerBankAccountView.ToList()
                   .GroupBy(m => m.IntroducerID).Select(m => m.First()).OrderBy(m => m.IntroducerName), "IntroducerID", "IntroducerName", introducerID);

            var introducerBankAccount = await _context.IntroducerBankAccountView
                .Where(m =>
                  (introducerID == 0 || m.IntroducerID == introducerID)
                ).OrderBy(m => m.IntroducerName).ThenBy(m => m.AccountName).ToListAsync();

            return View(introducerBankAccount);
        }

        // GET: IntroducerBankAccount/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var introducerBankAccount = await _context.IntroducerBankAccountView
                .FirstOrDefaultAsync(m => m.id == id);
            if (introducerBankAccount == null)
            {
                return NotFound();
            }

            return View(introducerBankAccount);
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
        public JsonResult IsExist(string AccountName, int id, int IntroducerID)
        {
            return Json(!_context.IntroducerBankAccount.Any(m => m.AccountName == AccountName && m.IntroducerID == IntroducerID && m.id != id));
        }
          
        // GET: IntroducerBankAccount/Create
        public IActionResult Create()
        {
            ViewBag.Introducer = new SelectList(_context.Introducer.OrderBy(m => m.IntroducerName).ToList(), "id", "IntroducerName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");

            return View();
        }

        // POST: IntroducerBankAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,IntroducerID,AccountName,CurrencyID,AccountTypeID,AccountHolderName,AccountHolderAddress,AccountNo,IBAN,BSB,BankCode,BankName,SWIFT,CountryID,BranchAddress,Reference,DateTimeModified,DateTimeAdded")] IntroducerBankAccount introducerBankAccount)
        {
            if (ModelState.IsValid)
            {
                introducerBankAccount.DateTimeModified = Utility.GetLocalDateTime();
                introducerBankAccount.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(introducerBankAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Introducer = new SelectList(_context.Introducer.OrderBy(m => m.IntroducerName).ToList(), "id", "IntroducerName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");

            return View(introducerBankAccount);
        }

        // GET: IntroducerBankAccount/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var introducerBankAccount = await _context.IntroducerBankAccount.FindAsync(id);
            if (introducerBankAccount == null)
            {
                return NotFound();
            }
            ViewBag.Introducer = new SelectList(_context.Introducer.OrderBy(m => m.IntroducerName).ToList(), "id", "IntroducerName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");
 
            return View(introducerBankAccount);
        }

        // POST: IntroducerBankAccount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,IntroducerID,AccountName,CurrencyID,AccountTypeID,AccountHolderName,AccountHolderAddress,AccountNo,IBAN,BSB,BankCode,BankName,SWIFT,CountryID,BranchAddress,Reference,DateTimeModified,DateTimeAdded")] IntroducerBankAccount introducerBankAccount)
        {
            if (id != introducerBankAccount.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    introducerBankAccount.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(introducerBankAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntroducerBankAccountExists(introducerBankAccount.id))
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
            ViewBag.Introducer = new SelectList(_context.Introducer.OrderBy(m => m.IntroducerName).ToList(), "id", "IntroducerName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");
 
            return View(introducerBankAccount);
        }

        // GET: IntroducerBankAccount/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var introducerBankAccount = await _context.IntroducerBankAccountView
                .FirstOrDefaultAsync(m => m.id == id);
            if (introducerBankAccount == null)
            {
                return NotFound();
            }

            return View(introducerBankAccount);
        }

        // POST: IntroducerBankAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var introducerBankAccount = await _context.IntroducerBankAccount.FindAsync(id);
            _context.IntroducerBankAccount.Remove(introducerBankAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntroducerBankAccountExists(int id)
        {
            return _context.IntroducerBankAccount.Any(e => e.id == id);
        }
    }
}
