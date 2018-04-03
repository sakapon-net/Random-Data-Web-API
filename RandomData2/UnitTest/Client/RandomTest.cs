using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Blaze.Randomization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Client
{
    [TestClass]
    public class RandomTest
    {
        static readonly Dictionary<int, HttpStatusCode> StatusCodes_4096 = new Dictionary<int, HttpStatusCode>
        {
            { -1, HttpStatusCode.NotFound },
            { 0, HttpStatusCode.OK },
            { 4096, HttpStatusCode.OK },
            { 4097, HttpStatusCode.NotFound },
        };

        [TestMethod]
        async public Task NewAlphabets_Bound()
        {
            foreach (var item in StatusCodes_4096)
                Assert.AreEqual(item.Value, await HttpHelper.GetAsync_StatusCode($"api/NewAlphabets/{item.Key}"));
        }

        [TestMethod]
        async public Task NewAlphanumerics_Bound()
        {
            foreach (var item in StatusCodes_4096)
                Assert.AreEqual(item.Value, await HttpHelper.GetAsync_StatusCode($"api/NewAlphanumerics/{item.Key}"));
        }

        [TestMethod]
        async public Task NewBytes_HexLower_Bound()
        {
            foreach (var item in StatusCodes_4096)
                Assert.AreEqual(item.Value, await HttpHelper.GetAsync_StatusCode($"api/NewBytes/hexlower/{item.Key}"));
        }

        [TestMethod]
        async public Task NewBytes_HexUpper_Bound()
        {
            foreach (var item in StatusCodes_4096)
                Assert.AreEqual(item.Value, await HttpHelper.GetAsync_StatusCode($"api/NewBytes/hexupper/{item.Key}"));
        }

        [TestMethod]
        async public Task NewBytes_Base64_Bound()
        {
            foreach (var item in StatusCodes_4096)
                Assert.AreEqual(item.Value, await HttpHelper.GetAsync_StatusCode($"api/NewBytes/base64/{item.Key}"));
        }

        [TestMethod]
        async public Task NewUuid()
        {
            for (int i = 0; i < 100; i++)
            {
                var result = await HttpHelper.GetAsync<Guid>("api/NewUuid");
                Console.WriteLine(result);
            }
        }

        [TestMethod]
        async public Task NewOrderedId()
        {
            for (int i = 0; i < 100; i++)
            {
                var result = await HttpHelper.GetAsync<GuidWithDateTime>("api/NewOrderedId");
                Console.WriteLine(result.Guid);
            }
        }

        [TestMethod]
        async public Task NewOrderedId_SqlServer()
        {
            for (int i = 0; i < 100; i++)
            {
                var result = await HttpHelper.GetAsync<GuidWithDateTime>("api/NewOrderedId/sqlserver");
                Console.WriteLine(result.Guid);
            }
        }

        [TestMethod]
        async public Task NewOrderedId2()
        {
            for (int i = 0; i < 100; i++)
            {
                var result = await HttpHelper.GetAsync<GuidWithDateTime>("api/NewOrderedId2");
                Console.WriteLine(result.Guid);
            }
        }

        [TestMethod]
        async public Task NewOrderedId2_SqlServer()
        {
            for (int i = 0; i < 100; i++)
            {
                var result = await HttpHelper.GetAsync<GuidWithDateTime>("api/NewOrderedId2/sqlserver");
                Console.WriteLine(result.Guid);
            }
        }
    }
}
