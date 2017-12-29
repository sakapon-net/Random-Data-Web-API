using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RandomWebApp
{
    public class JsonApiController : Controller
    {
        protected new ActionResult Json(object data)
        {
            Response.Headers["Expires"] = "-1";
            Response.Headers["Access-Control-Allow-Origin"] = "*";

            var callback = Request.QueryString["callback"];

            if (string.IsNullOrWhiteSpace(callback))
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                return JavaScript(string.Format("{0}({1});", callback, json));
            }
        }
    }
}
