using RandomLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RandomWebApp.Controllers
{
    // Attribute Routing in ASP.NET MVC 5
    // http://blogs.msdn.com/b/webdev/archive/2013/10/17/attribute-routing-in-asp-net-mvc-5.aspx
    [RoutePrefix("json")]
    [Route("{action=Index}")]
    public class RandomController : JsonApiController
    {
        public ActionResult Index()
        {
            return RedirectToAction("", "");
        }

        // GET: /json/NewAlphabets/8
        [Route("NewAlphabets/{length:int:range(0,4096)}")]
        public ActionResult NewAlphabets(int length)
        {
            return Json(RandomUtility.GenerateAlphabets(length));
        }

        // GET: /json/NewAlphanumerics/8
        [Route("NewAlphanumerics/{length:int:range(0,4096)}")]
        public ActionResult NewAlphanumerics(int length)
        {
            return Json(RandomUtility.GenerateAlphanumerics(length));
        }

        // GET: /json/NewBytes/hexlower/16
        [Route("NewBytes/hexlower/{length:int:range(0,4096)}")]
        public ActionResult NewBytes_HexLower(int length)
        {
            var data = RandomUtility.GenerateBytes(length);
            return Json(data.ToHexString(false));
        }

        // GET: /json/NewBytes/hexupper/16
        [Route("NewBytes/hexupper/{length:int:range(0,4096)}")]
        public ActionResult NewBytes_HexUpper(int length)
        {
            var data = RandomUtility.GenerateBytes(length);
            return Json(data.ToHexString(true));
        }

        // GET: /json/NewBytes/base64/16
        [Route("NewBytes/base64/{length:int:range(0,4096)}")]
        public ActionResult NewBytes_Base64(int length)
        {
            var data = RandomUtility.GenerateBytes(length);
            return Json(Convert.ToBase64String(data));
        }

        // GET: /json/NewUuid
        public ActionResult NewUuid()
        {
            return Json(Guid.NewGuid());
        }

        // GET: /json/NewOrderedId
        public ActionResult NewOrderedId()
        {
            var id = RandomUtility.GenerateOrderedGuid();
            return Json(new { id = id.Guid, date = id.DateTime.ToIso8601String() });
        }

        // GET: /json/NewOrderedId/sqlserver
        [Route("NewOrderedId/sqlserver")]
        public ActionResult NewOrderedId_SqlServer()
        {
            var id = RandomUtility.GenerateOrderedSqlGuid();
            return Json(new { id = id.Guid, date = id.DateTime.ToIso8601String() });
        }

        // GET: /json/NewOrderedId2
        public ActionResult NewOrderedId2()
        {
            var id = RandomUtility.GenerateOrderedGuid2();
            return Json(new { id = id.Guid, date = id.DateTime.ToIso8601String() });
        }

        // GET: /json/NewOrderedId2/sqlserver
        [Route("NewOrderedId2/sqlserver")]
        public ActionResult NewOrderedId2_SqlServer()
        {
            var id = RandomUtility.GenerateOrderedSqlGuid2();
            return Json(new { id = id.Guid, date = id.DateTime.ToIso8601String() });
        }
    }
}
