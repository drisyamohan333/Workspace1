using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XmlDemo.Models
{
    public class ProductCategory
    {

        public int ProductCategoryID { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Category")]
        public string strCategory { get; set; }
        //Navigational property
        public virtual ICollection<Product> products { get; set; }
    }
}