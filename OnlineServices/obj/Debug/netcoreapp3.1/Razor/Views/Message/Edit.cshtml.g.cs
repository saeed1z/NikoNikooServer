#pragma checksum "D:\Projects\Web Projects\NikoNikooServer\OnlineServices\Views\Message\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "55b336263308b45f538f16b4ee53622351af6e22"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Message_Edit), @"mvc.1.0.view", @"/Views/Message/Edit.cshtml")]
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
#line 1 "D:\Projects\Web Projects\NikoNikooServer\OnlineServices\Views\_ViewImports.cshtml"
using OnlineServices;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Projects\Web Projects\NikoNikooServer\OnlineServices\Views\_ViewImports.cshtml"
using OnlineServices.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"55b336263308b45f538f16b4ee53622351af6e22", @"/Views/Message/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7c2311e84e45e130a1a09235b04b9b0f38a5ec5e", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Message_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<OnlineServices.Models.ModelModel>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("alert alert-danger mb-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("alert"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Save", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Projects\Web Projects\NikoNikooServer\OnlineServices\Views\Message\Edit.cshtml"
  
    ViewData["Title"] = Model.Id != 0 ? "ویرایش مدل" : "ثبت مدل جدید";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"content-header row\">\r\n    <div class=\"content-header-left col-12 my-2\">\r\n        <div class=\"row breadcrumbs-top\">\r\n            <div class=\"col-12\">\r\n                <h5 class=\"content-header-title float-left\">");
#nullable restore
#line 12 "D:\Projects\Web Projects\NikoNikooServer\OnlineServices\Views\Message\Edit.cshtml"
                                                       Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                <div class=\"breadcrumb-wrapper \">\r\n                    <ol class=\"breadcrumb p-0 pl-1 mb-0\">\r\n                        <li class=\"breadcrumb-item\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "55b336263308b45f538f16b4ee53622351af6e226611", async() => {
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
            WriteLiteral("\r\n                        </li>\r\n                        <li class=\"breadcrumb-item\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "55b336263308b45f538f16b4ee53622351af6e228117", async() => {
                WriteLiteral("مدیریت مدل‌ها");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
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
#line 22 "D:\Projects\Web Projects\NikoNikooServer\OnlineServices\Views\Message\Edit.cshtml"
                       Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </li>\r\n                    </ol>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<div class=\"content-body\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "55b336263308b45f538f16b4ee53622351af6e229841", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 32 "D:\Projects\Web Projects\NikoNikooServer\OnlineServices\Views\Message\Edit.cshtml"
         if (!Html.ViewData.ModelState.IsValid)
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "55b336263308b45f538f16b4ee53622351af6e2210369", async() => {
                    WriteLiteral("\r\n            ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 34 "D:\Projects\Web Projects\NikoNikooServer\OnlineServices\Views\Message\Edit.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.All;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 36 "D:\Projects\Web Projects\NikoNikooServer\OnlineServices\Views\Message\Edit.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"        <section>
            <div class=""row"">
                <div class=""col-md-12"">
                    <div class=""simplebar-content"" style=""padding: 0px;"">
                        <div class=""nk-reply-item"">
                            <div class=""nk-reply-header"">
                                <div class=""user-card"">
                                    <div class=""user-avatar sm bg-blue""><span>AB</span></div>
                                    <div class=""user-name"">Abu Bin Ishtiyak</div>
                                </div>
                                <div class=""date-time"">14 Jan, 2020</div>
                            </div>
                            <div class=""nk-reply-body"">
                                <div class=""nk-reply-entry entry"">
                                    <p>Hello team,</p>
                                    <p>I am facing problem as i can not select currency on buy order page. Can you guys let me know what i am doing doing wrong? Please check attac");
                WriteLiteral(@"hed files.</p>
                                    <p>Thank you <br> Ishityak</p>
                                </div>
                                <div class=""attach-files"">
                                    <ul class=""attach-list"">
                                        <li class=""attach-item""><a class=""download"" href=""#""><em class=""icon ni ni-img""></em><span>error-show-On-order.jpg</span></a></li>
                                        <li class=""attach-item""><a class=""download"" href=""#""><em class=""icon ni ni-img""></em><span>full-page-error.jpg</span></a></li>
                                    </ul>
                                    <div class=""attach-foot""><span class=""attach-info"">2 files attached</span><a class=""attach-download link"" href=""#""><em class=""icon ni ni-download""></em><span>Download All</span></a></div>
                                </div>
                            </div>
                        </div>
                        <div class=""nk-reply-item"">
         ");
                WriteLiteral(@"                   <div class=""nk-reply-header"">
                                <div class=""user-card"">
                                    <div class=""user-avatar sm bg-pink""><span>ST</span></div>
                                    <div class=""user-name"">Support Team <span>(You)</span></div>
                                </div>
                                <div class=""date-time"">14 Jan, 2020</div>
                            </div>
                            <div class=""nk-reply-body"">
                                <div class=""nk-reply-entry entry"">
                                    <p>Hello Ishtiyak,</p>
                                    <p>We are really sorry to hear that, you have face an unexpected experience. Our team urgently look this matter and get back to you asap. </p>
                                    <p>Thank you very much. </p>
                                </div>
                                <div class=""nk-reply-from""> Replied by <span>Iliash Hossain</span> at ");
                WriteLiteral(@"11:32 AM </div>
                            </div>
                        </div>
                        <div class=""nk-reply-meta"">
                            <div class=""nk-reply-meta-info""><span class=""who"">Iliash Hossian</span> assigned user: <span class=""whom"">Saiful Islam</span> at 14 Jan, 2020 at 11:34 AM</div>
                        </div>
                        <div class=""nk-reply-item"">
                            <div class=""nk-reply-header"">
                                <div class=""user-card"">
                                    <div class=""user-avatar sm bg-purple""><span>IH</span></div>
                                    <div class=""user-name"">Iliash Hossain <span>added a note</span></div>
                                </div>
                                <div class=""date-time"">14 Jan, 2020</div>
                            </div>
                            <div class=""nk-reply-body"">
                                <div class=""nk-reply-entry entry note"">
           ");
                WriteLiteral(@"                         <p>Devement Team need to check out the issues.</p>
                                </div>
                            </div>
                        </div>
                        <div class=""nk-reply-meta"">
                            <div class=""nk-reply-meta-info""><strong>15 January 2020</strong></div>
                        </div>
                        <div class=""nk-reply-item"">
                            <div class=""nk-reply-header"">
                                <div class=""user-card"">
                                    <div class=""user-avatar sm bg-pink""><span>ST</span></div>
                                    <div class=""user-name"">Support Team <span>(You)</span></div>
                                </div>
                                <div class=""date-time"">15 Jan, 2020</div>
                            </div>
                            <div class=""nk-reply-body"">
                                <div class=""nk-reply-entry entry"">
                ");
                WriteLiteral(@"                    <p>Hello Ishtiyak,</p>
                                    <p>Thanks for waiting for us. Our team solved the issues. So check now on our website. Hopefuly you can order now.</p>
                                    <p>Thank you very much once again.</p>
                                </div>
                                <div class=""nk-reply-from""> Replied by <span>Noor Parvez</span> at 11:32 AM </div>
                            </div>
                        </div>
                        <div class=""nk-reply-form"">
                            <div class=""nk-reply-form-header"">
                                <ul class=""nav nav-tabs-s2 nav-tabs nav-tabs-sm"">
                                    <li class=""nav-item""><a class=""nav-link active"" data-toggle=""tab"" href=""#reply-form"">Reply</a></li>
                                    <li class=""nav-item""><a class=""nav-link"" data-toggle=""tab"" href=""#note-form"">Private Note</a></li>
                                </ul>
            ");
                WriteLiteral(@"                    <div class=""nk-reply-form-title"">
                                    <div class=""title"">Reply as:</div>
                                    <div class=""user-avatar xs bg-purple""><span>IH</span></div>
                                </div>
                            </div>
                            <div class=""tab-content"">
                                <div class=""tab-pane active"" id=""reply-form"">
                                    <div class=""nk-reply-form-editor"">
                                        <div class=""nk-reply-form-field""><textarea class=""form-control form-control-simple no-resize"" placeholder=""Hello""></textarea></div>
                                        <div class=""nk-reply-form-tools"">
                                            <ul class=""nk-reply-form-actions g-1"">
                                                <li class=""mr-2""><button class=""btn btn-primary"" type=""submit"">Reply</button></li>
                                                <li>
");
                WriteLiteral("                                                    <div class=\"dropdown\">\r\n                                                        <a class=\"btn btn-icon btn-sm btn-tooltip\" data-toggle=\"dropdown\"");
                BeginWriteAttribute("title", " title=\"", 8782, "\"", 8790, 0);
                EndWriteAttribute();
                WriteLiteral(@" href=""#"" data-original-title=""Templates""><em class=""icon ni ni-hash""></em></a>
                                                        <div class=""dropdown-menu"">
                                                            <ul class=""link-list-opt no-bdr link-list-template"">
                                                                <li class=""opt-head""><span>Quick Insert</span></li>
                                                                <li><a href=""#""><span>Thank you message</span></a></li>
                                                                <li><a href=""#""><span>Your issues solved</span></a></li>
                                                                <li><a href=""#""><span>Thank you message</span></a></li>
                                                                <li class=""divider""></li>
                                                                <li><a href=""#""><em class=""icon ni ni-file-plus""></em><span>Save as Template</span></a></li>
              ");
                WriteLiteral(@"                                                  <li><a href=""#""><em class=""icon ni ni-notes-alt""></em><span>Manage Template</span></a></li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </li>
                                                <li><a class=""btn btn-icon btn-sm"" data-toggle=""tooltip"" data-placement=""top""");
                BeginWriteAttribute("title", " title=\"", 10329, "\"", 10337, 0);
                EndWriteAttribute();
                WriteLiteral(" href=\"#\" data-original-title=\"Upload Attachment\"><em class=\"icon ni ni-clip-v\"></em></a></li>\r\n                                                <li><a class=\"btn btn-icon btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\"");
                BeginWriteAttribute("title", " title=\"", 10559, "\"", 10567, 0);
                EndWriteAttribute();
                WriteLiteral(" href=\"#\" data-original-title=\"Insert Emoji\"><em class=\"icon ni ni-happy\"></em></a></li>\r\n                                                <li><a class=\"btn btn-icon btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\"");
                BeginWriteAttribute("title", " title=\"", 10783, "\"", 10791, 0);
                EndWriteAttribute();
                WriteLiteral(@" href=""#"" data-original-title=""Upload Images""><em class=""icon ni ni-img""></em></a></li>
                                            </ul>
                                            <div class=""dropdown"">
                                                <a href=""#"" class=""dropdown-toggle btn-trigger btn btn-icon mr-n2"" data-toggle=""dropdown""><em class=""icon ni ni-more-v""></em></a>
                                                <div class=""dropdown-menu dropdown-menu-right"">
                                                    <ul class=""link-list-opt no-bdr"">
                                                        <li><a href=""#""><span>Another Option</span></a></li>
                                                        <li><a href=""#""><span>More Option</span></a></li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                     ");
                WriteLiteral(@"               </div>
                                </div>
                                <div class=""tab-pane"" id=""note-form"">
                                    <div class=""nk-reply-form-editor"">
                                        <div class=""nk-reply-form-field""><textarea class=""form-control form-control-simple no-resize"" placeholder=""Type your private note, that only visible to internal team.""></textarea></div>
                                        <div class=""nk-reply-form-tools"">
                                            <ul class=""nk-reply-form-actions g-1"">
                                                <li class=""mr-2""><button class=""btn btn-primary"" type=""submit"">Add Note</button></li>
                                                <li><a class=""btn btn-icon btn-sm"" data-toggle=""tooltip"" data-placement=""top""");
                BeginWriteAttribute("title", " title=\"", 12668, "\"", 12676, 0);
                EndWriteAttribute();
                WriteLiteral(@" href=""#"" data-original-title=""Upload Attachment""><em class=""icon ni ni-clip-v""></em></a></li>
                                            </ul>
                                            <div class=""dropdown"">
                                                <a href=""#"" class=""dropdown-toggle btn-trigger btn btn-icon mr-n2"" data-toggle=""dropdown""><em class=""icon ni ni-more-v""></em></a>
                                                <div class=""dropdown-menu dropdown-menu-right"">
                                                    <ul class=""link-list-opt no-bdr"">
                                                        <li><a href=""#""><span>Another Option</span></a></li>
                                                        <li><a href=""#""><span>More Option</span></a></li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
              ");
                WriteLiteral(@"                      </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <div class=""row"">
            <div class=""col-12"">
                <button type=""submit"" class=""btn btn-success glow mr-1 mb-1"">
                    <i class=""bx bx-check""></i>
                    <span class=""align-middle ml-25"">ثبت اطلاعات</span>
                </button>

            </div>
        </div>
        ");
#nullable restore
#line 203 "D:\Projects\Web Projects\NikoNikooServer\OnlineServices\Views\Message\Edit.cshtml"
   Write(Html.HiddenFor(p => p.Id));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
#nullable restore
#line 31 "D:\Projects\Web Projects\NikoNikooServer\OnlineServices\Views\Message\Edit.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Antiforgery = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-antiforgery", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Antiforgery, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<OnlineServices.Models.ModelModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
