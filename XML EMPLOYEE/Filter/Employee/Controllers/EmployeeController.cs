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
    public class EmployeeController : Controller
    {
        private EmployeeDbContext db = new EmployeeDbContext();

        //
        // GET: /Employee/



        public ActionResult ImportEmployees()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportEmployees(string tbFilePath)
        {
            string strResponse;

            strResponse = "<font style='font-size:18pt;'><b>Entered employees are .... </b><br><br><br></font>";


            if (!string.IsNullOrEmpty(tbFilePath))
            {
                try
                {


                    System.Xml.Linq.XDocument xmlDoc = System.Xml.Linq.XDocument.Load(tbFilePath);

                    var emps = from e in xmlDoc.Descendants("Employee")
                                select e;

                    foreach (var emp in emps)
                    {
                        Employee.Models.Employee newEmployee = new Employee.Models.Employee();
                        
                        newEmployee.Employee_Name = emp.Element("Name").Value;
                        newEmployee.EmployeeManagerID = Convert.ToInt32(emp.Element("Manager").Value);
                        newEmployee.Salary = Convert.ToInt32(emp.Element("Salary").Value);
                        db.Employees.Add(newEmployee);
                        db.SaveChanges();
                    }
                    strResponse = "<font style='font-size:18pt;'><b>Employee name added successfully in the database.</b><br><br><br></font>";
                }
                catch (System.Exception exx)
                {
                    Console.WriteLine(exx.Message);
                    strResponse = exx.Message;
                    strResponse = exx.Source + " " + exx.StackTrace;
                    // strResponse = exx.StackTrace;
                    //   strResponse = "<font style='font-size:18pt;color:red;'><b>Invalid file path.</b><br><br><br></font>";
                }

            }
            else
                strResponse = "<font style='font-size:18pt;color:red;'><b>Please specify a valid file path.</b><br><br><br></font>";
            ViewBag.response = strResponse;
            return View();
        }




        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.employeemanager);
            ViewBag.EmployeeList = new SelectList(db.EmployeeManagers, "EmployeeManagerID", "strManager");
            return View(employees.ToList());

        }


        [HttpPost]
        public ActionResult Index(int? EmployeeList, int? salaryRange)
        {
            var emps = db.Employees.Include(e => e.employeemanager);
            ViewBag.EmployeeList = new SelectList(db.EmployeeManagers, "EmployeeManagerID", "strManager");
            if (EmployeeList != null)
            {
                emps = emps.Where(e => e.EmployeeManagerID == EmployeeList);
            }
            if (salaryRange != null)
            {
                switch (salaryRange)
                {
                    case 1:
                        emps = emps.Where(e => e.Salary <10000);
                        break;
                    case 2:
                        emps = emps.Where(e => e.Salary >= 10000 && e.Salary <= 40000);
                        break;
                    case 3:
                        emps = emps.Where(e => e.Salary > 40000);
                        break;
                }
            }
            return View(emps);
        }



        //
        // GET: /Employee/Details/5

        public ActionResult Details(long id = 0)
        {
            Employee.Models.Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            ViewBag.EmployeeManagerID = new SelectList(db.EmployeeManagers, "EmployeeManagerID", "strManager");
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(Employee.Models.Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeManagerID = new SelectList(db.EmployeeManagers, "EmployeeManagerID", "strManager", employee.EmployeeManagerID);
            return View(employee);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Employee.Models.Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeManagerID = new SelectList(db.EmployeeManagers, "EmployeeManagerID", "strManager", employee.EmployeeManagerID);
            return View(employee);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(Employee.Models.Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeManagerID = new SelectList(db.EmployeeManagers, "EmployeeManagerID", "strManager", employee.EmployeeManagerID);
            return View(employee);
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Employee.Models.Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Employee.Models.Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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