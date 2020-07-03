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
    public class SlipController : Controller
    {
        private readonly tradingContext _context;

        public SlipController(tradingContext context)
        {
            _context = context;
        }

        // GET: Slip
        public async Task<IActionResult> Index(int providerTradingProfileID, bool isMissingSlipOnly)
        {
            ViewBag.isMissingSlipOnly = isMissingSlipOnly;
            ViewBag.ProviderTradingProfile = new SelectList(_context.SlipView.ToList().GroupBy(x => x.ProviderTradingProfileID).Select(x => x.First()).OrderBy(x=>x.ProviderTradingProfileName), "ProviderTradingProfileID", "ProviderTradingProfileName", providerTradingProfileID);

            var slipView = _context.SlipView.AsQueryable();

            if (isMissingSlipOnly)
                slipView = slipView.Where(x => x.ActualAmount == null || x.ActualAmount == 0);
            if (providerTradingProfileID > 0)
                slipView = slipView.Where(x => x.ProviderTradingProfileID == providerTradingProfileID);

            return View(await slipView.OrderBy(x => x.ProviderTradingProfileName).ToListAsync());
        }

        // GET: Slip/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slip = await _context.SlipView
                .FirstOrDefaultAsync(m => m.id == id);
            if (slip == null)
            {
                return NotFound();
            }

            return View(slip);
        }

        [HttpGet]
        public ActionResult GetTxn(int providerTradingProfileID)//kk
        {
            var txn = _context.Txn.Where(x => x.ProviderTradingProfileID == providerTradingProfileID).OrderBy(x=>x.ReferenceNo).Select(x => new
            {
                id = x.id,
                ReferenceNo = x.ReferenceNo
            }).ToList();

            return Json(txn);
        }

        [HttpGet]
        public ActionResult GetSender(int providerTradingProfileID)
        {
            var providerTradingProfile = _context.ProviderTradingProfile.FirstOrDefault(x => x.id == providerTradingProfileID);
            if (providerTradingProfile == null) return NotFound();

            var sender = _context.Sender.Where(x => x.ProviderID == providerTradingProfile.ProviderID).OrderBy(x=>x.SenderName).Select(x => new
            {
                id = x.id,
                SenderName = x.SenderName
            }).ToList();

            return Json(sender);
        }

        // GET: Slip/Create
        public IActionResult Create()
        { 
            ViewBag.ProviderTradingProfile = new SelectList(_context.TxnView.ToList().Where(x => x.Type != "L" && x.ProviderTradingProfileID != null).GroupBy(x => x.ProviderTradingProfileID).Select(x => x.First()).OrderBy(x => x.ProviderTradingProfileName), "ProviderTradingProfileID", "ProviderTradingProfileName"); 
            //ViewBag.Txn = new SelectList(_context.Txn.ToList(), "id", "ReferenceNo");
            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(x => x.AccountName).ToList(), "id", "AccountName");
            //ViewBag.Sender = new SelectList(_context.Sender.ToList(), "id", "SenderName");

            Slip slip = new Slip();
            slip.ReferenceNo = "S" + Utility.GetLocalDateTime().ToString("yyyyMMddHHmmss");
            slip.SlipDate = Utility.GetLocalDateTime().Date;
            return View(slip);
        }

        // POST: Slip/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ReferenceNo,SlipDate,ProviderTradingProfileID,TxnID,AccountBankAccountID,SenderID,SlipAmount,ActualAmount,DateTimeModified,DateTimeAdded")] Slip slip)
        {
            if (ModelState.IsValid)
            {
                slip.DateTimeModified = Utility.GetLocalDateTime();
                slip.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(slip);
                await _context.SaveChangesAsync();

                /////////////////////////////////////////////////////////////////////////////////////////////////////////// 
                //update AccountTxn
                if (slip.ActualAmount != null && slip.ActualAmount > 0)
                {
                    AccountTxn accountTxn = new AccountTxn();
                    accountTxn.Type = "S";
                    accountTxn.ReferenceID = slip.id;
                    accountTxn.AccountBankAccountID = slip.AccountBankAccountID;
                    accountTxn.AmountCredit = slip.ActualAmount;
                    accountTxn.DateTimeModified = Utility.GetLocalDateTime();
                    accountTxn.DateTimeAdded = Utility.GetLocalDateTime();
                    _context.Add(accountTxn);
                    await _context.SaveChangesAsync();
                }
                /////////////////////////////////////////////////////////////////////////////
                return RedirectToAction(nameof(Index));
            } 
            ViewBag.ProviderTradingProfile = new SelectList(_context.TxnView.ToList().Where(x => x.Type != "L").GroupBy(x => x.ProviderTradingProfileID).Select(x => x.First()).OrderBy(x => x.ProviderTradingProfileName), "ProviderTradingProfileID", "ProviderTradingProfileName");
            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(x => x.AccountName).ToList(), "id", "AccountName");

            return View(slip);
        }

        // GET: Slip/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slip = await _context.Slip.FindAsync(id);
            if (slip == null)
            {
                return NotFound();
            }
             
            ViewBag.ProviderTradingProfile = new SelectList(_context.TxnView.ToList().Where(x => x.Type != "L").GroupBy(x => x.ProviderTradingProfileID).Select(x => x.First()).OrderBy(x => x.ProviderTradingProfileName), "ProviderTradingProfileID", "ProviderTradingProfileName");
            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(x => x.AccountName).ToList(), "id", "AccountName");

            var providerTradingProfile = _context.ProviderTradingProfile.FirstOrDefault(x => x.id == slip.ProviderTradingProfileID);
            ViewBag.Sender = new SelectList(_context.Sender.ToList().Where(x => x.ProviderID == providerTradingProfile.ProviderID).OrderBy(x => x.SenderName), "id", "SenderName");
            ViewBag.Txn = new SelectList(_context.Txn.ToList().Where(x => x.ProviderTradingProfileID == slip.ProviderTradingProfileID).OrderBy(x => x.ReferenceNo), "id", "ReferenceNo");
             
            return View(slip);
        }

        // POST: Slip/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ReferenceNo,SlipDate,ProviderTradingProfileID,TxnID,AccountBankAccountID,SenderID,SlipAmount,ActualAmount,DateTimeModified,DateTimeAdded")] Slip slip)
        {
            if (id != slip.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    slip.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(slip);

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////// 
                    //update AccountTxn
                    var accountTxn = await _context.AccountTxn.AsNoTracking().SingleOrDefaultAsync(m => m.Type == "S" && m.ReferenceID == id);
                    if (accountTxn != null) _context.AccountTxn.Remove(accountTxn);
                     
                    if (slip.ActualAmount != null && slip.ActualAmount > 0)
                    { 
                        accountTxn = new AccountTxn();
                        accountTxn.Type = "S";
                        accountTxn.ReferenceID = id;
                        accountTxn.AccountBankAccountID = slip.AccountBankAccountID;
                        accountTxn.AmountCredit = slip.ActualAmount;
                        accountTxn.DateTimeModified = Utility.GetLocalDateTime();
                        accountTxn.DateTimeAdded = Utility.GetLocalDateTime();
                        _context.Add(accountTxn);
                    } 
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlipExists(slip.id))
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
            ViewBag.ProviderTradingProfile = new SelectList(_context.TxnView.ToList().Where(x => x.Type != "L").GroupBy(x => x.ProviderTradingProfileID).Select(x => x.First()).OrderBy(x => x.ProviderTradingProfileName), "ProviderTradingProfileID", "ProviderTradingProfileName");
            ViewBag.AccountBankAccount = new SelectList(_context.AccountBankAccount.OrderBy(x => x.AccountName).ToList(), "id", "AccountName");

            var providerTradingProfile = _context.ProviderTradingProfile.FirstOrDefault(x => x.id == slip.ProviderTradingProfileID);
            ViewBag.Sender = new SelectList(_context.Sender.ToList().Where(x => x.ProviderID == providerTradingProfile.ProviderID).OrderBy(x => x.SenderName), "id", "SenderName");
            ViewBag.Txn = new SelectList(_context.Txn.ToList().Where(x => x.ProviderTradingProfileID == slip.ProviderTradingProfileID).OrderBy(x => x.ReferenceNo), "id", "ReferenceNo");

            return View(slip);
        }

        // GET: Slip/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slip = await _context.SlipView
                .FirstOrDefaultAsync(m => m.id == id);
            if (slip == null)
            {
                return NotFound();
            }

            return View(slip);
        }

        // POST: Slip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slip = await _context.Slip.FindAsync(id);
            _context.Slip.Remove(slip);
            /////////////////////////////////////////////////////////////////////////////////////////////////////////// 
            //update AccountTxn
            var accountTxn = await _context.AccountTxn.AsNoTracking().SingleOrDefaultAsync(m => m.Type == "S" && m.ReferenceID == id);
            if (accountTxn != null) _context.AccountTxn.Remove(accountTxn);
            /////////////////////////////////////////////////////////////////////////////////////////////////////////// 
            /// 
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SlipExists(int id)
        {
            return _context.Slip.Any(e => e.id == id);
        }
    }
}
