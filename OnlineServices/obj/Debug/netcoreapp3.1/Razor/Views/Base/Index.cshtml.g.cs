#pragma checksum "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "be74efcc195ebbee8cfc8c2e8352ef9310d9536d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Base_Index), @"mvc.1.0.view", @"/Views/Base/Index.cshtml")]
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
#line 1 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\_ViewImports.cshtml"
using OnlineServices;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\_ViewImports.cshtml"
using OnlineServices.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
using OnlineServices.Utilities.Extensions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"be74efcc195ebbee8cfc8c2e8352ef9310d9536d", @"/Views/Base/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7c2311e84e45e130a1a09235b04b9b0f38a5ec5e", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Base_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<OnlineServices.Models.BaseListModel>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary mr-1 mb-1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-toggle", new global::Microsoft.AspNetCore.Html.HtmlString("tooltip"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("ویرایش"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("delete-confirm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("حذف"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
  
    ViewData["Title"] = "مدیریت آیتم مشخصه فنی";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"content-header row\">\r\n    <div class=\"content-header-left col-12 my-2\">\r\n        <div class=\"row breadcrumbs-top\">\r\n            <div class=\"col-12\">\r\n                <h5 class=\"content-header-title float-left\">");
#nullable restore
#line 13 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                                                       Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                <div class=\"breadcrumb-wrapper \">\r\n                    <ol class=\"breadcrumb p-0 pl-1 mb-0\">\r\n                        <li class=\"breadcrumb-item\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "be74efcc195ebbee8cfc8c2e8352ef9310d9536d7490", async() => {
                WriteLiteral("<i class=\"bx bx-home-alt\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </li>\r\n                        <li class=\"breadcrumb-item active\">\r\n                            ");
#nullable restore
#line 20 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                       Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                        </li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
</div>
<div class=""content-body"">
    <section id=""brand-list-content"">
        <div class=""row"">
            <div class=""col-12"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "be74efcc195ebbee8cfc8c2e8352ef9310d9536d9563", async() => {
                WriteLiteral("ثبت مشخصه جدید");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-basekindid", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 32 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                                               WriteLiteral(Model.BaseKindId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["basekindid"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-basekindid", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["basekindid"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
        </div>
        <div class=""row"">
            <div class=""col-md-12"">
                <div class=""card"">
                    <div class=""card-content table-responsive"">
                        <table class=""table mb-0 line-height-1"">
                            <thead>
                                <tr>
                                    <th>
                                        ردیف
                                    </th>
                                    <th>
                                        سرفصل اطلاعات
                                    </th>
                                    <th>
                                        عنوان
                                    </th>
                                    <th>
                                        توضیحات
                                    </th>
                                    <th>
                                        وضعیت
                                    </th>
           ");
            WriteLiteral("                         <th></th>\r\n                                    <th></th>\r\n                                </tr>\r\n                            </thead>\r\n                            <tbody>\r\n");
#nullable restore
#line 62 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                                 if (Model.BaseModel.Count > 0)
                                {
                                    foreach (var item in Model.BaseModel)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <tr>\r\n\r\n                                            <td>\r\n                                                ");
#nullable restore
#line 69 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                                           Write(item.RowNum);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                            </td>\r\n                                            <td>\r\n                                                ");
#nullable restore
#line 72 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                                           Write(item.BaseKindTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                            </td>\r\n                                            <td>\r\n                                                ");
#nullable restore
#line 75 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                                           Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                            </td>\r\n                                            <td>\r\n                                                ");
#nullable restore
#line 78 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                                           Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                            </td>\r\n                                            <td");
            BeginWriteAttribute("class", " class=\"", 3444, "\"", 3501, 1);
#nullable restore
#line 80 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
WriteAttributeValue("", 3452, item.IsActive ? "text-success" : "text-danger", 3452, 49, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                                ");
#nullable restore
#line 81 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                                            Write(item.IsActive ? "فعال" : "غیرفعال");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                            </td>\r\n                                            <td>\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "be74efcc195ebbee8cfc8c2e8352ef9310d9536d16379", async() => {
                WriteLiteral("\r\n                                                    <i class=\"badge-circle badge-circle-light-secondary bx bxs-pencil font-medium-1\"></i>\r\n                                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 84 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                                                                       WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 84 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                                                                                                       WriteLiteral(item.BaseKindId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["basekindid"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-basekindid", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["basekindid"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                            </td>\r\n                                            <td>\r\n                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "be74efcc195ebbee8cfc8c2e8352ef9310d9536d19912", async() => {
                WriteLiteral("\r\n                                                    <i class=\"badge-circle badge-circle-light-danger bx bxs-trash-alt font-medium-1\"></i>\r\n                                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 89 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                                                                         WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                            </td>\r\n                                        </tr>\r\n");
#nullable restore
#line 94 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                                    }
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                    <tr>
                                        <td colspan=""7"" class=""text-center"">
                                            اطلاعاتی جهت نمایش موجود نمی باشد
                                        </td>
                                    </tr>
");
#nullable restore
#line 103 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </tbody>\r\n                        </table>\r\n                    </div>\r\n\r\n");
#nullable restore
#line 108 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                      
                        var pager = Html.Pager(Model.PagerModel);
                    

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"pager\">\r\n                        ");
#nullable restore
#line 112 "C:\Users\siama\OneDrive\Desktop\onlineservicesv2\onlineservicesv2\OnlineServices\Views\Base\Index.cshtml"
                   Write(pager);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </section>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<OnlineServices.Models.BaseListModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591