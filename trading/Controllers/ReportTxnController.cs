using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using trading.Class;
using trading.Models;

namespace trading.Controllers
{
    public class ReportTxnController : Controller
    {
        private readonly tradingContext _context;

        public ReportTxnController(tradingContext context)
        {
            _context = context;
        }

        // GET: ReportTxn
        public async Task<IActionResult> Index(int clientTradingProfileID, int providerTradingProfileID, DateTime dateFilterStart, DateTime dateFilterEnd)
        {
            ViewBag.ClientTradingProfile = new SelectList(_context.ClientTradingProfile.ToList()
                      .OrderBy(m => m.ClientTradingProfileName), "id", "ClientTradingProfileName", clientTradingProfileID);

            ViewBag.ProviderTradingProfile = new SelectList(_context.ProviderTradingProfile.ToList()
                      .OrderBy(m => m.ProviderTradingProfileName), "id", "ProviderTradingProfileName", providerTradingProfileID);

            //date range
            if (dateFilterStart == DateTime.MinValue) dateFilterStart = new DateTime(Utility.GetLocalDateTime().Date.Year, Utility.GetLocalDateTime().Date.Month, 1); ;
            if (dateFilterEnd == DateTime.MinValue) dateFilterEnd = Utility.GetLocalDateTime().Date;

            ViewBag.dateFilterStart = dateFilterStart.ToString("yyyy-MM-dd");
            ViewBag.dateFilterEnd = dateFilterEnd.ToString("yyyy-MM-dd");

            var reportTxn =_context.ReportTxn.Where(x => x.TradeDate >= dateFilterStart && x.TradeDate <= dateFilterEnd &&
                            (clientTradingProfileID == 0 || x.ClientTradingProfileID == clientTradingProfileID) &&
                            (providerTradingProfileID == 0 || x.ProviderTradingProfileID == providerTradingProfileID));
            ViewBag.Total = String.Format("{0:0,0.00}", reportTxn.Sum(x => x.GrossProfitUSD));

            return View(await reportTxn
                .OrderBy(x => x.ReferenceNo).ToListAsync());
             
        }
         
        public FileStreamResult GeneratePDF(DateTime dateFilterStart, DateTime dateFilterEnd)
        {
            var reportTxn = _context.ReportTxn.Where(x => x.TradeDate >= dateFilterStart && x.TradeDate <= dateFilterEnd)
                   .OrderBy(x => x.ReferenceNo).ToList();
            
            PdfPTable table = new PdfPTable(7);
            MemoryStream workStream = new MemoryStream();
            Document document = new Document();
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            table.AddCell(new Phrase("Trade Date"));
            table.AddCell(new Phrase("Client Trading Profile"));
            table.AddCell(new Phrase("Gross Profit USD"));
            table.AddCell(new Phrase("Gross Profit USD (%)"));
            table.AddCell(new Phrase("Client Payout Currency"));
            table.AddCell(new Phrase("Client Payout Amount"));
            table.AddCell(new Phrase("Reference No."));

            foreach (var rpt in reportTxn)
            {
                table.AddCell(new Phrase(rpt.TradeDate.ToString("yyyy-MM-dd")));
                table.AddCell(new Phrase(rpt.ClientTradingProfileName.ToString()));
                table.AddCell(new Phrase(rpt.GrossProfitUSD.ToString()));
                table.AddCell(new Phrase(rpt.GrossProfitUSDPct.ToString()));
                table.AddCell(new Phrase(rpt.ClientCurrencyNameOut.ToString()));
                table.AddCell(new Phrase(rpt.ClientAmountOut.ToString()));
                table.AddCell(new Phrase(rpt.ReferenceNo.ToString()));
            }
            document.SetPageSize(PageSize.A4.Rotate()); 
            document.Open();   
            document.Add(table); 
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }

        // GET: ReportTxn/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTxn = await _context.ReportTxn
                .FirstOrDefaultAsync(m => m.TxnID == id);
            if (reportTxn == null)
            {
                return NotFound();
            }

            return View(reportTxn);
        }

        // GET: ReportTxn/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReportTxn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TxnID,ProviderPayinUSD,ClientPayoutUSD,IntroducerCommissionUSD,ReferenceNo,TradeDate,ClientTradingProfileName,ClientCurrencyNameOut,ClientAmountOut,GrossProfitUSD,GrossProfitUSDPct")] ReportTxn reportTxn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportTxn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportTxn);
        }

        // GET: ReportTxn/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTxn = await _context.ReportTxn.FindAsync(id);
            if (reportTxn == null)
            {
                return NotFound();
            }
            return View(reportTxn);
        }

        // POST: ReportTxn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TxnID,ProviderPayinUSD,ClientPayoutUSD,IntroducerCommissionUSD,ReferenceNo,TradeDate,ClientTradingProfileName,ClientCurrencyNameOut,ClientAmountOut,GrossProfitUSD,GrossProfitUSDPct")] ReportTxn reportTxn)
        {
            if (id != reportTxn.TxnID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportTxn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportTxnExists(reportTxn.TxnID))
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
            return View(reportTxn);
        }

        // GET: ReportTxn/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTxn = await _context.ReportTxn
                .FirstOrDefaultAsync(m => m.TxnID == id);
            if (reportTxn == null)
            {
                return NotFound();
            }

            return View(reportTxn);
        }

        // POST: ReportTxn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportTxn = await _context.ReportTxn.FindAsync(id);
            _context.ReportTxn.Remove(reportTxn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportTxnExists(int id)
        {
            return _context.ReportTxn.Any(e => e.TxnID == id);
        }
    }
}
