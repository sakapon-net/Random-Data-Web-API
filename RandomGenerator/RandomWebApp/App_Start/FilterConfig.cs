using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace RandomWebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void ValidateUrlScheme(HttpApplication app)
        {
            if (!app.Request.IsSecureConnection &&
                RequireHttps &&
                !string.Equals(app.Request.Url.Host, "localhost", StringComparison.InvariantCultureIgnoreCase))
            {
                // Uri.OriginalString プロパティを使用すると、:80 が追加されてしまうことがあります。
                var secureUrl = Regex.Replace(app.Request.Url.AbsoluteUri, @"^\w+(?=://)", Uri.UriSchemeHttps);

                if (PermanentHttps)
                {
                    app.Response.RedirectPermanent(secureUrl, true);
                }
                else
                {
                    app.Response.Redirect(secureUrl, true);
                }
            }
        }

        static bool RequireHttps
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["app:RequireHttps"]); }
        }

        static bool PermanentHttps
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["app:PermanentHttps"]); }
        }
    }
}