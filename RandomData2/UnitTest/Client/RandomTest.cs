using System;
using System.Threading.Tasks;
using Blaze.Randomization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Client
{
    [TestClass]
    public class RandomTest
    {
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
