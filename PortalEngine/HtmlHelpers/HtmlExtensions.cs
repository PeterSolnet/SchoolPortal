using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Mvc.Ajax;

namespace PortalEngine.HtmlHelpers
{
    
        public static class HtmlExtensions
        {
            public static MvcHtmlString IconActionLink(this AjaxHelper helper, string icon, string text, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
            {
                var builder = new TagBuilder("i");
                builder.MergeAttribute("class", icon);
                var link = helper.ActionLink("[replaceme] " + text, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes).ToHtmlString();
                return new MvcHtmlString(link.Replace("[replaceme]", builder.ToString()));
            }

            public static MvcHtmlString IconActionLink(this HtmlHelper helper, string icon, string text, string actionName, string controllerName, object routeValues, object htmlAttributes)
            {
                var builder = new TagBuilder("i");
                builder.MergeAttribute("class", icon);
                var link = helper.ActionLink("[replaceme] " + text, actionName, controllerName, routeValues, htmlAttributes).ToHtmlString();
                return new MvcHtmlString(link.Replace("[replaceme]", builder.ToString()));
            }

            
        }
}