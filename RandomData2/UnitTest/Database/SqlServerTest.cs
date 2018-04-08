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
        public void Categories()
        {
            var expected = Enumerable.Range(0, 10)
                .Select(i =>
                {
                    var id = RandomData.GenerateOrderedGuid2();
                    var name = RandomData.GenerateAlphanumerics(20);
                    System.Threading.Thread.Sleep(1);

                    return new Category
                    {
                        Id = id.Guid.ToString(),
                        Created = id.DateTime,
                        Name = name,
                    };
                })
                .ToArray();

            using (var db = new RandomTestDb())
            {
                db.Categories.AddRange(expected);
                db.SaveChanges();
            }

            Category[] actual;
            using (var db = new RandomTestDb())
            {
                var lower = DateTime.UtcNow.AddSeconds(-30);
                actual = db.Categories
                    .Where(p => p.Created.CompareTo(lower) > 0)
                    .OrderBy(p => p.Id)
                    .ToArray();
            }

            CollectionAssert.AreEqual(expected.Select(x => x.Name).ToArray(), actual.Select(x => x.Name).ToArray());
        }

        [TestMethod]
        public void Products()
        {
            var expected = Enumerable.Range(0, 10)
                .Select(i =>
                {
                    var idSql = RandomData.GenerateOrderedSqlGuid2();
                    var name = RandomData.GenerateAlphanumerics(20);
                    System.Threading.Thread.Sleep(1);

                    return new Product
                    {
                        Id = idSql.Guid,
                        Created = idSql.DateTime,
                        Name = name,
                    };
                })
                .ToArray();

            using (var db = new RandomTestDb())
            {
                db.Products.AddRange(expected);
                db.SaveChanges();
            }

            Product[] actual;
            using (var db = new RandomTestDb())
            {
                var lower = DateTime.UtcNow.AddSeconds(-30);
                actual = db.Products
                    .Where(p => p.Created.CompareTo(lower) > 0)
                    .OrderBy(p => p.Id)
                    .ToArray();
            }

            CollectionAssert.AreEqual(expected.Select(x => x.Name).ToArray(), actual.Select(x => x.Name).ToArray());
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
