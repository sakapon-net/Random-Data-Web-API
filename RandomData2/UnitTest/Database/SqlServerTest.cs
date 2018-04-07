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
                var lower = new DateTime(2014, 4, 10, 14, 54, 59).ToSqlGuid2();
                var upper = new DateTime(2014, 4, 10, 14, 55, 1).ToSqlGuid2();

                var products = db.Products
                    .Where(p => p.Id.CompareTo(lower) > 0)
                    .Where(p => p.Id.CompareTo(upper) < 0)
                    .ToArray();

                foreach (var item in products)
                {
                    Console.WriteLine(item.Created);
                }
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
