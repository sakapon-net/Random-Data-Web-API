using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace UnitTest.Database
{
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
