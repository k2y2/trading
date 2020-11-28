using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks; 
using HtmlAgilityPack; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using trading.Class;
using trading.Models;
 
namespace trading.Controllers
{
    public class DfrController : Controller
    {
        private readonly tradingContext _context;

        public DfrController(tradingContext context)
        {
            _context = context;
        }

        // GET: Dfr
        public async Task<IActionResult> Index(DateTime dateFilter)
        {
            if (dateFilter == DateTime.MinValue) dateFilter = Utility.GetLocalDateTime().Date;
            ViewBag.dateFilter = dateFilter.ToString("yyyy-MM-dd");
            //var dfr = from d in _context.Dfr
            //          where d.TradeDate == dateFilter
            //               select d;

            //IEnumerable<DfrView> model = null;

            //var dfrView =  (from d in _context.Dfr
            //         join c in _context.CurrencyPair on d.CurrencyPairID equals c.id
            //         where d.TradeDate == dateFilter

            //         select new DfrView
            //         { 
            //             id = d.id,
            //             TradeDate = d.TradeDate,
            //             CurrencyPairID = d.CurrencyPairID,
            //             Rate = d.Rate,
            //             DateTimeModified = d.DateTimeModified,
            //             DateTimeAdded = d.DateTimeAdded,
            //             CurrencyPairName = c.CurrencyPairName
            //         }
            //    )  ;
            //dfr = dfr.Where(d => d.TradeDate == dateFilter);

            //return View(await dfrView.ToListAsync());
            //return View(await dfr.ToListAsync());
            return View(await _context.DfrView.Where(m => m.TradeDate == dateFilter).OrderBy(m => m.CurrencyPairName).ToListAsync());
        }

        // GET: Dfr/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dfrView = await _context.DfrView
                .FirstOrDefaultAsync(m => m.id == id);

            //var dfrView = await (from d in _context.Dfr
            //                   join c in _context.CurrencyPair on d.CurrencyPairID equals c.id
            //                   where d.id == id

            //                   select new DfrView
            //                   {
            //                       id = d.id,
            //                       TradeDate = d.TradeDate,
            //                       CurrencyPairID = d.CurrencyPairID,
            //                       Rate = d.Rate,
            //                       DateTimeModified = d.DateTimeModified,
            //                       DateTimeAdded = d.DateTimeAdded,
            //                       CurrencyPairName = c.CurrencyPairName
            //                   }
            //    ).FirstOrDefaultAsync();

            if (dfrView == null)
            {
                return NotFound();
            }

            return View(dfrView);
        }

        private string getWebResponse(string url)
        {
            WebClient client = new WebClient();  
            string htmlCode = "";

            while (true)
            {
                try
                {
                    htmlCode = client.DownloadString(url);
                    if (htmlCode != null) break;
                }
                catch (WebException e)
                {
                    //Console.WriteLine(e.Message);
                    if (e.Message.Contains("400")) break;
                    Thread.Sleep(1000);
                }
            }
              
            return htmlCode;
        }

        private HtmlNodeCollection getNodes(string url, string xpath)
        {
            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential("1979862209538", "n8n2rkai6pb1q6r5gd2qs472pg");//prod
            //client.Credentials = new NetworkCredential("weq702735343", "fcbrm3lv59atklcukqlh7r21un");//test

            string htmlCode =""; 

            while (true)
            {
                try
                {
                    htmlCode = client.DownloadString(url);
                    if (htmlCode != null) break;
                }
                catch (WebException e)
                {
                    //Console.WriteLine(e.Message);
                    if (e.Message.Contains("400")) break;
                    Thread.Sleep(1000);
                }
            }
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlCode);
            return doc.DocumentNode.SelectNodes(xpath);
        }

        [HttpPost]
        public ActionResult UpdateDfr()
        { 
            string url; 
            HtmlNodeCollection colNode; 
            string resp;
            bool parseResult;
            decimal rate;
            var currencyPair = _context.CurrencyPairView;

            DateTime today = Utility.GetLocalDateTime().Date;

            var dfrList = _context.Dfr.Where(m => m.TradeDate == today);
            _context.RemoveRange(dfrList); 

            foreach (var cp in currencyPair)
            { 
                if (cp.CurrencyName1=="ETH" || cp.CurrencyName1 == "UST" || cp.CurrencyName2 == "ETH" || cp.CurrencyName2 == "UST")
                {
                    string currFrom = cp.CurrencyName1 == "UST" ? "USDT" : cp.CurrencyName1;
                    string currTo = cp.CurrencyName2 == "UST" ? "USDT" : cp.CurrencyName2;

                    url = "https://min-api.cryptocompare.com/data/price?fsym="+currFrom+"&tsyms=" + currTo;
                    resp = getWebResponse(url);
                    var result = Regex.Match(resp, @"\d+(\.\d+)?").Value;

                    parseResult = decimal.TryParse(result, out rate);

                    if (parseResult)
                    {
                        Dfr dfr = new Dfr();
                        dfr.TradeDate = Utility.GetLocalDateTime().Date;
                        dfr.CurrencyPairID = cp.id;
                        dfr.Rate = rate;
                        dfr.DateTimeAdded = Utility.GetLocalDateTime();
                        dfr.DateTimeModified = Utility.GetLocalDateTime();
                        _context.Add(dfr);
                    }
                }
                else
                { 
                    url = "https://xecdapi.xe.com/v1/convert_from.xml/?from=" + cp.CurrencyName1 + "&to=" + cp.CurrencyName2;

                    colNode = getNodes(url, "//to/rate/mid");
                    if (colNode != null)
                    {
                        string s = colNode[0].InnerText;
                        parseResult = decimal.TryParse(s, NumberStyles.Float, CultureInfo.CreateSpecificCulture("en-US"), out rate);

                        if (parseResult)
                        {
                            Dfr dfr = new Dfr();
                            dfr.TradeDate = Utility.GetLocalDateTime().Date;
                            dfr.CurrencyPairID = cp.id;
                            dfr.Rate = rate;
                            dfr.DateTimeAdded = Utility.GetLocalDateTime();
                            dfr.DateTimeModified = Utility.GetLocalDateTime();
                            _context.Add(dfr);
                        }
                    }
                }
            }
            _context.SaveChanges();
            //var txn = _context.TxnCompleteView.Where(x => x.id == txnID).FirstOrDefault();
            return Json(null);
        }

        [HttpPost]
        public ActionResult UpdateDfr2()
        { 
            //ChromeOptions options = new ChromeOptions();
            //options.AddArgument("headless");

            //Close the Chrome Driver console
            //ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
            //driverService.HideCommandPromptWindow = true;

            //ChromeDriver driver = new ChromeDriver(driverService, options);
            //driver.Navigate().GoToUrl("https://www.xe.com/currencyconverter/convert/?Amount=1&From=USD&To=VND");
            //string html = driver.PageSource;

            //WebBrowser wb;
            //HtmlWeb w = new HtmlWeb();
            //HtmlAgilityPack.HtmlDocument doc = w.Load("https://www.xe.com/currencyconverter/convert/?Amount=1&From=USD&To=VND");
            string url;//"https://www.x-rates.com/calculator/?from=USD&to=hkd"
            HtmlNodeCollection colNode;
            decimal rate;
            var currencyPair = _context.CurrencyPairView;

            DateTime today = Utility.GetLocalDateTime().Date;
            var dfrList = _context.Dfr.Where(m => m.TradeDate == today);
            _context.RemoveRange(dfrList);

            foreach (var cp in currencyPair)
            { 
                url = "https://www.x-rates.com/calculator/?from=" + cp.CurrencyName1 + "&to=" + cp.CurrencyName2;
                colNode = getNodes(url, "//span[@class='ccOutputRslt']");

                string[] s = colNode[0].InnerText.Split(" ");
                bool parseResult = decimal.TryParse(s[0], out rate);

                if (parseResult)
                {
                    Dfr dfr = new Dfr(); 
                    dfr.TradeDate = Utility.GetLocalDateTime().Date;
                    dfr.CurrencyPairID = cp.id;
                    dfr.Rate = rate;
                    dfr.DateTimeAdded = Utility.GetLocalDateTime();
                    dfr.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Add(dfr); 
                }
            }
            _context.SaveChanges();
            //var txn = _context.TxnCompleteView.Where(x => x.id == txnID).FirstOrDefault();
            return Json(null);
        }

        [HttpGet]
        public JsonResult IsExist(short CurrencyPairID, int id, DateTime TradeDate)
        {
            return Json(!_context.Dfr.Any(x => x.CurrencyPairID == CurrencyPairID && x.TradeDate == TradeDate && x.id != id));
        }
        public IActionResult Update()
        {
            return View();
        }
        
        // GET: Dfr/Create
        public IActionResult Create()
        {
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(x => x.CurrencyPairName).ToList(), "id", "CurrencyPairName");
            //ViewData["DefaultDate"] = Utility.GetLocalDateTime().Date.ToString("yyyy-MM-dd");

            Dfr dfr = new Dfr();
            dfr.TradeDate = Utility.GetLocalDateTime().Date;

            return View("Create", dfr);
        }

        // POST: Dfr/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,TradeDate,CurrencyPairID,Rate,DateTimeModified,DateTimeAdded")] Dfr dfr)
        {
            if (ModelState.IsValid)
            {
                dfr.DateTimeModified = Utility.GetLocalDateTime();
                dfr.DateTimeAdded = Utility.GetLocalDateTime();
                _context.Add(dfr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(x => x.CurrencyPairName).ToList(), "id", "CurrencyPairName");
            return View(dfr);
        }

        // GET: Dfr/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dfr = await _context.Dfr.FindAsync(id);
            if (dfr == null)
            {
                return NotFound();
            }
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(x => x.CurrencyPairName).ToList(), "id", "CurrencyPairName");
            return View(dfr);
        }

        // POST: Dfr/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,TradeDate,CurrencyPairID,Rate,DateTimeModified,DateTimeAdded")] Dfr dfr)
        {
            if (id != dfr.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dfr.DateTimeModified = Utility.GetLocalDateTime();
                    _context.Update(dfr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DfrExists(dfr.id))
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
            ViewBag.CurrencyPair = new SelectList(_context.CurrencyPair.OrderBy(x => x.CurrencyPairName).ToList(), "id", "CurrencyPairName");
            return View(dfr);
        }

        // GET: Dfr/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dfrView = await _context.DfrView
                .FirstOrDefaultAsync(m => m.id == id);

            //var dfrView = await (from d in _context.Dfr
            //                     join c in _context.CurrencyPair on d.CurrencyPairID equals c.id
            //                     where d.id == id

            //                     select new DfrView
            //                     {
            //                         id = d.id,
            //                         TradeDate = d.TradeDate,
            //                         CurrencyPairID = d.CurrencyPairID,
            //                         Rate = d.Rate,
            //                         DateTimeModified = d.DateTimeModified,
            //                         DateTimeAdded = d.DateTimeAdded,
            //                         CurrencyPairName = c.CurrencyPairName
            //                     }
            //    ).FirstOrDefaultAsync();

            if (dfrView == null)
            {
                return NotFound();
            }

            return View(dfrView);
        }

        // POST: Dfr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dfr = await _context.Dfr.FindAsync(id);
            _context.Dfr.Remove(dfr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DfrExists(int id)
        {
            return _context.Dfr.Any(e => e.id == id);
        }
    }
}
