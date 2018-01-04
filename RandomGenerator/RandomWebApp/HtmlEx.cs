using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RandomWebApp
{
    public static class HtmlEx
    {
        public static MvcHtmlString TextLink(this HtmlHelper htmlHelper, string url)
        {
            var builder = new TagBuilder("a")
            {
                InnerHtml = HttpUtility.HtmlEncode(url),
            };
            builder.MergeAttribute("href", url);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }
    }
}
