using System;
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
    public class PayoutController : Controller
    {
        private readonly tradingContext _context;

        public PayoutController(tradingContext context)
        {
            _context = context;
        }

        // GET: Payout
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

            return View(await _context.PayoutView
                .Where(x => x.TradeDate >= dateFilterStart &&
                            x.TradeDate <= dateFilterEnd &&
                            (clientTradingProfileID == 0 || x.ClientTradingProfileID == clientTradingProfileID) &&
                            (providerTradingProfileID == 0 || x.LinkedProviderTradingProfileID == providerTradingProfileID))
                .OrderByDescending(x => x.TradeDate).ToListAsync()); 
        }

        // GET: Payout/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payoutView = await _context.PayoutView
                .FirstOrDefaultAsync(m => m.id == id);
            if (payoutView == null)
            {
                return NotFound();
            }

            return View(payoutView);
        }
        
        [HttpGet]
        public JsonResult UpdateTxn(int clientTradingProfileID)
        { 
            var txn = new SelectList(_context.TxnView.Where(t=>(clientTradingProfileID==0 || t.ClientTradingProfileID == clientTradingProfileID) && t.Type != "D" && t.Status != "C" && t.PayoutDone).OrderBy(m => m.ReferenceNo)
                .Select(t => new
                {
                    id = t.id,
                    text = t.ClientTradingProfileName + ", " + t.TradeDate.ToString() + ", " + (t.ClientAmountOut.ToString() ?? "") + ", " + (t.ClientCurrencyNameOut ?? "") + ", " + t.ReferenceNo
                }).ToList(), "id", "text");
            return Json(txn); 
        }

        [HttpGet]
        public JsonResult GetTxn(int txnID)
        {
            var txn = _context.TxnCompleteView.Where(x => x.id == txnID).FirstOrDefault();
            return Json(JsonConvert.SerializeObject(txn));
        }

        // GET: Payout/Create
        public IActionResult Create()
        {            
            ViewBag.ClientTradingProfile = new SelectList(_context.TxnView.Where(t => t.Type != "D" && t.Status != "C" && t.PayoutDone)
                .Select(t => new
                {
                    id = t.ClientTradingProfileID,
                    text = t.ClientTradingProfileName 
                }).Distinct().OrderBy(t => t.text).ToList(), "id", "text"); 

            ViewBag.Txn = new SelectList(_context.TxnView.Where(t => t.Type != "D" && t.Status != "C" && t.PayoutDone).OrderBy(m=>m.ReferenceNo)
                .Select(t => new 
                {
                    id = t.id,
                    text = t.ClientTradingProfileName+", "+t.TradeDate.ToString() + ", " + (t.ClientAmountOut.ToString() ?? "") + ", "+(t.ClientCurrencyNameOut ?? "") + ", " + t.ReferenceNo 
                }).ToList(), "id", "text");
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(x => x.AccountName).ToList(), "id", "AccountName");

            Payout payout = new Payout();
            payout.ReferenceNo = "P" + Utility.GetLocalDateTime().ToString("yyyyMMddHHmmss");
            return View(payout);
        }

        // POST: Payout/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ReferenceNo,TxnID,ClientPayoutAmount,ClientPayoutUSDRate,ProviderPayinUSDRate,UsedCurrencyID,UsedAmount,UsedClientPayoutFXRate,UsedUSDRate,AccountBankAccountID,DateTimeModified,DateTimeAdded")] Payout payout)
        {
            if (ModelState.IsValid)
            {
                payout.DateTimeModified = Utility.GetLocalDateTime();
                payout.DateTimeAdded = Utility.GetLocalDateTime();

                _context.Add(payout);
                await _context.SaveChangesAsync();

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                //check ClientPayoutMissing, update Status
                var reportTxn = await _context.ReportTxnOriginal.AsNoTracking().SingleOrDefaultAsync(m => m.TxnID == payout.TxnID);
                if (reportTxn != null)
                { 
                    var txn = await _context.Txn.AsNoTracking().SingleOrDefaultAsync(m => m.id == payout.TxnID);
                    if (txn != null)
                    {
                        if (reportTxn.ClientPayoutMissing < reportTxn.ClientAmountOut && reportTxn.ClientPayoutMissing > 0)
                            txn.Status = "P";
                        else if (reportTxn.ClientPayoutMissing <= 0)
                            txn.Status = "C";
                        else
                            txn.Status = ""; 

                        _context.Update(txn);
                        await _context.SaveChangesAsync();
                    } 
                }
                /////////////////////////////////////////////////////////////////////////////////////////////////////////// 
                //update AccountTxn
                AccountTxn accountTxn = new AccountTxn();
                accountTxn.Type = "P";
                accountTxn.ReferenceID = payout.id;
                accountTxn.AccountBankAccountID = payout.AccountBankAccountID;
                accountTxn.AmountDebit = payout.ClientPayoutAmount; 
                accountTxn.DateTimeModified = Utility.GetLocalDateTime();
                accountTxn.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(accountTxn);
                await _context.SaveChangesAsync();
                /////////////////////////////////////////////////////////////////////////////
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClientTradingProfile = new SelectList(_context.TxnView.Where(t => t.Type != "D" && t.Status != "C" && t.PayoutDone)
                .Select(t => new
                {
                    id = t.ClientTradingProfileID,
                    text = t.ClientTradingProfileName
                }).Distinct().OrderBy(t => t.text).ToList(), "id", "text");

            ViewBag.Txn = new SelectList(_context.TxnView.Where(t => t.Type != "D" && t.Status != "C" && t.PayoutDone).OrderBy(m => m.ReferenceNo)
                .Select(t => new
                {
                    id = t.id,
                    text = t.ClientTradingProfileName + ", " + t.TradeDate.ToString() + ", " + (t.ClientAmountOut.ToString() ?? "") + ", " + (t.ClientCurrencyNameOut ?? "") + ", " + t.ReferenceNo
                }).ToList(), "id", "text"); 
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(x => x.AccountName).ToList(), "id", "AccountName");

            return View(payout);
        }

        // GET: Payout/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payout = await _context.Payout.FindAsync(id);
            if (payout == null)
            {
                return NotFound();
            }

            ViewBag.ClientTradingProfile = new SelectList(_context.TxnView.Where(t => t.Type != "D" && t.Status != "C" && t.PayoutDone)
                .Select(t => new
                {
                    id = t.ClientTradingProfileID,
                    text = t.ClientTradingProfileName
                }).Distinct().OrderBy(t => t.text).ToList(), "id", "text");
            ViewBag.Txn = new SelectList(_context.TxnView.Where(t => t.id == payout.TxnID || (t.Type != "D" && t.Status != "C" && t.PayoutDone)).OrderBy(m => m.ReferenceNo)
                .Select(t => new
                {
                    id = t.id,
                    text = t.ClientTradingProfileName + ", " + t.TradeDate.ToString() + ", " + (t.ClientAmountOut.ToString() ?? "") + ", " + (t.ClientCurrencyNameOut ?? "") + ", " + t.ReferenceNo
                }).ToList(), "id", "text"); 
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(x => x.AccountName).ToList(), "id", "AccountName");

            return View(payout);
        }

        // POST: Payout/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ReferenceNo,TxnID,ClientPayoutAmount,ClientPayoutUSDRate,ProviderPayinUSDRate,UsedCurrencyID,UsedAmount,UsedClientPayoutFXRate,UsedUSDRate,AccountBankAccountID,DateTimeModified,DateTimeAdded")] Payout payout)
        {
            if (id != payout.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ReportTxnOriginal reportTxn;
                    Txn txn;

                    var payoutOld = await _context.Payout.AsNoTracking().SingleOrDefaultAsync(m => m.id == id);
                    
                    //update payout
                    payout.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(payout);
                    await _context.SaveChangesAsync();

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //check ClientPayoutMissing, update Status
                    reportTxn = await _context.ReportTxnOriginal.AsNoTracking().SingleOrDefaultAsync(m => m.TxnID == payout.TxnID);
                    txn = await _context.Txn.AsNoTracking().SingleOrDefaultAsync(m => m.id == payout.TxnID);
                    if (txn != null && reportTxn != null)
                    {
                        if (reportTxn.ClientPayoutMissing < reportTxn.ClientAmountOut && reportTxn.ClientPayoutMissing > 0)
                            txn.Status = "P";
                        else if (reportTxn.ClientPayoutMissing <= 0)
                            txn.Status = "C"; 
                        else 
                            txn.Status = ""; 

                        _context.Update(txn);
                        await _context.SaveChangesAsync(); 
                    }

                    //if associated txnID changed, update the original txn
                    if (payoutOld.TxnID != payout.TxnID)
                    {
                        reportTxn = await _context.ReportTxnOriginal.AsNoTracking().SingleOrDefaultAsync(m => m.TxnID == payoutOld.TxnID);
                        txn = await _context.Txn.AsNoTracking().SingleOrDefaultAsync(m => m.id == payoutOld.TxnID);
                        if (txn != null)
                        {
                            if (reportTxn == null || reportTxn.ClientPayoutMissing >= reportTxn.ClientAmountOut)
                                txn.Status = "";
                            else if (reportTxn.ClientPayoutMissing > 0)
                                txn.Status = "P";
                            else
                                txn.Status = "C";

                            _context.Update(txn);
                            await _context.SaveChangesAsync();
                        }
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////// 
                    //update AccountTxn
                    var accountTxn = await _context.AccountTxn.AsNoTracking().SingleOrDefaultAsync(m => m.Type == "P" && m.ReferenceID == id);
                    if (accountTxn != null)
                    {
                        accountTxn.AccountBankAccountID = payout.AccountBankAccountID; 
                        accountTxn.AmountDebit = payout.ClientPayoutAmount; 
                        accountTxn.DateTimeModified = Utility.GetLocalDateTime();
                        _context.Update(accountTxn);
                        await _context.SaveChangesAsync();
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayoutExists(payout.id))
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

            ViewBag.ClientTradingProfile = new SelectList(_context.TxnView.Where(t => t.Type != "D" && t.Status != "C" && t.PayoutDone)
                .Select(t => new
                {
                    id = t.ClientTradingProfileID,
                    text = t.ClientTradingProfileName
                }).Distinct().OrderBy(t => t.text).ToList(), "id", "text");
            ViewBag.Txn = new SelectList(_context.TxnView.Where(t => t.id == payout.TxnID || (t.Type != "D" && t.Status != "C" && t.PayoutDone)).OrderBy(m => m.ReferenceNo)
                .Select(t => new
                {
                    id = t.id,
                    text = t.ClientTradingProfileName + ", " + t.TradeDate.ToString() + ", " + (t.ClientAmountOut.ToString() ?? "") + ", " + (t.ClientCurrencyNameOut ?? "") + ", " + t.ReferenceNo
                }).ToList(), "id", "text"); 
            ViewBag.Currency = new SelectList(_context.Currency.OrderBy(m => m.CurrencyName).ToList(), "id", "CurrencyName");
            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(x => x.AccountName).ToList(), "id", "AccountName");

            return View(payout);
        }

        // GET: Payout/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payoutView = await _context.PayoutView
                .FirstOrDefaultAsync(m => m.id == id);
            if (payoutView == null)
            {
                return NotFound();
            }

            return View(payoutView);
        }

        // POST: Payout/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payout = await _context.Payout.FindAsync(id);
            _context.Payout.Remove(payout);
            /////////////////////////////////////////////////////////////////////////////////////////////////////////// 
            //update AccountTxn
            var accountTxn = await _context.AccountTxn.AsNoTracking().SingleOrDefaultAsync(m => m.Type == "P" && m.ReferenceID == id);
            if (accountTxn != null) _context.AccountTxn.Remove(accountTxn);
            /////////////////////////////////////////////////////////////////////////////////////////////////////////// 
            ///
            await _context.SaveChangesAsync();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            //check ClientPayoutMissing, update Status
            var reportTxn = await _context.ReportTxnOriginal.AsNoTracking().SingleOrDefaultAsync(m => m.TxnID == payout.TxnID);
             
            var txn = await _context.Txn.AsNoTracking().SingleOrDefaultAsync(m => m.id == payout.TxnID);
            if (txn != null)
            { 
                if (reportTxn == null || reportTxn.ClientPayoutMissing >= reportTxn.ClientAmountOut)
                    txn.Status = "";
                else if (reportTxn.ClientPayoutMissing > 0)
                    txn.Status = "P";
                else
                    txn.Status = "C";
                     
                _context.Update(txn);
                await _context.SaveChangesAsync();
            }
              
            return RedirectToAction(nameof(Index));
        }

        private bool PayoutExists(int id)
        {
            return _context.Payout.Any(e => e.id == id);
        }
    }
}
