using System;
using System.Text;
using System.IO;
using OnlineServices.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace OnlineServices.Utilities.Extensions
{
    public static class HtmlExtensions
    {
        public static IHtmlContent Pager<TModel>(this IHtmlHelper<TModel> html, PagerModel model)
        {
            if (model.TotalRecords == 0)
                return new HtmlString("");

            var links = new StringBuilder();
            if (model.ShowTotalSummary && (model.TotalPages > 0))
            {
                links.Append("<div class=\"total-summary\">");
                links.Append(string.Format(" <span class=\"c-page\">نمایش {0}</span> تا {1} (از <span class=\"t-page\">{2}</span> رکورد)", (model.PageIndex * model.PageSize) + 1, (model.PageIndex + 1) * model.PageSize > model.TotalRecords ? model.TotalRecords : (model.PageIndex + 1) * model.PageSize, model.TotalRecords));
                links.Append("</div>");
            }
            if (model.ShowPagerItems && (model.TotalPages > 1))
            {
                links.Append("<ul class=\"pagination\">");
                if (model.ShowFirst)
                {
                    //first page
                    if ((model.PageIndex >= 3) && (model.TotalPages > model.IndividualPagesDisplayedCount))
                    {
                        model.RouteValues.page = 1;

                        links.Append("<li class=\"page-item previous\">");
                        if (model.UseRouteLinks)
                        {
                            var link = html.RouteLink("اولین", model.RouteActionName, model.RouteValues, new { title = "اولین", @class = "page-link" });
                            links.Append(link.ToHtmlString());
                        }
                        else
                        {
                            var link = html.ActionLink("اولین", model.RouteActionName, model.RouteValues, new { title = "اولین", @class = "page-link" });
                            links.Append(link.ToHtmlString());
                        }
                        links.Append("</li>");
                    }
                }
                if (model.ShowPrevious)
                {
                    //previous page
                    //if (model.PageIndex > 0)
                    {
                        model.RouteValues.page = (model.PageIndex);

                        links.Append("<li class=\"page-item previous " + (model.PageIndex == 0 ? "disabled" : "") + "\">");
                        if (model.UseRouteLinks)
                        {
                            var link = html.RouteLink("قبلی", model.RouteActionName, model.RouteValues, new { title = "قبلی", @class = "page-link" });
                            links.Append(link.ToHtmlString());
                        }
                        else
                        {
                            var link = html.ActionLink("قبلی", model.RouteActionName, model.RouteValues, new { title = "قبلی", @class = "page-link" });
                            links.Append(link.ToHtmlString());
                        }
                        links.Append("</li>");
                    }
                }
                if (model.ShowIndividualPages)
                {
                    //individual pages
                    var firstIndividualPageIndex = model.GetFirstIndividualPageIndex();
                    var lastIndividualPageIndex = model.GetLastIndividualPageIndex();
                    for (var i = firstIndividualPageIndex; i <= lastIndividualPageIndex; i++)
                    {
                        if (model.PageIndex == i)
                        {
                            links.AppendFormat("<li class=\"page-item active\"><span class=\"page-link\">{0}</span></li>", (i + 1));
                        }
                        else
                        {
                            model.RouteValues.page = (i + 1);

                            links.Append("<li class=\"page-item individual-page\">");
                            if (model.UseRouteLinks)
                            {
                                var link = html.RouteLink((i + 1).ToString(), model.RouteActionName, model.RouteValues, new { title = string.Format("صفحه {0}", (i + 1)), @class = "page-link" });
                                links.Append(link.ToHtmlString());
                            }
                            else
                            {
                                var link = html.ActionLink((i + 1).ToString(), model.RouteActionName, model.RouteValues, new { title = string.Format("صفحه {0}", (i + 1)), @class = "page-link" });
                                links.Append(link.ToHtmlString());
                            }
                            links.Append("</li>");
                        }
                    }
                }
                if (model.ShowNext)
                {
                    //next page
                    //if ((model.PageIndex + 1) < model.TotalPages)
                    {
                        model.RouteValues.page = (model.PageIndex + 2);

                        links.Append("<li class=\"page-item next " + ((model.PageIndex + 1) >= model.TotalPages ? "disabled" : "") + "\">");
                        if (model.UseRouteLinks)
                        {
                            var link = html.RouteLink("بعدی", model.RouteActionName, model.RouteValues, new { title = "بعدی", @class = "page-link" });
                            links.Append(link.ToHtmlString());
                        }
                        else
                        {
                            var link = html.ActionLink("بعدی", model.RouteActionName, model.RouteValues, new { title = "بعدی", @class = "page-link" });
                            links.Append(link.ToHtmlString());
                        }
                        links.Append("</li>");
                    }
                }
                if (model.ShowLast)
                {
                    //last page
                    if (((model.PageIndex + 3) < model.TotalPages) && (model.TotalPages > model.IndividualPagesDisplayedCount))
                    {
                        model.RouteValues.page = model.TotalPages;

                        links.Append("<li class=\"page-item next\">");
                        if (model.UseRouteLinks)
                        {
                            var link = html.RouteLink("آخرین", model.RouteActionName, model.RouteValues, new { title = "آخرین", @class = "page-link" });
                            links.Append(link.ToHtmlString());
                        }
                        else
                        {
                            var link = html.ActionLink("آخرین", model.RouteActionName, model.RouteValues, new { title = "آخرین", @class = "page-link" });
                            links.Append(link.ToHtmlString());
                        }
                        links.Append("</li>");
                    }
                }
                links.Append("</ul>");
            }
            var result = links.ToString();
            if (!string.IsNullOrEmpty(result))
            {
                result = "<nav>" + result + "</nav>";
            }
            return new HtmlString(result);
        }
        public static IHtmlContent AjaxPager<TModel>(this IHtmlHelper<TModel> html, PagerModel model, string from = "")
        {
            if (model.TotalRecords == 0)
                return new HtmlString("");

            var links = new StringBuilder();
            if (model.ShowTotalSummary && (model.TotalPages > 0))
            {
                links.Append("<div class=\"total-summary\">");
                links.Append(string.Format(" <span class=\"c-page\">نمایش {0}</span> تا {1} (از <span class=\"t-page\">{2}</span> رکورد)", (model.PageIndex * model.PageSize) + 1, (model.PageIndex + 1) * model.PageSize > model.TotalRecords ? model.TotalRecords : (model.PageIndex + 1) * model.PageSize, model.TotalRecords));
                links.Append("</div>");
            }
            if (model.ShowPagerItems && (model.TotalPages > 1))
            {
                links.Append("<ul class=\"pagination\">");
                if (model.ShowFirst)
                {
                    //first page
                    if ((model.PageIndex >= 3) && (model.TotalPages > model.IndividualPagesDisplayedCount))
                    {
                        model.RouteValues.page = 1;

                        links.Append("<li class=\"page-item previous\">");
                        if (model.UseRouteLinks)
                        {
                            links.Append("<a title=\"اولین\" class=\"page-link ajax-pager" + from + " pager-firstpage\" data-page=\"" + 1 + "\"  >اولین</a>");
                        }
                        else
                        {
                            links.Append("<a title=\"اولین\" class=\"page-link ajax-pager" + from + " pager-firstpage\" data-page=\"" + 1 + "\"  >اولین</a>");
                        }
                        links.Append("</li>");
                    }
                }
                if (model.ShowPrevious)
                {
                    //previous page
                    //if (model.PageIndex > 0)
                    {
                        model.RouteValues.page = (model.PageIndex);

                        links.Append("<li class=\"page-item previous " + (model.PageIndex == 0 ? "disabled" : "") + "\">");
                        if (model.UseRouteLinks)
                        {
                            links.Append("<a title=\"قبلی\" class=\"page-link ajax-pager" + from + " pager-prevpage\" data-page=\"" + model.PageIndex + "\"  >قبلی</a>");
                        }
                        else
                        {
                            links.Append("<a title=\"قبلی\" class=\"page-link ajax-pager" + from + " pager-prevpage\" data-page=\"" + model.PageIndex + "\"  >قبلی</a>");
                        }
                        links.Append("</li>");
                    }
                }
                if (model.ShowIndividualPages)
                {
                    //individual pages
                    var firstIndividualPageIndex = model.GetFirstIndividualPageIndex();
                    var lastIndividualPageIndex = model.GetLastIndividualPageIndex();
                    for (var i = firstIndividualPageIndex; i <= lastIndividualPageIndex; i++)
                    {
                        if (model.PageIndex == i)
                        {
                            links.AppendFormat("<li class=\"page-item active\"><span class=\"page-link\">{0}</span></li>", (i + 1));
                        }
                        else
                        {
                            model.RouteValues.page = (i + 1);

                            links.Append("<li class=\"page-item individual-page\">");
                            if (model.UseRouteLinks)
                            {
                                links.Append("<a class=\"page-link ajax-pager" + from + "\" data-page=\"" + (i + 1) + "\"  >" + (i + 1) + "</a>");
                            }
                            else
                            {
                                links.Append("<a class=\"page-link ajax-pager" + from + "\" data-page=\"" + (i + 1) + "\"  >" + (i + 1) + "</a>");
                            }
                            links.Append("</li>");
                        }
                    }
                }
                if (model.ShowNext)
                {
                    //next page
                    //if ((model.PageIndex + 1) < model.TotalPages)
                    {
                        model.RouteValues.page = (model.PageIndex + 2);

                        links.Append("<li class=\"page-item next " + ((model.PageIndex + 1) >= model.TotalPages ? "disabled" : "") + "\">");
                        if (model.UseRouteLinks)
                        {
                            links.Append("<a title=\"بعدی\" class=\"page-link ajax-pager" + from + " pager-nextpage\" data-page=\"" + (model.PageIndex + 2) + "\"  >بعدی</a>");
                        }
                        else
                        {
                            links.Append("<a title=\"بعدی\" class=\"page-link ajax-pager" + from + " pager-nextpage\" data-page=\"" + (model.PageIndex + 2) + "\"  >بعدی</a>");
                        }
                        links.Append("</li>");
                    }
                }
                if (model.ShowLast)
                {
                    //last page
                    if (((model.PageIndex + 3) < model.TotalPages) && (model.TotalPages > model.IndividualPagesDisplayedCount))
                    {
                        model.RouteValues.page = model.TotalPages;

                        links.Append("<li class=\"page-item next\">");
                        if (model.UseRouteLinks)
                        {
                            links.Append("<a title=\"آخرین\" class=\"page-link ajax-pager" + from + " pager-lastpage\" data-page=\"" + model.TotalPages + "\"  >آخرین</a>");
                        }
                        else
                        {
                            links.Append("<a title=\"آخرین\" class=\"page-link ajax-pager" + from + " pager-lastpage\" data-page=\"" + model.TotalPages + "\"  >آخرین</a>");
                        }
                        links.Append("</li>");
                    }
                }
                links.Append("</ul>");
            }
            var result = links.ToString();
            if (!string.IsNullOrEmpty(result))
            {
                result = "<nav>" + result + "</nav>";
            }
            return new HtmlString(result);
        }
        public static string ToHtmlString(this IHtmlContent tag)
        {
            using var writer = new StringWriter();
            tag.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}

