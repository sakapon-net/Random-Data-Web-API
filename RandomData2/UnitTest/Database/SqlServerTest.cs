using System;
using System.Linq;
using Blaze.Randomization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Database
{
    [TestClass]
    public class SqlServerTest
    {
        [TestMethod]
        public void Data()
        {
            InsertData();
            GetData();
        }

        static void InsertData()
        {
            using (var db = new RandomTestDb())
            {
                for (int i = 0; i < 20; i++)
                {
                    var id = RandomData.GenerateOrderedGuid2();
                    var idSql = RandomData.GenerateOrderedSqlGuid2();
                    var name = RandomData.GenerateAlphanumerics(20);

                    db.Categories.Add(new Category
                    {
                        Id = id.Guid.ToString(),
                        Created = id.DateTime,
                        Name = name,
                    });
                    db.Products.Add(new Product
                    {
                        Id = idSql.Guid,
                        Created = idSql.DateTime,
                        Name = name,
                    });

                    System.Threading.Thread.Sleep(1);
                }

                db.SaveChanges();
            }
        }

        static void GetData()
        {
            using (var db = new RandomTestDb())
            {
                var now = DateTime.UtcNow;
                var lower = now.AddSeconds(-30).ToSqlGuid2();
                var upper = now.ToSqlGuid2();

                var products = db.Products
                    .Where(p => p.Id.CompareTo(lower) > 0)
                    .Where(p => p.Id.CompareTo(upper) < 0)
                    .ToArray();

                Assert.AreEqual(20, products.Length);
                foreach (var product in products)
                    Console.WriteLine(product.Created.ToIso8601String());
            }
        }
    }

    public static class RandomDataHelper
    {
        public static Guid ToSqlGuid(this DateTime dateTime)
        {
            return new Guid(0, 0, 0, dateTime.ToBytesForSqlGuid());
        }

        public static Guid ToSqlGuid2(this DateTime dateTime)
        {
            return new Guid(new byte[10].Concat(dateTime.ToBytes2()).ToArray());
        }
    }
}
