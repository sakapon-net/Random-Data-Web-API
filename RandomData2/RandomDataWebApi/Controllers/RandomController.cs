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
        /// Creates new alphabets with the specified length.
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
        /// Creates new alphanumerics with the specified length.
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
        /// Creates a new byte sequence with the specified length.
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
        /// Creates a new byte sequence with the specified length.
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
        /// Creates a new byte sequence with the specified length.
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
        /// <returns>A version 4 UUID.</returns>
        [HttpGet]
        [Route("NewUuid")]
        public Guid NewUuid()
        {
            return Guid.NewGuid();
        }

        [HttpGet]
        [Route("NewOrderedId")]
        public GuidWithDateTime NewOrderedId()
        {
            return RandomData.GenerateOrderedGuid();
        }

        [HttpGet]
        [Route("NewOrderedId/sqlserver")]
        public GuidWithDateTime NewOrderedId_SqlServer()
        {
            return RandomData.GenerateOrderedSqlGuid();
        }

        [HttpGet]
        [Route("NewOrderedId2")]
        public GuidWithDateTime NewOrderedId2()
        {
            return RandomData.GenerateOrderedGuid2();
        }

        [HttpGet]
        [Route("NewOrderedId2/sqlserver")]
        public GuidWithDateTime NewOrderedId2_SqlServer()
        {
            return RandomData.GenerateOrderedSqlGuid2();
        }
    }
}
