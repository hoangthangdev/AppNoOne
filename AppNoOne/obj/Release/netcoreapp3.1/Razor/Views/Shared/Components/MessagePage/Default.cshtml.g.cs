#pragma checksum "D:\AppNoOne\AppNoOne\Views\Shared\Components\MessagePage\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eb8311a1ccc4a0a07db65ec510d1d6cc8b027baa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_MessagePage_Default), @"mvc.1.0.view", @"/Views/Shared/Components/MessagePage/Default.cshtml")]
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
#line 1 "D:\AppNoOne\AppNoOne\Views\_ViewImports.cshtml"
using AppNoOne;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\AppNoOne\AppNoOne\Views\_ViewImports.cshtml"
using AppNoOne.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eb8311a1ccc4a0a07db65ec510d1d6cc8b027baa", @"/Views/Shared/Components/MessagePage/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f00ccbd9481cc4649f61d23c4bbac1490d934059", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared_Components_MessagePage_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AppNoOne.ViewComponents.MessagePage.Message>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\AppNoOne\AppNoOne\Views\Shared\Components\MessagePage\Default.cshtml"
  
    Layout = "_LayoutManage";
    ViewData["Title"] = @Model.title;

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"card m-4\">\r\n   <div class=\"card-header bg-danger text-light\">\r\n       <h4>");
#nullable restore
#line 8 "D:\AppNoOne\AppNoOne\Views\Shared\Components\MessagePage\Default.cshtml"
      Write(Model.title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n   </div>\r\n   <div class=\"card-body\">\r\n       ");
#nullable restore
#line 11 "D:\AppNoOne\AppNoOne\Views\Shared\Components\MessagePage\Default.cshtml"
  Write(Html.Raw(Model.htmlcontent));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n   </div>\r\n   <div class=\"card-footer\">\r\n       Chuyển đến - <a");
            BeginWriteAttribute("href", " href=\"", 373, "\"", 398, 1);
#nullable restore
#line 14 "D:\AppNoOne\AppNoOne\Views\Shared\Components\MessagePage\Default.cshtml"
WriteAttributeValue("", 380, Model.urlredirect, 380, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 14 "D:\AppNoOne\AppNoOne\Views\Shared\Components\MessagePage\Default.cshtml"
                                            Write(Model.urlredirect);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n   </div>\r\n</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AppNoOne.ViewComponents.MessagePage.Message> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591