using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Blaze.Randomization;

namespace RandomDataWebApi.Controllers
{
    /// <summary>
    /// Generates random data.
    /// </summary>
    [RoutePrefix("api")]
    public class RandomController : ApiController
    {
        /// <summary>
        /// Creates new alphabets of the specified length.
        /// </summary>
        /// <param name="length">The length of the result. 0 ≦ x ≦ 4096.</param>
        /// <returns>Alphabets.</returns>
        [HttpGet]
        [Route("NewAlphabets/{length:int:range(0,4096)}")]
        public string NewAlphabets(int length)
        {
            return RandomData.GenerateAlphabets(length);
        }

        /// <summary>
        /// Creates new alphanumerics of the specified length.
        /// </summary>
        /// <param name="length">The length of the result. 0 ≦ x ≦ 4096.</param>
        /// <returns>Alphanumerics.</returns>
        [HttpGet]
        [Route("NewAlphanumerics/{length:int:range(0,4096)}")]
        public string NewAlphanumerics(int length)
        {
            return RandomData.GenerateAlphanumerics(length);
        }

        /// <summary>
        /// Creates a new byte sequence of the specified length, with the lowercase hexadecimal format.
        /// </summary>
        /// <param name="length">The length of the result. 0 ≦ x ≦ 4096.</param>
        /// <returns>The lowercase hexadecimal format.</returns>
        [HttpGet]
        [Route("NewBytes/hexlower/{length:int:range(0,4096)}")]
        public string NewBytes_HexLower(int length)
        {
            var data = RandomData.GenerateBytes(length);
            return data.ToHexString(false);
        }

        /// <summary>
        /// Creates a new byte sequence of the specified length, with the uppercase hexadecimal format.
        /// </summary>
        /// <param name="length">The length of the result. 0 ≦ x ≦ 4096.</param>
        /// <returns>The uppercase hexadecimal format.</returns>
        [HttpGet]
        [Route("NewBytes/hexupper/{length:int:range(0,4096)}")]
        public string NewBytes_HexUpper(int length)
        {
            var data = RandomData.GenerateBytes(length);
            return data.ToHexString(true);
        }

        /// <summary>
        /// Creates a new byte sequence of the specified length, with the Base64 format.
        /// </summary>
        /// <param name="length">The length of the result. 0 ≦ x ≦ 4096.</param>
        /// <returns>The Base64 format.</returns>
        [HttpGet]
        [Route("NewBytes/base64/{length:int:range(0,4096)}")]
        public string NewBytes_Base64(int length)
        {
            var data = RandomData.GenerateBytes(length);
            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// Creates a new UUID (GUID).
        /// </summary>
        /// <returns>A <a href="http://en.wikipedia.org/wiki/Universally_unique_identifier#Version_4_.28random.29" target="_blank">version 4 UUID</a>.</returns>
        [HttpGet]
        [Route("NewUuid")]
        public Guid NewUuid()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Creates a new time-ordered 16-byte ID with the UUID format.
        /// The upper 8 bytes represent ticks of date/time (by 10<sup>-7</sup> second) and the other 8 bytes are randomly generated.
        /// </summary>
        /// <returns>A pair of the ID and the created date/time.</returns>
        [HttpGet]
        [Route("NewOrderedId")]
        public GuidWithDateTime NewOrderedId()
        {
            return RandomData.GenerateOrderedGuid();
        }

        /// <summary>
        /// Creates a new time-ordered 16-byte ID with the UUID format, which is ordered for the uniqueidentifier data type of the SQL Server.
        /// The lower 8 bytes represent ticks of date/time (by 10<sup>-7</sup> second) and the other 8 bytes are randomly generated.
        /// </summary>
        /// <returns>A pair of the ID and the created date/time.</returns>
        [HttpGet]
        [Route("NewOrderedId/sqlserver")]
        public GuidWithDateTime NewOrderedId_SqlServer()
        {
            return RandomData.GenerateOrderedSqlGuid();
        }

        /// <summary>
        /// Creates a new time-ordered 16-byte ID with the UUID format.
        /// The upper 6 bytes represent ticks of date/time (by about 10<sup>-3</sup> second) and the other 10 bytes are randomly generated.
        /// </summary>
        /// <returns>A pair of the ID and the created date/time.</returns>
        [HttpGet]
        [Route("NewOrderedId2")]
        public GuidWithDateTime NewOrderedId2()
        {
            return RandomData.GenerateOrderedGuid2();
        }

        /// <summary>
        /// Creates a new time-ordered 16-byte ID with the UUID format, which is ordered for the uniqueidentifier data type of the SQL Server.
        /// The lower 6 bytes represent ticks of date/time (by about 10<sup>-3</sup> second) and the other 10 bytes are randomly generated.
        /// </summary>
        /// <returns>A pair of the ID and the created date/time.</returns>
        [HttpGet]
        [Route("NewOrderedId2/sqlserver")]
        public GuidWithDateTime NewOrderedId2_SqlServer()
        {
            return RandomData.GenerateOrderedSqlGuid2();
        }
    }
}
