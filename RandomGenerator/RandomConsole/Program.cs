using RandomLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomConsole
{
    static class Program
    {
        static void Main(string[] args)
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
                    var id = RandomUtility.GenerateOrderedGuid2();
                    var idSql = RandomUtility.GenerateOrderedSqlGuid2();
                    var name = RandomUtility.GenerateAlphanumerics(20);

                    db.Categories.Add(new Category
                    {
                        Id = id.Guid.ToString(),
                        Created = id.DateTime,
                        Name = name,
                    });
                    db.Products.Add(new Product
                    {
                        Id = idSql.Guid,
                        Created = id.DateTime,
                        Name = name,
                    });

                    //System.Threading.Thread.Sleep(1);
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

        public static Guid ToSqlGuid(this DateTime dateTime)
        {
            return new Guid(0, 0, 0, dateTime.ToBytesForSqlGuid());
        }

        public static Guid ToSqlGuid2(this DateTime dateTime)
        {
            return new Guid(new byte[10].Concat(dateTime.ToBytes2()).ToArray());
        }
    }

    public class RandomTestDb : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }

    public class Category
    {
        [MaxLength(36)]
        public string Id { get; set; }
        public DateTime Created { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }

    public class Product
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
