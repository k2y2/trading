#pragma checksum "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "522b1781901fea103a1f77ddea680bcba955d9c6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ReportAccountPayable_Index), @"mvc.1.0.view", @"/Views/ReportAccountPayable/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\k2y2\source\repos\trading\trading\Views\_ViewImports.cshtml"
using trading;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\k2y2\source\repos\trading\trading\Views\_ViewImports.cshtml"
using trading.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"522b1781901fea103a1f77ddea680bcba955d9c6", @"/Views/ReportAccountPayable/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"72c63bdcc17537a09b4a8a21681ecc397d2c23fd", @"/Views/_ViewImports.cshtml")]
    public class Views_ReportAccountPayable_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<trading.Models.ReportAccountPayable>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<script src=""https://code.jquery.com/jquery-3.5.0.js"" integrity=""sha256-r/AaFHrszJtwpe+tHyNi/XCfMxYpbsRg2Uqn0x3s2zc="" crossorigin=""anonymous""></script>

<script type=""text/javascript"">

    $(document).ready(function () {
        $.fn.dataTable.moment('M/D/YYYY');
        var table = $('#tblAccountPayable').DataTable({
            ""paging"": false,
            ""ordering"": true,
            ""info"": false,
            ""searching"": false
        });

    });
</script>

<h1>Account Payable</h1>

");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "522b1781901fea103a1f77ddea680bcba955d9c64699", async() => {
                WriteLiteral("\r\n    <div class=\"form-actions no-color\">\r\n        <p>\r\n            <table class=\"table-borderless\">\r\n                <tr>\r\n                    <td>Client Trading Profile:</td>\r\n                    <td>");
#nullable restore
#line 32 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
                   Write(Html.DropDownList("clientTradingProfileID", ViewBag.ClientTradingProfile as SelectList, "", new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                    <td></td>\r\n                </tr>\r\n                <tr>\r\n                    <td>Currency Out:</td>\r\n                    <td>");
#nullable restore
#line 37 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
                   Write(Html.DropDownList("currencyID", ViewBag.Currency as SelectList, "", new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                    <td></td>\r\n                </tr>\r\n                <tr>\r\n                    <td><input type=\"date\" id=\"dateFilterStart\" name=\"dateFilterStart\"");
                BeginWriteAttribute("value", " value=\"", 1449, "\"", 1481, 1);
#nullable restore
#line 41 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
WriteAttributeValue("", 1457, ViewBag.dateFilterStart, 1457, 24, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" /></td>\r\n                    <td><input type=\"date\" id=\"dateFilterEnd\" name=\"dateFilterEnd\"");
                BeginWriteAttribute("value", " value=\"", 1574, "\"", 1604, 1);
#nullable restore
#line 42 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
WriteAttributeValue("", 1582, ViewBag.dateFilterEnd, 1582, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" /></td>\r\n                    <td><input type=\"submit\" value=\"Search\" class=\"btn btn-default\" /></td>\r\n                </tr>\r\n            </table>\r\n        </p>\r\n    </div>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n<table id=\"tblAccountPayable\" class=\"table table-striped table-bordered table-sticky\" data-sticky-top=\"thead tr:first-child\" data-sticky-left=\"tr td:first-child, tr th:first-child\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 54 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ClientTradingProfileName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 57 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.TradeDateSince));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 60 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.TradeDateUp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 63 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.BalanceIn));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 66 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ClientCurrencyNameIn));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 69 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.BalanceInUSD));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 72 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ClientAmountUncompleted));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 75 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ClientCurrencyNameOut));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 78 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ClientAmountUncompletedUSD));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 81 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Rate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 84 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ActiveDays));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 87 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ActiveDaysWeekend));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 90 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.DaysWeekend));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 95 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 99 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.ClientTradingProfileName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 102 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.TradeDateSince));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 105 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.TradeDateUp));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 108 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.BalanceIn));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 111 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.ClientCurrencyNameIn));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 114 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.BalanceInUSD));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 117 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.ClientAmountUncompleted));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 120 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.ClientCurrencyNameOut));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 123 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.ClientAmountUncompletedUSD));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 126 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.Rate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 129 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.ActiveDays));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 132 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.ActiveDaysWeekend));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 135 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.DaysWeekend));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 138 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n    <tfoot>\r\n        <tr><td></td><td></td><td>Total</td><td></td><td></td><td>");
#nullable restore
#line 141 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
                                                             Write(ViewBag.TotalInUSD);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td><td>");
#nullable restore
#line 141 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
                                                                                         Write(ViewBag.TotalOut);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td><td></td><td><label class=\"control-label\">");
#nullable restore
#line 141 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportAccountPayable\Index.cshtml"
                                                                                                                                                         Write(ViewBag.TotalOutUSD);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label></td><td></td><td></td><td></td><td></td></tr>\r\n    </tfoot>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<trading.Models.ReportAccountPayable>> Html { get; private set; }
    }
}
#pragma warning restore 1591
