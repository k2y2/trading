#pragma checksum "C:\Users\k2y2\source\repos\trading\trading\Views\ReportProvider\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "39195e8ad27d8719db8d907af1092422e02bb1c0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ReportProvider_Index), @"mvc.1.0.view", @"/Views/ReportProvider/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"39195e8ad27d8719db8d907af1092422e02bb1c0", @"/Views/ReportProvider/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"72c63bdcc17537a09b4a8a21681ecc397d2c23fd", @"/Views/_ViewImports.cshtml")]
    public class Views_ReportProvider_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<trading.Models.ReportProvider>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportProvider\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Provider Slip</h1>\r\n \r\n<table class=\"table table-striped table-bordered\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 14 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportProvider\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ProviderTradingProfileName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 17 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportProvider\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.MissingSlipAmount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 20 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportProvider\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.TotalProviderExpectedPayInAmount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 23 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportProvider\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.TotalSlipAmount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 26 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportProvider\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.TotalActualAmount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 31 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportProvider\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 34 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportProvider\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.ProviderTradingProfileName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 37 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportProvider\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.MissingSlipAmount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 40 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportProvider\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.TotalProviderExpectedPayInAmount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 43 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportProvider\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.TotalSlipAmount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 46 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportProvider\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.TotalActualAmount));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n");
            WriteLiteral("        </tr>\r\n");
#nullable restore
#line 54 "C:\Users\k2y2\source\repos\trading\trading\Views\ReportProvider\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<trading.Models.ReportProvider>> Html { get; private set; }
    }
}
#pragma warning restore 1591
