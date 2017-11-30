using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace XmlDemo.Models
{
    public class ProductDbContext : DbContext
    {

            public ProductDbContext() : base("name=ProductDbContext")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}