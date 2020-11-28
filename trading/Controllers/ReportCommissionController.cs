using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using trading.Models;

namespace trading.Controllers
{
    public class ReportCommissionController : Controller
    {
        private readonly tradingContext _context;
        IHostingEnvironment _env;
        public ReportCommissionController(tradingContext context, IHostingEnvironment env)
        {
            _context = context; 
            _env = env;
        }
         
        public IActionResult Export()
        {
            int m = DateTime.Today.Month;
            var month = new Dictionary<int, string>
                            {{1, "JAN"},
                             {2, "FEB"},
                             {3, "MAR"},
                             {4, "APR"},
                             {5, "MAY"},
                             {6, "JUN"},
                             {7, "JUL"},
                             {8, "AUG"},
                             {9, "SEP"},
                             {10, "OCT"},
                             {11, "NOV"},
                             {12, "DEC"}};

            ViewBag.Month = new SelectList(month, "Key", "Value", m);  
            ViewBag.Introducer = new SelectList(_context.Introducer.OrderBy(x => x.IntroducerName).ToList(), "id", "IntroducerName");
              
            if (HttpContext.Session.GetString("Role") == "S")
                return View(); 

            return RedirectToAction("Logout","Home");
        }

        [HttpPost]
        public ActionResult Export(int Month, int IntroducerID, string Export)
        {
            if (IntroducerID == 0) return new EmptyResult();

            string month = new DateTime(DateTime.Today.Year, Month, 1).ToString("MMM", CultureInfo.InvariantCulture);
            var introducer = _context.Introducer.FirstOrDefault(m => m.id == IntroducerID);
            string introducerName = introducer.IntroducerName;
            string introducerLegalName = introducer.IntroducerLegalName;
            string wsName = introducerName.Length > 31 ? introducerName.Substring(0, 31) : introducerName;
            //decimal? total = 0;

            DataTable dt2 = new DataTable(wsName);
            dt2.Columns.AddRange(new DataColumn[9] { new DataColumn("Trade Ref"),
                                                    new DataColumn("Date"),
                                                    new DataColumn("Client"),
                                                    new DataColumn("Sell Cur"),
                                                    new DataColumn("Sell Amount"),
                                                    new DataColumn("Purchase Cur"),
                                                    new DataColumn("Purchase Amount"),
                                                    new DataColumn("Rate"),
                                                    new DataColumn("USD Equivalent")});

            var reportCommission2 = from rpt2 in _context.ReportCommission2
                                    where rpt2.TradeDate.Month == Month && rpt2.IntroducerID == IntroducerID
                                    select rpt2;
            var total2 = reportCommission2.Sum(m => m.IntroducerCommissionUSD);

            foreach (var row in reportCommission2)
            {
                dt2.Rows.Add(row.ReferenceNo, row.TradeDate.ToShortDateString(), row.ClientTradingProfileName, row.ClientCurrencyNameIn, String.Format("{0:n}", row.ClientAmountIn), row.ClientCurrencyNameOut, String.Format("{0:n}", row.ClientAmountOut), row.IntroducerCommissionRate + "%", row.IntroducerCommissionUSD == null ? "INCOMPLETE" : String.Format("{0:n}", row.IntroducerCommissionUSD));
            }
            dt2.Rows.Add("", "", "", "", "", "", "", "Total 2", String.Format("{0:n}", total2));//String.Format("{0:n}", total2)

            int dt2RowCount = dt2.Rows.Count;
            

            /////////////////////////////////////////////////////////////////////////////////
            //

            DataTable dt1 = new DataTable(wsName);
            dt1.Columns.AddRange(new DataColumn[9] { new DataColumn("Trade Ref"),
                                                    new DataColumn("Date"),
                                                    new DataColumn("Client"),
                                                    new DataColumn("Sell Cur"),
                                                    new DataColumn("Sell Amount"),
                                                    new DataColumn("Purchase Cur"),
                                                    new DataColumn("Purchase Amount"),
                                                    new DataColumn("Rate"),
                                                    new DataColumn("USD Equivalent")});

            var reportCommission1 = from rpt1 in _context.ReportCommission1
                                    where rpt1.TradeDate.Month == Month && rpt1.IntroducerID == IntroducerID
                                    select rpt1;
            var total1 = reportCommission1.Sum(m => m.IntroducerCommissionUSD);

            foreach (var row in reportCommission1)
            {
                dt1.Rows.Add(row.ReferenceNo, row.TradeDate.ToShortDateString(), row.ClientTradingProfileName, row.ClientCurrencyNameIn, String.Format("{0:n}", row.ClientAmountIn), row.ClientCurrencyNameOut, String.Format("{0:n}", row.ClientAmountOut), row.IntroducerCommissionRate + "%", row.IntroducerCommissionUSD == null ? "INCOMPLETE" : String.Format("{0:n}", row.IntroducerCommissionUSD));
            }
            dt1.Rows.Add("", "", "", "", "", "", "", "Total 1", String.Format("{0:n}", total1));

            int dt1RowCount = dt1.Rows.Count;
            /////////////////////////////////////////////////////////////////////////////////
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt1);
                ws.Row(1).InsertRowsAbove(14);
                ws.Column(1).InsertColumnsBefore(1);
                ws.SetShowGridLines(false);
                ws.Column(1).Width = 30;
                ws.Column(2).AdjustToContents();
                ws.Column(6).AdjustToContents();
                ws.Column(8).AdjustToContents();
                ws.Column(9).AdjustToContents();
                ws.Column(10).AdjustToContents();

                //address
                ws.Cell(9, 1).Value = "Please issue invoice to";
                ws.Cell(10, 1).Value = "Atlantic Partners Asia Holdings Limited";
                ws.Cell(11, 1).Value = "20/F CMA Connaught Road";
                ws.Cell(12, 1).Value = "Sheung Wan Hong Kong";

                //header
                var header = ws.Range(ws.Cell(4, 4).Address, ws.Cell(6, 4).Address);
                header.Style.Font.FontSize = 22;
                header.Style.Font.FontColor = XLColor.Blue;
                ws.Cell(4, 4).Value = "Commission Report";
                ws.Cell(5, 4).Value = introducerLegalName;
                ws.Cell(6, 4).Value = "'" + month + "-" + DateTime.Today.Year.ToString();

                //tables
                ws.Cell(13, 4).Value = "Type 1: Commissions Based on Trade Gross Profit";
                ws.Cell(dt1RowCount + 17, 4).Value = "Type 2: Commissions Based % of Amounts Traded";
                ws.Cell(dt1RowCount + 19, 2).InsertTable(dt2);
                ws.Cell(dt1RowCount + dt2RowCount + 20, 9).Value = "Grand Total";
                ws.Cell(dt1RowCount + dt2RowCount + 20, 10).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(dt1RowCount + dt2RowCount + 20, 10).DataType = XLDataType.Number;
                ws.Cell(dt1RowCount + dt2RowCount + 20, 10).Value = total1 + total2;

                //ws.Column(6).CellsUsed().SetDataType(XLDataType.Number);

                //ws.Range(16, 6, 16 + dt1RowCount - 1, 6).Style.NumberFormat.Format = "0.00";
                //ws.Range(16,6,16+ dt1RowCount-1,6).DataType = XLDataType.Number;
                //ws.Range(20 + dt1RowCount, 6, 20 + dt1RowCount + dt2RowCount - 2, 6).Style.NumberFormat.Format = "0.00";
                //ws.Range(20+ dt1RowCount, 6, 20 + dt1RowCount+ dt2RowCount-2,6).DataType = XLDataType.Number;


                //logo
                var logoCommPath = _env.WebRootFileProvider.GetFileInfo("image/logoComm.png").PhysicalPath;
                var logoComm = ws.AddPicture(logoCommPath);
                logoComm.Placement = ClosedXML.Excel.Drawings.XLPicturePlacement.FreeFloating;
                 
                using (MemoryStream stream = new MemoryStream())
                { 
                    wb.SaveAs(stream);
                    if (Export == "EXCEL")
                    {
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", introducerName + "_" + month + ".xlsx");
                    }
                    else
                    {
                        Workbook book = new Workbook();
                        book.LoadFromStream(stream);
                        MemoryStream streamPDF = new MemoryStream();
                        var sheet = book.Worksheets[0];
                        sheet.PageSetup.Orientation = PageOrientationType.Landscape;
                        sheet.PageSetup.FitToPagesWide = 1;
                        sheet.PageSetup.FitToPagesTall = 0;
                        book.SaveToStream(streamPDF, FileFormat.PDF);

                        return File(streamPDF.ToArray(), "application/pdf", introducerName + "_" + month + ".pdf");
                    }
                }
            }
        }

        //[HttpPost]
        //public FileResult Export(int Month, int IntroducerID, string Export)
        //{
        //    if (IntroducerID == 0) return null;
        //    string month = new DateTime(DateTime.Today.Year, Month, 1).ToString("MMM", CultureInfo.InvariantCulture);
        //    var introducer = _context.Introducer.FirstOrDefault(m => m.id == IntroducerID);
        //    string introducerName = introducer.IntroducerName;
        //    using (XLWorkbook wb = GetWorkbook(Month, IntroducerID))
        //    {
        //        //wb = GetWorkbook(Month, IntroducerID);
        //        MemoryStream stream = new MemoryStream();
        //        //using (MemoryStream stream = new MemoryStream())


        //        if (Export == "EXCEL")
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", introducerName + "_" + month + ".xlsx");
        //        }
        //        else
        //        {
        //            wb.SaveAs(stream);
        //            Workbook book = new Workbook();
        //            book.LoadFromStream(stream);
        //            MemoryStream streamPDF = new MemoryStream();
        //            var sheet = book.Worksheets[0];
        //            sheet.PageSetup.Orientation = PageOrientationType.Landscape;
        //            sheet.PageSetup.FitToPagesWide = 1;
        //            sheet.PageSetup.FitToPagesTall = 0;
        //            book.SaveToStream(streamPDF, FileFormat.PDF);

        //            return File(streamPDF.ToArray(), "application/pdf", introducerName + "_" + month + ".pdf");
        //        }
        //    }
        //}

        //[HttpPost]
        //public FileResult Export(int Month, int IntroducerID, string Export)
        //{ 
        //    if (IntroducerID == 0) return null;

        //    string month = new DateTime(DateTime.Today.Year, Month, 1).ToString("MMM", CultureInfo.InvariantCulture);
        //    var introducer = _context.Introducer.FirstOrDefault(m => m.id == IntroducerID);
        //    string introducerName = introducer.IntroducerName;
        //    string introducerLegalName = introducer.IntroducerLegalName;
        //    //decimal? total = 0;

        //    DataTable dt2 = new DataTable(introducerName);
        //    dt2.Columns.AddRange(new DataColumn[9] { new DataColumn("Trade Ref"),
        //                                            new DataColumn("Date"),
        //                                            new DataColumn("Client"),
        //                                            new DataColumn("Sell Cur"),
        //                                            new DataColumn("Sell Amount"),
        //                                            new DataColumn("Purchase Cur"),
        //                                            new DataColumn("Purchase Amount"),
        //                                            new DataColumn("Rate"),
        //                                            new DataColumn("USD Equivalent")});

        //    var reportCommission2 = from rpt2 in _context.ReportCommission2
        //                            where rpt2.TradeDate.Month == Month && rpt2.IntroducerID == IntroducerID
        //                            select rpt2;
        //    var total2 = reportCommission2.Sum(m => m.IntroducerCommissionUSD);

        //    foreach (var row in reportCommission2)
        //    {
        //        dt2.Rows.Add(row.ReferenceNo, row.TradeDate.ToShortDateString(), row.ClientTradingProfileName, row.ClientCurrencyNameIn, String.Format("{0:n}", row.ClientAmountIn) , row.ClientCurrencyNameOut, String.Format("{0:n}", row.ClientAmountOut), row.IntroducerCommissionRate+"%", row.IntroducerCommissionUSD==null ? "INCOMPLETE" : String.Format("{0:n}", row.IntroducerCommissionUSD));
        //    }
        //    dt2.Rows.Add("", "", "", "", "", "", "","Total 1", String.Format("{0:n}", total2));

        //    int dt2RowCount = dt2.Rows.Count;
        //    /////////////////////////////////////////////////////////////////////////////////
        //    //

        //    DataTable dt1 = new DataTable(introducerName);
        //    dt1.Columns.AddRange(new DataColumn[9] { new DataColumn("Trade Ref"),
        //                                            new DataColumn("Date"),
        //                                            new DataColumn("Client"),
        //                                            new DataColumn("Sell Cur"),
        //                                            new DataColumn("Sell Amount"),
        //                                            new DataColumn("Purchase Cur"),
        //                                            new DataColumn("Purchase Amount"),
        //                                            new DataColumn("Rate"),
        //                                            new DataColumn("USD Equivalent")});

        //    var reportCommission1 = from rpt1 in _context.ReportCommission1
        //                            where rpt1.TradeDate.Month == Month && rpt1.IntroducerID == IntroducerID
        //                            select rpt1;
        //    var total1 = reportCommission1.Sum(m => m.IntroducerCommissionUSD);

        //    foreach (var row in reportCommission1)
        //    {
        //        dt1.Rows.Add(row.ReferenceNo, row.TradeDate.ToShortDateString(), row.ClientTradingProfileName, row.ClientCurrencyNameIn, String.Format("{0:n}", row.ClientAmountIn), row.ClientCurrencyNameOut, String.Format("{0:n}", row.ClientAmountOut),  row.IntroducerCommissionRate  + "%", row.IntroducerCommissionUSD == null ? "INCOMPLETE" : String.Format("{0:n}", row.IntroducerCommissionUSD));
        //    }
        //    dt1.Rows.Add("", "", "", "", "", "", "", "Total 2", String.Format("{0:n}", total1));

        //    int dt1RowCount = dt1.Rows.Count;
        //    /////////////////////////////////////////////////////////////////////////////////
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        var ws = wb.Worksheets.Add(dt2);
        //        ws.Row(1).InsertRowsAbove(14);
        //        ws.Column(1).InsertColumnsBefore(1);
        //        ws.SetShowGridLines(false);
        //        ws.Column(1).Width = 30;
        //        ws.Column(2).AdjustToContents();
        //        ws.Column(6).AdjustToContents();
        //        ws.Column(8).AdjustToContents();
        //        ws.Column(9).AdjustToContents();
        //        ws.Column(10).AdjustToContents();

        //        //address
        //        ws.Cell(9, 1).Value = "Please issue invoice to";
        //        ws.Cell(10, 1).Value = "Atlantic Partners Asia Holdings Limited";
        //        ws.Cell(11, 1).Value = "20/F CMA Connaught Road";
        //        ws.Cell(12, 1).Value = "Sheung Wan Hong Kong";

        //        //header
        //        var header = ws.Range(ws.Cell(4, 4).Address, ws.Cell(6, 4).Address); 
        //        header.Style.Font.FontSize = 22;
        //        header.Style.Font.FontColor = XLColor.Blue;
        //        ws.Cell(4, 4).Value = "Commission Report";
        //        ws.Cell(5, 4).Value = introducerLegalName;
        //        ws.Cell(6, 4).Value = "'"+month + "-" + DateTime.Today.Year.ToString();

        //        //tables
        //        ws.Cell(13, 4).Value = "Type 2: Commissions Based % of Amounts Traded";
        //        ws.Cell(dt2RowCount+17, 4).Value = "Type 1: Commissions Based on Trade Gross Profit";
        //        ws.Cell(dt2RowCount+19, 2).InsertTable(dt1);
        //        ws.Cell(dt2RowCount + dt1RowCount + 20, 9).Value = "Grand Total";
        //        ws.Cell(dt2RowCount + dt1RowCount + 20, 10).Value = String.Format("{0:n}", total2 + total1);

        //        //logo
        //        var logoCommPath = _env.WebRootFileProvider.GetFileInfo("image/logoComm.png").PhysicalPath;
        //        var logoComm = ws.AddPicture(logoCommPath);
        //        logoComm.Placement = ClosedXML.Excel.Drawings.XLPicturePlacement.FreeFloating;

        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            //wb.SaveAs(stream);
        //            //return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", introducerName + "_" + month + ".xlsx");


        //            wb.SaveAs(stream);
        //            Workbook book = new Workbook();
        //            book.LoadFromStream(stream);
        //            MemoryStream stream1 = new MemoryStream();
        //            //book.PrintDocument.PrinterSettings.DefaultPageSettings.Landscape = true;
        //            //book.PrintDocument.Print();
        //            var sheet = book.Worksheets[0];
        //            sheet.PageSetup.Orientation = PageOrientationType.Landscape;
        //            sheet.PageSetup.FitToPagesWide = 1;
        //            sheet.PageSetup.FitToPagesTall = 0;
        //            book.SaveToStream(stream1, FileFormat.PDF);

        //            return File(stream1.ToArray(), "application/pdf", introducerName + "_" + month + ".pdf");


        //        }
        //    }
        //}
    }
}
