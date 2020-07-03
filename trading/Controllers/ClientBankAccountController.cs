using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using trading.Class;
using trading.Models;

namespace trading.Controllers
{
    public class ClientBankAccountController : Controller
    {
        private readonly tradingContext _context;

        public ClientBankAccountController(tradingContext context)
        {
            _context = context;
        }

        // GET: ClientBankAccount
        public async Task<IActionResult> Index(int clientID)
        {
            ViewBag.Client = new SelectList(_context.ClientBankAccountView.ToList()
                .GroupBy(m => m.ClientID).Select(m => m.First()).OrderBy(m => m.ClientName), "ClientID", "ClientName", clientID);
              
            var clientBankAccount = await _context.ClientBankAccountView
                .Where(m => 
                  (clientID == 0 || m.ClientID == clientID)
                ).OrderBy(m => m.ClientName).ThenBy(m => m.AccountName).ToListAsync();

            return View(clientBankAccount);
        }

        // GET: ClientBankAccount/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientBankAccount = await _context.ClientBankAccountView
                .FirstOrDefaultAsync(m => m.id == id);
            if (clientBankAccount == null)
            {
                return NotFound();
            }

            return View(clientBankAccount);
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
        public JsonResult IsExist(string AccountName, int id, int ClientID)
        {
            return Json(!_context.ClientBankAccount.Any(m => m.AccountName == AccountName && m.ClientID == ClientID && m.id != id));
        }

        // GET: ClientBankAccount/Create
        public IActionResult Create()
        {
            ViewBag.Client = new SelectList(_context.Client.OrderBy(m=>m.ClientName).ToList(), "id", "ClientName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");

            return View();
        }

        // POST: ClientBankAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ClientID,AccountName,CurrencyID,AccountTypeID,AccountHolderName,AccountHolderAddress,AccountNo,IBAN,BSB,BankCode,BankName,SWIFT,CountryID,BranchAddress,Reference,DateTimeModified,DateTimeAdded")] ClientBankAccount clientBankAccount)
        {
            if (ModelState.IsValid)
            {
                clientBankAccount.DateTimeModified = Utility.GetLocalDateTime();
                clientBankAccount.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(clientBankAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } 
            ViewBag.Client = new SelectList(_context.Client.OrderBy(m => m.ClientName).ToList(), "id", "ClientName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");

            return View(clientBankAccount);
        }

        // GET: ClientBankAccount/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientBankAccount = await _context.ClientBankAccount.FindAsync(id);
            if (clientBankAccount == null)
            {
                return NotFound();
            }
            ViewBag.Client = new SelectList(_context.Client.OrderBy(m => m.ClientName).ToList(), "id", "ClientName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");
 
            return View(clientBankAccount);
        }

        // POST: ClientBankAccount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ClientID,AccountName,CurrencyID,AccountTypeID,AccountHolderName,AccountHolderAddress,AccountNo,IBAN,BSB,BankCode,BankName,SWIFT,CountryID,BranchAddress,Reference,DateTimeModified,DateTimeAdded")] ClientBankAccount clientBankAccount)
        {
            if (id != clientBankAccount.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    clientBankAccount.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(clientBankAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientBankAccountExists(clientBankAccount.id))
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
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountType = new SelectList(_context.AccountType.ToList(), "id", "AccountTypeName");
            ViewBag.Country = new SelectList(_context.Country.OrderBy(m => m.CountryName).ToList(), "id", "CountryName");
 
            return View(clientBankAccount);
        }

        // GET: ClientBankAccount/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientBankAccount = await _context.ClientBankAccountView
                .FirstOrDefaultAsync(m => m.id == id);
            if (clientBankAccount == null)
            {
                return NotFound();
            }

            return View(clientBankAccount);
        }

        // POST: ClientBankAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientBankAccount = await _context.ClientBankAccount.FindAsync(id);
            _context.ClientBankAccount.Remove(clientBankAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientBankAccountExists(int id)
        {
            return _context.ClientBankAccount.Any(e => e.id == id);
        }
    }
}
