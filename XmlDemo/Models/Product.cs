using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XmlDemo.Models
{
    public class Product
    {

        public long ID { get; set; }
        public string Product_Name { get; set; }

        public string Details { get; set; }
        public int Price { get; set; }

        public virtual ProductCategory productcategory { get; set; }

        public int ProductCategoryID { get; set; }
    }
}