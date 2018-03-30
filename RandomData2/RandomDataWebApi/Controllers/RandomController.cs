﻿using System;
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
    [Route("{action}")]
    public class RandomController : ApiController
    {
        [HttpGet]
        [Route("NewAlphabets/{length:int:range(0,4096)}")]
        public string NewAlphabets(int length)
        {
            return RandomData.GenerateAlphabets(length);
        }

        [HttpGet]
        [Route("NewAlphanumerics/{length:int:range(0,4096)}")]
        public string NewAlphanumerics(int length)
        {
            return RandomData.GenerateAlphanumerics(length);
        }

        [HttpGet]
        [Route("NewBytes/hexlower/{length:int:range(0,4096)}")]
        public string NewBytes_HexLower(int length)
        {
            var data = RandomData.GenerateBytes(length);
            return data.ToHexString(false);
        }

        [HttpGet]
        [Route("NewBytes/hexupper/{length:int:range(0,4096)}")]
        public string NewBytes_HexUpper(int length)
        {
            var data = RandomData.GenerateBytes(length);
            return data.ToHexString(true);
        }

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
        /// <returns>A new UUID (GUID).</returns>
        [HttpGet]
        public Guid NewUuid() => Guid.NewGuid();
    }
}