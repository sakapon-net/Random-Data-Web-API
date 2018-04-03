using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace RandomDataWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static string Title { get; } = typeof(WebApiApplication).Assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;
        public static string Copyright { get; } = typeof(WebApiApplication).Assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
        public static string Version { get; } = typeof(WebApiApplication).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection &&
                RequireHttps &&
                !string.Equals(Request.Url.Host, "localhost", StringComparison.InvariantCultureIgnoreCase))
            {
                // If we use Uri.OriginalString property, ":80" may be appended.
                var secureUrl = Regex.Replace(Request.Url.AbsoluteUri, @"^\w+(?=://)", Uri.UriSchemeHttps);

                if (PermanentHttps)
                    Response.RedirectPermanent(secureUrl, true);
                else
                    Response.Redirect(secureUrl, true);
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
