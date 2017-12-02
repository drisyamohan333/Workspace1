using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employee.Models;

namespace Employee.Controllers
{
    public class EmployeeManagerController : Controller
    {
        private EmployeeDbContext db = new EmployeeDbContext();

        //
        // GET: /EmployeeManager/

        public ActionResult Index()
        {
            return View(db.EmployeeManagers.ToList());
        }

        //
        // GET: /EmployeeManager/Details/5

        public ActionResult Details(int id = 0)
        {
            EmployeeManager employeemanager = db.EmployeeManagers.Find(id);
            if (employeemanager == null)
            {
                return HttpNotFound();
            }
            return View(employeemanager);
        }

        //
        // GET: /EmployeeManager/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /EmployeeManager/Create

        [HttpPost]
        public ActionResult Create(EmployeeManager employeemanager)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeManagers.Add(employeemanager);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeemanager);
        }

        //
        // GET: /EmployeeManager/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EmployeeManager employeemanager = db.EmployeeManagers.Find(id);
            if (employeemanager == null)
            {
                return HttpNotFound();
            }
            return View(employeemanager);
        }

        //
        // POST: /EmployeeManager/Edit/5

        [HttpPost]
        public ActionResult Edit(EmployeeManager employeemanager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeemanager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeemanager);
        }

        //
        // GET: /EmployeeManager/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EmployeeManager employeemanager = db.EmployeeManagers.Find(id);
            if (employeemanager == null)
            {
                return HttpNotFound();
            }
            return View(employeemanager);
        }

        //
        // POST: /EmployeeManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeManager employeemanager = db.EmployeeManagers.Find(id);
            db.EmployeeManagers.Remove(employeemanager);
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