﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using trading.Class;
using trading.Models;

namespace trading.Controllers
{
    public class TxnController : Controller
    {
        private readonly tradingContext _context;

        public TxnController(tradingContext context)
        {
            _context = context;
        }

        // GET: Txn
        public async Task<IActionResult> Index(int clientTradingProfileID, int providerTradingProfileID, DateTime dateFilterStart, DateTime dateFilterEnd)
        {
            ViewBag.ClientTradingProfile = new SelectList(_context.ClientTradingProfile.ToList()
                      .OrderBy(m => m.ClientTradingProfileName), "id", "ClientTradingProfileName", clientTradingProfileID);

            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.ToList()
                      .OrderBy(m => m.ProviderTradingProfileName), "id", "ProviderTradingProfileName", providerTradingProfileID);

            //date range
            if (dateFilterStart == DateTime.MinValue) dateFilterStart = new DateTime(Utility.GetLocalDateTime().Date.Year, Utility.GetLocalDateTime().Date.Month, 1); ;
            if (dateFilterEnd == DateTime.MinValue) dateFilterEnd = Utility.GetLocalDateTime().Date;// Utility.GetLocalDateTime().Date;

            ViewBag.dateFilterStart = dateFilterStart.ToString("yyyy-MM-dd");
            ViewBag.dateFilterEnd = dateFilterEnd.ToString("yyyy-MM-dd");

            return View(await _context.TxnView
                .Where(x => x.TradeDate >= dateFilterStart && 
                            x.TradeDate <= dateFilterEnd &&
                            (clientTradingProfileID == 0 || x.ClientTradingProfileID == clientTradingProfileID) &&
                            (providerTradingProfileID == 0 || x.ProviderTradingProfileID == providerTradingProfileID))
                .OrderByDescending(x=>x.TradeDate).ToListAsync());
        }

        // GET: Txn/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var txn = await _context.TxnView.FirstOrDefaultAsync(m => m.id == id);

            if (txn == null)
            {
                return NotFound();
            }

            return View(txn);
        }
        
        [HttpGet]
        public JsonResult GetProviderCurrency(int providerTradingProfileID)
        {
            var providerTradingProfile = _context.ProviderTradingProfile.Where(m => m.id == providerTradingProfileID).FirstOrDefault();
            if (providerTradingProfile == null) return null;
            var currencyPairView = _context.CurrencyPairView.Where(m => m.id == providerTradingProfile.CurrencyPairID).FirstOrDefault();

            return Json(JsonConvert.SerializeObject(currencyPairView));
        }

        [HttpGet]
        public JsonResult GetClientCurrency(int clientCurrencyPairID)
        {
            var currencyPairView = _context.CurrencyPairView.Where(m => m.id == clientCurrencyPairID).FirstOrDefault();

            return Json(JsonConvert.SerializeObject(currencyPairView));
        }

        [HttpGet]
        public JsonResult GetClientCurrencyPairPriceRate(int clientTradingProfileID)
        {
            var clientTradingProfile = _context.ClientTradingProfile.Where(m => m.id == clientTradingProfileID).FirstOrDefault();
             
            return Json(JsonConvert.SerializeObject(clientTradingProfile)); 
        }

        [HttpGet]
        public JsonResult GetDfr(DateTime tradeDate, short clientCurrencyPairID)
        {
            //use inversed CurrencyPair
            short currencyPairID2 = 0;
            var currencyPairView = _context.CurrencyPairView.Where(m => m.id== clientCurrencyPairID).FirstOrDefault();
            if (currencyPairView != null) currencyPairID2 = currencyPairView.CurrencyPairID2;

            var dfr = _context.Dfr.Where(m => m.TradeDate == tradeDate && m.CurrencyPairID == currencyPairID2).FirstOrDefault();
             
            return Json(JsonConvert.SerializeObject(dfr)); 
        }

        [HttpGet]
        public JsonResult GetCostRate(DateTime providerCostDate, int providerTradingProfileID)
        {
            var costRate = _context.CostRate.Where(m => m.TradeDate == providerCostDate && m.ProviderTradingProfileID == providerTradingProfileID).FirstOrDefault();

            return Json(JsonConvert.SerializeObject(costRate));
        }

        // GET: Txn/CreateDeposit
        public IActionResult CreateDeposit()
        {
            ViewBag.ClientTradingProfile = new SelectList(_context.ClientTradingProfile.OrderBy(x => x.ClientTradingProfileName).ToList(), "id", "ClientTradingProfileName");
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(x => x.CurrencyPairName).ToList(), "id", "CurrencyPairName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(x => x.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.ProviderBankAccount = new SelectList(_context.ProviderBankAccount.OrderBy(x => x.AccountName).ToList(), "id", "AccountName");
            ViewBag.Deposit = new SelectList(_context.Txn.ToList().Where(t => t.Type == "D" && t.Status != "C").OrderBy(x => x.ReferenceNo), "id", "ReferenceNo");
            //ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.OrderBy(x => x.ProviderTradingProfileName).ToList(), "id", "ProviderTradingProfileName");
            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfileView.OrderBy(x => x.ProviderTradingProfileName)
                .Select(x => new
                {
                    id = x.id,
                    text = x.ProviderTradingProfileName + ", " + x.CurrencyPairName 
                }).ToList(), "id", "text");

            Txn txn = new Txn();
            txn.ReferenceNo = "D" + Utility.GetLocalDateTime().ToString("yyyyMMddHHmmss");
            txn.Type = "D";
            txn.TradeDate = Utility.GetLocalDateTime().Date;
            txn.ProviderCostDate = Utility.GetLocalDateTime().Date;

            return View("Create", txn);
        }

        // GET: Txn/CreateLQuote
        public IActionResult CreateLQuote()
        {
            ViewBag.ClientTradingProfile = new SelectList(_context.ClientTradingProfile.OrderBy(x => x.ClientTradingProfileName).ToList(), "id", "ClientTradingProfileName");
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(x => x.CurrencyPairName).ToList(), "id", "CurrencyPairName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(x => x.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.ProviderBankAccount = new SelectList(_context.ProviderBankAccount.OrderBy(x => x.AccountName).ToList(), "id", "AccountName");
            //ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.OrderBy(x => x.ProviderTradingProfileName).ToList(), "id", "ProviderTradingProfileName");
            ViewBag.Deposit = new SelectList(_context.Txn.ToList().Where(t => t.Type == "D" && t.Status != "C").OrderBy(x => x.ReferenceNo), "id", "ReferenceNo");
            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfileView.OrderBy(x => x.ProviderTradingProfileName)
                .Select(x => new
                {
                    id = x.id,
                    text = x.ProviderTradingProfileName + ", " + x.CurrencyPairName
                }).ToList(), "id", "text");

            Txn txn = new Txn();
            txn.ReferenceNo = "L" + Utility.GetLocalDateTime().ToString("yyyyMMddHHmmss");
            txn.Type = "L";
            txn.TradeDate = Utility.GetLocalDateTime().Date;

            return View("Create", txn);
        }

        // GET: Txn/CreateQuote
        public IActionResult CreateQuote()
        {
            ViewBag.ClientTradingProfile = new SelectList(_context.ClientTradingProfile.OrderBy(x => x.ClientTradingProfileName).ToList(), "id", "ClientTradingProfileName");
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(x => x.CurrencyPairName).ToList(), "id", "CurrencyPairName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(x => x.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.ProviderBankAccount = new SelectList(_context.ProviderBankAccount.OrderBy(x => x.AccountName).ToList(), "id", "AccountName");
            //ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.OrderBy(x => x.ProviderTradingProfileName).ToList(), "id", "ProviderTradingProfileName");
            ViewBag.Deposit = new SelectList(_context.Txn.ToList().Where(t => t.Type == "D" && t.Status != "C").OrderBy(x => x.ReferenceNo), "id", "ReferenceNo");
            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfileView.OrderBy(x => x.ProviderTradingProfileName)
                .Select(x => new
                {
                    id = x.id,
                    text = x.ProviderTradingProfileName + ", " + x.CurrencyPairName
                }).ToList(), "id", "text");

            Txn txn = new Txn();
            txn.ReferenceNo = "Q" + Utility.GetLocalDateTime().ToString("yyyyMMddHHmmss");
            txn.Type = "Q";
            txn.TradeDate = Utility.GetLocalDateTime().Date;
            txn.ProviderCostDate = Utility.GetLocalDateTime().Date;

            return View("Create", txn);
        }

        // POST: Txn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ReferenceNo,Type,LinkedDepositID,TradeDate,ClientTradingProfileID,Status,ClientPriceRate,ClientCurrencyPairID,ClientDfrRate,ClientUniqueDfr,ClientExRate,ClientCurrencyIDIn,ClientAmountIn,ClientCurrencyIDOut,ClientAmountOut,ClientPayOutAccountID,ProviderTradingProfileID,ProviderCurrencyID,ProviderCostDate,ProviderCostRate,ProviderExpectedPayInAmount,ProviderBankAccountID,DateTimeModified,DateTimeAdded")] Txn txn)
        {
            if (ModelState.IsValid)
            {
                txn.DateTimeModified = Utility.GetLocalDateTime();
                txn.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(txn);

                if (txn.Type == "L")
                {
                    var deposit = await _context.Txn.FindAsync(txn.LinkedDepositID);
                    deposit.Status = "C";
                    deposit.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(deposit);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ClientTradingProfile = new SelectList(_context.ClientTradingProfile.OrderBy(x => x.ClientTradingProfileName).ToList(), "id", "ClientTradingProfileName");
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(x => x.CurrencyPairName).ToList(), "id", "CurrencyPairName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(x => x.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.ProviderBankAccount = new SelectList(_context.ProviderBankAccount.OrderBy(x => x.AccountName).ToList(), "id", "AccountName");
            //ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.OrderBy(x => x.ProviderTradingProfileName).ToList(), "id", "ProviderTradingProfileName");
            ViewBag.Deposit = new SelectList(_context.Txn.ToList().Where(t => t.Type == "D" && t.Status != "C").OrderBy(x => x.ReferenceNo), "id", "ReferenceNo");
            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfileView.OrderBy(x => x.ProviderTradingProfileName)
                .Select(x => new
                {
                    id = x.id,
                    text = x.ProviderTradingProfileName + ", " + x.CurrencyPairName
                }).ToList(), "id", "text");

            return View(txn);
        }

        // GET: Txn/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var txn = await _context.Txn.FindAsync(id);
            if (txn == null)
            {
                return NotFound();
            }

            ViewBag.ClientTradingProfile = new SelectList(_context.ClientTradingProfile.OrderBy(x => x.ClientTradingProfileName).ToList(), "id", "ClientTradingProfileName");
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(x => x.CurrencyPairName).ToList(), "id", "CurrencyPairName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(x => x.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.ProviderBankAccount = new SelectList(_context.ProviderBankAccount.OrderBy(x => x.AccountName).ToList(), "id", "AccountName");
            //ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.OrderBy(x => x.ProviderTradingProfileName).ToList(), "id", "ProviderTradingProfileName");
            ViewBag.Deposit = new SelectList(_context.Txn.ToList().Where(t => (t.Type == "D" && t.Status != "C") || t.id == txn.LinkedDepositID).OrderBy(x => x.ReferenceNo), "id", "ReferenceNo");
            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfileView.OrderBy(x => x.ProviderTradingProfileName)
                .Select(x => new
                {
                    id = x.id,
                    text = x.ProviderTradingProfileName + ", " + x.CurrencyPairName
                }).ToList(), "id", "text");

            return View(txn);
        }

        // POST: Txn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ReferenceNo,Type,LinkedDepositID,TradeDate,ClientTradingProfileID,Status,ClientPriceRate,ClientCurrencyPairID,ClientDfrRate,ClientUniqueDfr,ClientExRate,ClientCurrencyIDIn,ClientAmountIn,ClientCurrencyIDOut,ClientAmountOut,ClientPayOutAccountID,ProviderTradingProfileID,ProviderCurrencyID,ProviderCostDate,ProviderCostRate,ProviderExpectedPayInAmount,ProviderBankAccountID,DateTimeModified,DateTimeAdded")] Txn txn)
        {
            if (id != txn.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Txn txnOld;
                    if (txn.Type == "L")
                    {
                        txnOld = await _context.Txn.AsNoTracking().SingleOrDefaultAsync(m => m.id == txn.id);
                        if (txnOld.LinkedDepositID != txn.LinkedDepositID)
                        {
                            var depositOld = await _context.Txn.FindAsync(txnOld.LinkedDepositID);
                            depositOld.Status = "";
                            depositOld.DateTimeModified = Utility.GetLocalDateTime();
                            _context.Update(depositOld);

                            var depositNew = await _context.Txn.FindAsync(txn.LinkedDepositID);
                            depositNew.Status = "C";
                            depositNew.DateTimeModified = Utility.GetLocalDateTime();
                            _context.Update(depositNew);
                        }
                    }

                    txn.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(txn);
                    await _context.SaveChangesAsync();

                    //check ClientPayoutMissing, update Status
                    var reportTxn = await _context.ReportTxn.AsNoTracking().SingleOrDefaultAsync(m => m.TxnID == txn.id);
                    if (reportTxn == null || reportTxn.ClientPayoutMissing > 0)
                        txn.Status = "";
                    else
                        txn.Status = "C";

                    _context.Update(txn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TxnExists(txn.id))
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

            ViewBag.ClientTradingProfile = new SelectList(_context.ClientTradingProfile.OrderBy(x => x.ClientTradingProfileName).ToList(), "id", "ClientTradingProfileName");
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(x => x.CurrencyPairName).ToList(), "id", "CurrencyPairName");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(x => x.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.ProviderBankAccount = new SelectList(_context.ProviderBankAccount.OrderBy(x => x.AccountName).ToList(), "id", "AccountName");
            //ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.OrderBy(x => x.ProviderTradingProfileName).ToList(), "id", "ProviderTradingProfileName");
            ViewBag.Deposit = new SelectList(_context.Txn.ToList().Where(t => (t.Type == "D" && t.Status != "C") || t.id == txn.LinkedDepositID).OrderBy(x => x.ReferenceNo), "id", "ReferenceNo");
            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfileView.OrderBy(x => x.ProviderTradingProfileName)
                .Select(x => new
                {
                    id = x.id,
                    text = x.ProviderTradingProfileName + ", " + x.CurrencyPairName
                }).ToList(), "id", "text");

            return View(txn);
        }

        // GET: Txn/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var txn = await _context.TxnView.FirstOrDefaultAsync(m => m.id == id);

            if (txn == null)
            {
                return NotFound();
            }

            return View(txn);
        }

        // POST: Txn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var txn = await _context.Txn.FindAsync(id);

            if (txn.Type == "D")
            {
                var linkedQuote = await _context.Txn.AsNoTracking().SingleOrDefaultAsync(m => m.LinkedDepositID == txn.id);
                if (linkedQuote != null)
                {
                    _context.Txn.Remove(linkedQuote);
                }
            }
            else if (txn.Type == "L")
            {
                var linkedDeposit = await _context.Txn.AsNoTracking().SingleOrDefaultAsync(m => m.id == txn.LinkedDepositID);
                if (linkedDeposit != null)
                {
                    linkedDeposit.Status = "";
                    linkedDeposit.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(linkedDeposit);
                }
            }

            _context.Txn.Remove(txn);

            //remove associated payout
            var payout = await _context.Payout.Where(m => m.TxnID == id).ToListAsync(); 
            _context.Payout.RemoveRange(payout);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TxnExists(int id)
        {
            return _context.Txn.Any(e => e.id == id);
        }
    }
}
