using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XmlDemo.Models;

namespace XmlDemo.Controllers
{
    public class ProductController : Controller
    {
        private ProductDbContext db = new ProductDbContext();

        //
        // GET: /Product/


        public ActionResult ImportProducts()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportProducts(string tbFilePath)
        {
            string strResponse;

            strResponse = "<font style='font-size:18pt;'><b>Entered ImportGiftItems .... </b><br><br><br></font>";
             

            if (!string.IsNullOrEmpty(tbFilePath))
            {
                try
                {


                    System.Xml.Linq.XDocument xmlDoc = System.Xml.Linq.XDocument.Load(tbFilePath);

                    var gifts = from g in xmlDoc.Descendants("Product")
                                select g;

                    foreach (var gift in gifts)
                    {
                        Product newGift = new Product();
                        newGift.Details = gift.Element("Details").Value;
                        newGift.Product_Name = gift.Element("Name").Value;
                        newGift.ProductCategoryID = Convert.ToInt32(gift.Element("Category").Value);
                        newGift.Price = Convert.ToInt32(gift.Element("Price").Value);
                        db.Products.Add(newGift);
                        db.SaveChanges();
                    }
                    strResponse = "<font style='font-size:18pt;'><b>Gift items added successfully in the database.</b><br><br><br></font>";
                }
                catch (System.Exception exx)
                {
                    Console.WriteLine(exx.Message);
                    strResponse = exx.Message;
                    strResponse = exx.Source+" "+exx.StackTrace;
                   // strResponse = exx.StackTrace;
                 //   strResponse = "<font style='font-size:18pt;color:red;'><b>Invalid file path.</b><br><br><br></font>";
                }

            }
            else
                strResponse = "<font style='font-size:18pt;color:red;'><b>Please specify a valid file path.</b><br><br><br></font>";
            ViewBag.response = strResponse;
            return View();
        }


        //
        // GET: /Gifts/

        public ActionResult Index()
        {

            var gifts = db.Products.Include(g => g.productcategory);
            ViewBag.GiftList = new SelectList(db.ProductCategories, "ProductCategoryID", "strCategory");
            return View(gifts.ToList());

        }



        [HttpPost]
        public ActionResult Index(int? GiftList, int? priceRange)
        {
            var gifts = db.Products.Include(g => g.productcategory);
            ViewBag.GiftList = new SelectList(db.ProductCategories, "ProductCategoryID", "strCategory");
            if (GiftList != null)
            {
                gifts = gifts.Where(g => g.ProductCategoryID == GiftList);
            }
            if (priceRange != null)
            {
                switch (priceRange)
                {
                    case 1:
                        gifts = gifts.Where(g => g.Price < 50);
                        break;
                    case 2:
                        gifts = gifts.Where(g => g.Price >= 50 && g.Price <= 100);
                        break;
                    case 3:
                        gifts = gifts.Where(g => g.Price > 100);
                        break;
                }
            }
            return View(gifts);
        }

       

        //
        // GET: /Product/Details/5

        public ActionResult Details(long id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "strCategory");
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "strCategory", product.ProductCategoryID);
            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "strCategory", product.ProductCategoryID);
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "strCategory", product.ProductCategoryID);
            return View(product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}