using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Client
{
    [TestClass]
    public class GlobalTest
    {
        [TestMethod]
        async public Task UriParameters()
        {
            Assert.AreEqual(HttpStatusCode.NotFound, await HttpHelper.GetAsync_StatusCode("api/NewAlphabets?length=8"));
            Assert.AreEqual(HttpStatusCode.NotFound, await HttpHelper.GetAsync_StatusCode("api/NewAlphabets/?length=8"));
        }

        [TestMethod]
        async public Task Cors()
        {
            var uri = "api/NewUuid";

            using (var http = new HttpClient { BaseAddress = HttpHelper.BaseUri })
            {
                http.DefaultRequestHeaders.Add("Origin", "http://localhost:8080");
                var response = await http.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var allowOrigin = response.Headers.GetValues("Access-Control-Allow-Origin").ToArray();
                CollectionAssert.AreEqual(new[] { "*" }, allowOrigin);
            }
        }

        async static Task ContentType_Test(string[] acceptMediaTypes, string expectedMediaType)
        {
            var uri = "api/NewUuid";

            using (var http = new HttpClient { BaseAddress = HttpHelper.BaseUri })
            {
                foreach (var mediaType in acceptMediaTypes)
                    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
                var response = await http.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                Assert.AreEqual(expectedMediaType, response.Content.Headers.ContentType.MediaType);
                var result = await response.Content.ReadAsAsync<Guid>();
                Console.WriteLine(result);
            }
        }

        [TestMethod]
        async public Task ContentType_Json()
        {
            await ContentType_Test(new[] { "application/json" }, "application/json");
        }

        [TestMethod]
        async public Task ContentType_Xml()
        {
            await ContentType_Test(new[] { "application/xml" }, "application/json");
        }

        [TestMethod]
        async public Task ContentType_Html()
        {
            await ContentType_Test(new[] { "text/html" }, "application/json");
        }

        [TestMethod]
        async public Task ContentType_Chrome()
        {
            await ContentType_Test(new[] { "text/html", "application/xml" }, "application/json");
        }
    }
}
